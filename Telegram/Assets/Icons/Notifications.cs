//
// Copyright Fela Ameghino 2015-2024
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//       LottieGen version:
//           7.1.0+ge1fa92580f
//       
//       Command:
//           LottieGen -Language CSharp -Namespace Telegram.Assets.Icons -Public -WinUIVersion 2.7 -InputFile Notifications.json
//       
//       Input file:
//           Notifications.json (3247 bytes created 17:41+01:00 Dec 21 2021)
//       
//       LottieGen source:
//           http://aka.ms/Lottie
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// ____________________________________
// |       Object stats       | Count |
// |__________________________|_______|
// | All CompositionObjects   |    50 |
// |--------------------------+-------|
// | Expression animators     |     9 |
// | KeyFrame animators       |     5 |
// | Reference parameters     |     8 |
// | Expression operations    |     0 |
// |--------------------------+-------|
// | Animated brushes         |     - |
// | Animated gradient stops  |     - |
// | ExpressionAnimations     |     1 |
// | PathKeyFrameAnimations   |     - |
// |--------------------------+-------|
// | ContainerVisuals         |     1 |
// | ShapeVisuals             |     1 |
// |--------------------------+-------|
// | ContainerShapes          |     - |
// | CompositionSpriteShapes  |     2 |
// |--------------------------+-------|
// | Brushes                  |     2 |
// | Gradient stops           |     2 |
// | CompositionVisualSurface |     - |
// ------------------------------------
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Numerics;
using Windows.Graphics;
using Windows.UI;
using Microsoft.UI;
using Microsoft.UI.Composition;

namespace Telegram.Assets.Icons
{
    // Name:        u_notification
    // Frame rate:  60 fps
    // Frame count: 30
    // Duration:    500.0 mS
    sealed partial class Notifications
        : Microsoft.UI.Xaml.Controls.IAnimatedVisualSource
        , Microsoft.UI.Xaml.Controls.IAnimatedVisualSource2
    {
        // Animation duration: 0.500 seconds.
        internal const long c_durationTicks = 5000000;

        public Microsoft.UI.Xaml.Controls.IAnimatedVisual TryCreateAnimatedVisual(Compositor compositor)
        {
            object ignored = null;
            return TryCreateAnimatedVisual(compositor, out ignored);
        }

        public Microsoft.UI.Xaml.Controls.IAnimatedVisual TryCreateAnimatedVisual(Compositor compositor, out object diagnostics)
        {
            diagnostics = null;

            if (Notifications_AnimatedVisual.IsRuntimeCompatible())
            {
                return
                    new Notifications_AnimatedVisual(
                        compositor
                        );
            }

            return null;
        }

        /// <summary>
        /// Gets the number of frames in the animation.
        /// </summary>
        public double FrameCount => 30d;

        /// <summary>
        /// Gets the frame rate of the animation.
        /// </summary>
        public double Framerate => 60d;

        /// <summary>
        /// Gets the duration of the animation.
        /// </summary>
        public TimeSpan Duration => TimeSpan.FromTicks(c_durationTicks);

        /// <summary>
        /// Converts a zero-based frame number to the corresponding progress value denoting the
        /// start of the frame.
        /// </summary>
        public double FrameToProgress(double frameNumber)
        {
            return frameNumber / 30d;
        }

        /// <summary>
        /// Returns a map from marker names to corresponding progress values.
        /// </summary>
        public IReadOnlyDictionary<string, double> Markers =>
            new DictionaryStringDouble
            {
                { "NormalToPointerOver_Start", 0.0 },
                { "NormalToPointerOver_End", 1 },
            };

        /// <summary>
        /// Sets the color property with the given name, or does nothing if no such property
        /// exists.
        /// </summary>
        public void SetColorProperty(string propertyName, Color value)
        {
        }

        /// <summary>
        /// Sets the scalar property with the given name, or does nothing if no such property
        /// exists.
        /// </summary>
        public void SetScalarProperty(string propertyName, double value)
        {
        }

        sealed partial class Notifications_AnimatedVisual : Microsoft.UI.Xaml.Controls.IAnimatedVisual
        {
            const long c_durationTicks = 5000000;
            readonly Compositor _c;
            readonly ExpressionAnimation _reusableExpressionAnimation;
            CompositionColorGradientStop _gradientStop_0_AlmostCornflowerBlue_FF60BCEB;
            CompositionColorGradientStop _gradientStop_1_AlmostSteelBlue_FF306FC4;
            ContainerVisual _root;
            CubicBezierEasingFunction _cubicBezierEasingFunction_0;
            ExpressionAnimation _rootProgress;
            ScalarKeyFrameAnimation _rotationAngleInDegreesScalarAnimation_0_to_0;
            StepEasingFunction _holdThenStepEasingFunction;
            StepEasingFunction _stepThenHoldEasingFunction;

            static void StartProgressBoundAnimation(
                CompositionObject target,
                string animatedPropertyName,
                CompositionAnimation animation,
                ExpressionAnimation controllerProgressExpression)
            {
                target.StartAnimation(animatedPropertyName, animation);
                var controller = target.TryGetAnimationController(animatedPropertyName);
                controller.Pause();
                controller.StartAnimation("Progress", controllerProgressExpression);
            }

            ScalarKeyFrameAnimation CreateScalarKeyFrameAnimation(float initialProgress, float initialValue, CompositionEasingFunction initialEasingFunction)
            {
                var result = _c.CreateScalarKeyFrameAnimation();
                result.Duration = TimeSpan.FromTicks(c_durationTicks);
                result.InsertKeyFrame(initialProgress, initialValue, initialEasingFunction);
                return result;
            }

            Vector2KeyFrameAnimation CreateVector2KeyFrameAnimation(float initialProgress, Vector2 initialValue, CompositionEasingFunction initialEasingFunction)
            {
                var result = _c.CreateVector2KeyFrameAnimation();
                result.Duration = TimeSpan.FromTicks(c_durationTicks);
                result.InsertKeyFrame(initialProgress, initialValue, initialEasingFunction);
                return result;
            }

            // - - - Shape tree root for layer: icon
            // - - ShapeGroup: Group 2
            CanvasGeometry Geometry_0()
            {
                CanvasGeometry result;
                using (var builder = new CanvasPathBuilder(null))
                {
                    builder.SetFilledRegionDetermination(CanvasFilledRegionDetermination.Winding);
                    builder.BeginFigure(new Vector2(60F, -15F));
                    builder.AddCubicBezier(new Vector2(60F, -48.137001F), new Vector2(33.137001F, -75F), new Vector2(0F, -75F));
                    builder.AddCubicBezier(new Vector2(-33.137001F, -75F), new Vector2(-60F, -48.137001F), new Vector2(-60F, -15F));
                    builder.AddLine(new Vector2(-60F, 30F));
                    builder.AddLine(new Vector2(-65.9449997F, 37.4309998F));
                    builder.AddCubicBezier(new Vector2(-69.6750031F, 42.0950012F), new Vector2(-68.9189987F, 48.8989983F), new Vector2(-64.2549973F, 52.6300011F));
                    builder.AddCubicBezier(new Vector2(-62.7210007F, 53.8569984F), new Vector2(-60.8889999F, 54.6380005F), new Vector2(-58.9609985F, 54.901001F));
                    builder.AddLine(new Vector2(-57.5F, 55F));
                    builder.AddLine(new Vector2(57.4790001F, 55F));
                    builder.AddCubicBezier(new Vector2(63.4539986F, 55F), new Vector2(68.2969971F, 50.1559982F), new Vector2(68.2969971F, 44.1809998F));
                    builder.AddCubicBezier(new Vector2(68.2969971F, 41.7280006F), new Vector2(67.4639969F, 39.348999F), new Vector2(65.9329987F, 37.4319992F));
                    builder.AddLine(new Vector2(60F, 30F));
                    builder.AddLine(new Vector2(60F, -15F));
                    builder.EndFigure(CanvasFigureLoop.Closed);
                    result = CanvasGeometry.CreatePath(builder);
                }
                return result;
            }

            // - - - Shape tree root for layer: icon
            // - - ShapeGroup: Group 1
            CanvasGeometry Geometry_1()
            {
                CanvasGeometry result;
                using (var builder = new CanvasPathBuilder(null))
                {
                    builder.SetFilledRegionDetermination(CanvasFilledRegionDetermination.Winding);
                    builder.BeginFigure(new Vector2(24.4990005F, 65.0039978F));
                    builder.AddCubicBezier(new Vector2(22.1809998F, 76.413002F), new Vector2(12.0930004F, 85F), new Vector2(0F, 85F));
                    builder.AddCubicBezier(new Vector2(-12.0930004F, 85F), new Vector2(-22.1809998F, 76.413002F), new Vector2(-24.4990005F, 65.0039978F));
                    builder.AddLine(new Vector2(24.4990005F, 65.0039978F));
                    builder.EndFigure(CanvasFigureLoop.Closed);
                    result = CanvasGeometry.CreatePath(builder);
                }
                return result;
            }

            // Stop 0
            CompositionColorGradientStop GradientStop_0_AlmostCornflowerBlue_FF60BCEB()
            {
                return _gradientStop_0_AlmostCornflowerBlue_FF60BCEB = _c.CreateColorGradientStop(0F, Color.FromArgb(0xFF, 0x60, 0xBC, 0xEB));
            }

            // Stop 1
            CompositionColorGradientStop GradientStop_1_AlmostSteelBlue_FF306FC4()
            {
                return _gradientStop_1_AlmostSteelBlue_FF306FC4 = _c.CreateColorGradientStop(1F, Color.FromArgb(0xFF, 0x30, 0x6F, 0xC4));
            }

            // - Shape tree root for layer: icon
            // ShapeGroup: Group 2
            CompositionLinearGradientBrush LinearGradientBrush_0()
            {
                var result = _c.CreateLinearGradientBrush();
                var colorStops = result.ColorStops;
                colorStops.Add(GradientStop_0_AlmostCornflowerBlue_FF60BCEB());
                colorStops.Add(GradientStop_1_AlmostSteelBlue_FF306FC4());
                result.MappingMode = CompositionMappingMode.Absolute;
                result.StartPoint = new Vector2(0.818000019F, -74.4800034F);
                result.EndPoint = new Vector2(-0.0670000017F, 55.223999F);
                return result;
            }

            // - Shape tree root for layer: icon
            // ShapeGroup: Group 1
            CompositionLinearGradientBrush LinearGradientBrush_1()
            {
                var result = _c.CreateLinearGradientBrush();
                var colorStops = result.ColorStops;
                colorStops.Add(_gradientStop_0_AlmostCornflowerBlue_FF60BCEB);
                colorStops.Add(_gradientStop_1_AlmostSteelBlue_FF306FC4);
                result.MappingMode = CompositionMappingMode.Absolute;
                result.StartPoint = new Vector2(0.210999995F, 66.8830032F);
                result.EndPoint = new Vector2(0.58099997F, 85.2060013F);
                return result;
            }

            // - Shape tree root for layer: icon
            // ShapeGroup: Group 2
            CompositionPathGeometry PathGeometry_0()
            {
                return _c.CreatePathGeometry(new CompositionPath(Geometry_0()));
            }

            // - Shape tree root for layer: icon
            // ShapeGroup: Group 1
            CompositionPathGeometry PathGeometry_1()
            {
                return _c.CreatePathGeometry(new CompositionPath(Geometry_1()));
            }

            // Shape tree root for layer: icon
            // Path 1
            CompositionSpriteShape SpriteShape_0()
            {
                var result = _c.CreateSpriteShape(PathGeometry_0());
                result.CenterPoint = new Vector2(0F, -20F);
                result.Offset = new Vector2(100F, 100F);
                result.FillBrush = LinearGradientBrush_0();
                StartProgressBoundAnimation(result, "RotationAngleInDegrees", RotationAngleInDegreesScalarAnimation_0_to_0(), RootProgress());
                return result;
            }

            // Shape tree root for layer: icon
            // Path 1
            CompositionSpriteShape SpriteShape_1()
            {
                var result = _c.CreateSpriteShape(PathGeometry_1());
                result.CenterPoint = new Vector2(0F, -20F);
                result.FillBrush = LinearGradientBrush_1();
                StartProgressBoundAnimation(result, "RotationAngleInDegrees", _rotationAngleInDegreesScalarAnimation_0_to_0, _rootProgress);
                StartProgressBoundAnimation(result, "Offset", OffsetVector2Animation(), _rootProgress);
                return result;
            }

            // The root of the composition.
            ContainerVisual Root()
            {
                var result = _root = _c.CreateContainerVisual();
                var propertySet = result.Properties;
                propertySet.InsertScalar("Progress", 0F);
                propertySet.InsertScalar("t0", 0F);
                propertySet.InsertScalar("t1", 0F);
                // Shape tree root for layer: icon
                result.Children.InsertAtTop(ShapeVisual_0());
                StartProgressBoundAnimation(propertySet, "t0", t0ScalarAnimation_0_to_1(), _rootProgress);
                StartProgressBoundAnimation(propertySet, "t1", t1ScalarAnimation_0_to_1(), _rootProgress);
                return result;
            }

            CubicBezierEasingFunction CubicBezierEasingFunction_0()
            {
                return _cubicBezierEasingFunction_0 = _c.CreateCubicBezierEasingFunction(new Vector2(0.600000024F, 0F), new Vector2(0.400000006F, 1F));
            }

            ExpressionAnimation RootProgress()
            {
                var result = _rootProgress = _c.CreateExpressionAnimation("_.Progress");
                result.SetReferenceParameter("_", _root);
                return result;
            }

            // Rotation
            ScalarKeyFrameAnimation RotationAngleInDegreesScalarAnimation_0_to_0()
            {
                // Frame 0.
                var result = _rotationAngleInDegreesScalarAnimation_0_to_0 = CreateScalarKeyFrameAnimation(0F, 0F, HoldThenStepEasingFunction());
                // Frame 10.
                result.InsertKeyFrame(0.333333343F, 20F, CubicBezierEasingFunction_0());
                // Frame 20.
                result.InsertKeyFrame(0.666666687F, -10F, _cubicBezierEasingFunction_0);
                // Frame 29.
                result.InsertKeyFrame(0.966666639F, 0F, _cubicBezierEasingFunction_0);
                return result;
            }

            ScalarKeyFrameAnimation t0ScalarAnimation_0_to_1()
            {
                // Frame 0.
                var result = CreateScalarKeyFrameAnimation(0F, 0F, _stepThenHoldEasingFunction);
                result.SetReferenceParameter("_", _root);
                // Frame 10.
                result.InsertKeyFrame(0.333333313F, 1F, _cubicBezierEasingFunction_0);
                // Frame 10.
                result.InsertKeyFrame(0.333333343F, 0F, _stepThenHoldEasingFunction);
                // Frame 15.
                result.InsertKeyFrame(0.49999997F, 1F, _c.CreateCubicBezierEasingFunction(new Vector2(0.600000024F, 0F), new Vector2(0.800000012F, 0.532000005F)));
                return result;
            }

            ScalarKeyFrameAnimation t1ScalarAnimation_0_to_1()
            {
                // Frame 15.
                var result = CreateScalarKeyFrameAnimation(0.5F, 0F, _stepThenHoldEasingFunction);
                result.SetReferenceParameter("_", _root);
                // Frame 20.
                result.InsertKeyFrame(0.666666627F, 1F, _c.CreateCubicBezierEasingFunction(new Vector2(0.200000003F, 0.492000014F), new Vector2(0.400000006F, 1F)));
                // Frame 20.
                result.InsertKeyFrame(0.666666687F, 0F, _stepThenHoldEasingFunction);
                // Frame 29.
                result.InsertKeyFrame(0.966666579F, 1F, _cubicBezierEasingFunction_0);
                return result;
            }

            // Shape tree root for layer: icon
            ShapeVisual ShapeVisual_0()
            {
                var result = _c.CreateShapeVisual();
                result.Size = new Vector2(200F, 200F);
                var shapes = result.Shapes;
                // ShapeGroup: Group 2
                shapes.Add(SpriteShape_0());
                // ShapeGroup: Group 1
                shapes.Add(SpriteShape_1());
                return result;
            }

            StepEasingFunction HoldThenStepEasingFunction()
            {
                var result = _holdThenStepEasingFunction = _c.CreateStepEasingFunction();
                result.IsFinalStepSingleFrame = true;
                return result;
            }

            StepEasingFunction StepThenHoldEasingFunction()
            {
                var result = _stepThenHoldEasingFunction = _c.CreateStepEasingFunction();
                result.IsInitialStepSingleFrame = true;
                return result;
            }

            // - Shape tree root for layer: icon
            // ShapeGroup: Group 1
            // Offset
            Vector2KeyFrameAnimation OffsetVector2Animation()
            {
                // Frame 0.
                var result = CreateVector2KeyFrameAnimation(0F, new Vector2(100F, 100F), _holdThenStepEasingFunction);
                result.SetReferenceParameter("_", _root);
                // Frame 10.
                result.InsertExpressionKeyFrame(0.333333313F, "Pow(1-_.t0,3)*Vector2(100,100)+(3*Square(1-_.t0)*_.t0*Vector2(95,98.333))+(3*(1-_.t0)*Square(_.t0)*Vector2(85,94.667))+(Pow(_.t0,3)*Vector2(80,93))", StepThenHoldEasingFunction());
                // Frame 15.
                result.InsertExpressionKeyFrame(0.49999997F, "Pow(1-_.t0,3)*Vector2(80,93)+(3*Square(1-_.t0)*_.t0*Vector2(77.386,92.129))+(3*(1-_.t0)*Square(_.t0)*Vector2(88.712,99.261))+(Pow(_.t0,3)*Vector2(100.116,100.541))", _stepThenHoldEasingFunction);
                // Frame 20.
                result.InsertExpressionKeyFrame(0.666666627F, "Pow(1-_.t1,3)*Vector2(100.116,100.541)+(3*Square(1-_.t1)*_.t1*Vector2(110.525,101.709))+(3*(1-_.t1)*Square(_.t1)*Vector2(121,97))+(Pow(_.t1,3)*Vector2(121,97))", _stepThenHoldEasingFunction);
                // Frame 29.
                result.InsertExpressionKeyFrame(0.966666579F, "Pow(1-_.t1,3)*Vector2(121,97)+(3*Square(1-_.t1)*_.t1*Vector2(121,97))+(3*(1-_.t1)*Square(_.t1)*Vector2(105,101.667))+(Pow(_.t1,3)*Vector2(100,100))", _stepThenHoldEasingFunction);
                // Frame 29.
                result.InsertKeyFrame(0.966666639F, new Vector2(100F, 100F), _stepThenHoldEasingFunction);
                return result;
            }

            internal Notifications_AnimatedVisual(
                Compositor compositor
                )
            {
                _c = compositor;
                _reusableExpressionAnimation = compositor.CreateExpressionAnimation();
                Root();
            }

            public Visual RootVisual => _root;
            public TimeSpan Duration => TimeSpan.FromTicks(c_durationTicks);
            public Vector2 Size => new Vector2(200F, 200F);
            void IDisposable.Dispose() => _root?.Dispose();

            internal static bool IsRuntimeCompatible()
            {
                return Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7);
            }
        }
    }
}
