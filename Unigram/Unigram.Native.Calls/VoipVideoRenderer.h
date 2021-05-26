#include "pch.h"

#include <stddef.h>
#include <memory>

#include "api/video/i420_buffer.h"
#include "libyuv.h"

#include "api/video/video_frame.h"
#include "api/video/video_source_interface.h"
#include "media/base/video_adapter.h"
#include "media/base/video_broadcaster.h"

#include <winrt/Microsoft.Graphics.Canvas.h>
#include <winrt/Microsoft.Graphics.Canvas.Effects.h>
#include <winrt/Windows.Storage.h>

#include <Microsoft.Graphics.Canvas.h>
#include <Microsoft.Graphics.Canvas.native.h>

#include <d3d11.h>

namespace ABI {
	using namespace Microsoft::Graphics::Canvas;
}

using namespace winrt::Microsoft::Graphics::Canvas;
using namespace winrt::Microsoft::Graphics::Canvas::Effects;
using namespace winrt::Microsoft::Graphics::Canvas::UI::Xaml;
using namespace winrt::Windows::Foundation::Numerics;
using namespace winrt::Windows::Graphics::DirectX;

struct VoipVideoRenderer : public rtc::VideoSinkInterface<webrtc::VideoFrame>
{
	bool m_disposed{ false };
	bool m_readyToDraw;

	winrt::event_token m_eventToken;
	winrt::slim_mutex m_lock;

	std::shared_ptr<CanvasControl> m_canvasControl;
	webrtc::VideoRotation m_rotation{ webrtc::kVideoRotation_0 };
	PixelShaderEffect m_shader{ nullptr };
	CanvasBitmap m_bitmapY{ nullptr };
	CanvasBitmap m_bitmapU{ nullptr };
	CanvasBitmap m_bitmapV{ nullptr };

	VoipVideoRenderer(CanvasControl canvas, bool fill) {
		m_canvasControl = std::make_shared<CanvasControl>(canvas);
		m_readyToDraw = canvas.ReadyToDraw();

		m_eventToken = canvas.Draw([this, fill](const CanvasControl sender, CanvasDrawEventArgs const args) {
			m_readyToDraw = true;

			if (m_bitmapY != nullptr) {
				float width = sender.Size().Width;
				float height = sender.Size().Height;
				float bitmapWidth = m_bitmapY.SizeInPixels().Width;
				float bitmapHeight = m_bitmapY.SizeInPixels().Height;

				bool rotate = m_rotation == webrtc::kVideoRotation_90 || m_rotation == webrtc::kVideoRotation_270;
				float ratioX = width / (rotate ? bitmapHeight : bitmapWidth);
				float ratioY = height / (rotate ? bitmapWidth : bitmapHeight);
				float x = (width - bitmapWidth) / 2;
				float y = (height - bitmapHeight) / 2;

				float3x2 matrix;
				switch (m_rotation) {
				case webrtc::kVideoRotation_0:
					matrix = float3x2::identity();
					break;
				case webrtc::kVideoRotation_180:
					matrix = make_float3x2_rotation(180 * (M_PI / 180), float2(width / 2, height / 2));
					break;
				case webrtc::kVideoRotation_90:
					matrix = make_float3x2_rotation(90 * (M_PI / 180), float2(width / 2, height / 2));
					break;
				case webrtc::kVideoRotation_270:
					matrix = make_float3x2_rotation(270 * (M_PI / 180), float2(width / 2, height / 2));
					break;
				}

				args.DrawingSession().Transform(matrix * make_float3x2_scale(std::min(ratioX, ratioY), float2(width / 2, height / 2)));
				args.DrawingSession().DrawImage(m_shader, x, y);
			}
			});
	}

	~VoipVideoRenderer() {
		winrt::slim_lock_guard const guard(m_lock);

		m_disposed = true;

		if (m_canvasControl) {
			m_canvasControl->Draw(m_eventToken);
			m_canvasControl = nullptr;
		}

		if (m_shader != nullptr) {
			m_shader.Close();
			m_shader = nullptr;
		}

		if (m_bitmapY != nullptr) {
			m_bitmapY.Close();
			m_bitmapY = nullptr;
		}

		if (m_bitmapU != nullptr) {
			m_bitmapU.Close();
			m_bitmapU = nullptr;
		}

		if (m_bitmapV != nullptr) {
			m_bitmapV.Close();
			m_bitmapV = nullptr;
		}
	}

	void OnFrame(const webrtc::VideoFrame& frame) override
	{
		winrt::slim_lock_guard const guard(m_lock);

		if (!m_canvasControl || m_disposed || !m_readyToDraw) {
			return;
		}

		rtc::scoped_refptr<webrtc::I420BufferInterface> buffer(frame.video_frame_buffer()->ToI420());

		m_rotation = frame.rotation();
		int32_t width = buffer->width();
		int32_t height = buffer->height();

		auto sizeY = buffer->StrideY() * height;
		auto sizeUV = sizeY / 2;

		if (m_bitmapY == nullptr || m_bitmapY.SizeInPixels().Width != width || m_bitmapY.SizeInPixels().Height != height)
		{
			auto creator = m_canvasControl->as<ICanvasResourceCreatorWithDpi>();
			auto format = DirectXPixelFormat::R8UIntNormalized;

			// This is needed to force BGRA rendering
			uint8_t* fill = new uint8_t[width * height * 4];
			std::fill_n(fill, width * height * 4, 0xFFFFFFFF);
			auto bgra = winrt::array_view<uint8_t const>(fill, width * height * 4);

			auto yView = winrt::array_view<uint8_t const>(buffer->DataY(), sizeY);
			m_bitmapY = CanvasBitmap::CreateFromBytes(creator, yView, width, height, format);
			auto uView = winrt::array_view<uint8_t const>(buffer->DataU(), sizeUV);
			m_bitmapU = CanvasBitmap::CreateFromBytes(creator, uView, width / 2, height / 2, format);
			auto vView = winrt::array_view<uint8_t const>(buffer->DataV(), sizeUV);
			m_bitmapV = CanvasBitmap::CreateFromBytes(creator, vView, width / 2, height / 2, format);

			if (m_shader == nullptr)
			{
				FILE* file = _wfopen(L"Assets\\i420.bin", L"rb");
				fseek(file, 0, SEEK_END);
				size_t length = ftell(file);
				fseek(file, 0, SEEK_SET);
				uint8_t* buffer = new uint8_t[length];
				fread(buffer, 1, length, file);
				fclose(file);

				auto shaderView = winrt::array_view<uint8_t const>(buffer, buffer + length);
				m_shader = winrt::Microsoft::Graphics::Canvas::Effects::PixelShaderEffect(shaderView);
				m_shader.Source1BorderMode(EffectBorderMode::Hard);
				m_shader.Source2BorderMode(EffectBorderMode::Hard);
				m_shader.Source3BorderMode(EffectBorderMode::Hard);

				delete[] buffer;
			}

			m_shader.Source1(m_bitmapY);
			m_shader.Source2(m_bitmapU);
			m_shader.Source3(m_bitmapV);
			m_shader.Source4(CanvasBitmap::CreateFromBytes(creator, bgra, width, height, DirectXPixelFormat::R8G8B8A8UIntNormalized));

			delete[] fill;
		}
		else
		{
			m_bitmapY.as<ABI::ICanvasBitmap>()->SetPixelBytes(sizeY, (BYTE*)buffer->DataY());
			m_bitmapU.as<ABI::ICanvasBitmap>()->SetPixelBytes(sizeUV, (BYTE*)buffer->DataU());
			m_bitmapV.as<ABI::ICanvasBitmap>()->SetPixelBytes(sizeUV, (BYTE*)buffer->DataV());
		}

		m_canvasControl->Invalidate();
	}
};
