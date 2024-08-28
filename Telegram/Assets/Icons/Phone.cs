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
//           LottieGen -Language CSharp -Namespace Telegram.Assets.Icons -Public -WinUIVersion 2.7 -InputFile Phone.json
//       
//       Input file:
//           Phone.json (3476 bytes created 17:41+01:00 Dec 21 2021)
//       
//       LottieGen source:
//           http://aka.ms/Lottie
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// ___________________________________________________________
// |       Object stats       | UAP v11 count | UAP v7 count |
// |__________________________|_______________|______________|
// | All CompositionObjects   |            50 |           46 |
// |--------------------------+---------------+--------------|
// | Expression animators     |             5 |            4 |
// | KeyFrame animators       |             5 |            4 |
// | Reference parameters     |             5 |            4 |
// | Expression operations    |             0 |            0 |
// |--------------------------+---------------+--------------|
// | Animated brushes         |             - |            - |
// | Animated gradient stops  |             - |            - |
// | ExpressionAnimations     |             1 |            1 |
// | PathKeyFrameAnimations   |             1 |            - |
// |--------------------------+---------------+--------------|
// | ContainerVisuals         |             1 |            1 |
// | ShapeVisuals             |             1 |            1 |
// |--------------------------+---------------+--------------|
// | ContainerShapes          |             1 |            1 |
// | CompositionSpriteShapes  |             2 |            2 |
// |--------------------------+---------------+--------------|
// | Brushes                  |             2 |            2 |
// | Gradient stops           |             2 |            2 |
// | CompositionVisualSurface |             - |            - |
// -----------------------------------------------------------
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
    // Name:        u_phone
    // Frame rate:  60 fps
    // Frame count: 30
    // Duration:    500.0 mS
    sealed partial class Phone
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

            if (Phone_AnimatedVisual_UAPv11.IsRuntimeCompatible())
            {
                return
                    new Phone_AnimatedVisual_UAPv11(
                        compositor
                        );
            }

            if (Phone_AnimatedVisual_UAPv7.IsRuntimeCompatible())
            {
                return
                    new Phone_AnimatedVisual_UAPv7(
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

        sealed partial class Phone_AnimatedVisual_UAPv11 : Microsoft.UI.Xaml.Controls.IAnimatedVisual
        {
            const long c_durationTicks = 5000000;
            readonly Compositor _c;
            readonly ExpressionAnimation _reusableExpressionAnimation;
            ContainerVisual _root;
            CubicBezierEasingFunction _cubicBezierEasingFunction_0;
            ExpressionAnimation _rootProgress;
            StepEasingFunction _holdThenStepEasingFunction;

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

            PathKeyFrameAnimation CreatePathKeyFrameAnimation(float initialProgress, CompositionPath initialValue, CompositionEasingFunction initialEasingFunction)
            {
                var result = _c.CreatePathKeyFrameAnimation();
                result.Duration = TimeSpan.FromTicks(c_durationTicks);
                result.InsertKeyFrame(initialProgress, initialValue, initialEasingFunction);
                return result;
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

            CompositionSpriteShape CreateSpriteShape(CompositionGeometry geometry, Matrix3x2 transformMatrix, CompositionBrush fillBrush)
            {
                var result = _c.CreateSpriteShape(geometry);
                result.TransformMatrix = transformMatrix;
                result.FillBrush = fillBrush;
                return result;
            }

            // - - - - - Shape tree root for layer: icon
            // - - - ShapeGroup: Group 2 Offset:<100, 100>
            // - Path
            CanvasGeometry Geometry_0()
            {
                CanvasGeometry result;
                using (var builder = new CanvasPathBuilder(null))
                {
                    builder.SetFilledRegionDetermination(CanvasFilledRegionDetermination.Winding);
                    builder.BeginFigure(new Vector2(-35F, -80F));
                    builder.AddCubicBezier(new Vector2(-43.2840004F, -80F), new Vector2(-50F, -73.2839966F), new Vector2(-50F, -65F));
                    builder.AddCubicBezier(new Vector2(-50F, -65F), new Vector2(-50F, 65F), new Vector2(-50F, 65F));
                    builder.AddCubicBezier(new Vector2(-50F, 73.2839966F), new Vector2(-43.2840004F, 80F), new Vector2(-35F, 80F));
                    builder.AddCubicBezier(new Vector2(-35F, 80F), new Vector2(35F, 80F), new Vector2(35F, 80F));
                    builder.AddCubicBezier(new Vector2(43.2840004F, 80F), new Vector2(50F, 73.2839966F), new Vector2(50F, 65F));
                    builder.AddCubicBezier(new Vector2(50F, 65F), new Vector2(50F, -65F), new Vector2(50F, -65F));
                    builder.AddCubicBezier(new Vector2(50F, -73.2839966F), new Vector2(43.2840004F, -80F), new Vector2(35F, -80F));
                    builder.AddCubicBezier(new Vector2(35F, -80F), new Vector2(-35F, -80F), new Vector2(-35F, -80F));
                    builder.EndFigure(CanvasFigureLoop.Closed);
                    result = CanvasGeometry.CreatePath(builder);
                }
                return result;
            }

            // - - - - - Shape tree root for layer: icon
            // - - - ShapeGroup: Group 2 Offset:<100, 100>
            // - Path
            CanvasGeometry Geometry_1()
            {
                CanvasGeometry result;
                using (var builder = new CanvasPathBuilder(null))
                {
                    builder.SetFilledRegionDetermination(CanvasFilledRegionDetermination.Winding);
                    builder.BeginFigure(new Vector2(35F, -80F));
                    builder.AddCubicBezier(new Vector2(43.2840004F, -80F), new Vector2(50F, -73.2839966F), new Vector2(50F, -65F));
                    builder.AddCubicBezier(new Vector2(50F, -65F), new Vector2(50F, 65F), new Vector2(50F, 65F));
                    builder.AddCubicBezier(new Vector2(50F, 73.2839966F), new Vector2(43.2840004F, 80F), new Vector2(35F, 80F));
                    builder.AddCubicBezier(new Vector2(35F, 80F), new Vector2(-35F, 80F), new Vector2(-35F, 80F));
                    builder.AddCubicBezier(new Vector2(-43.2840004F, 80F), new Vector2(-50F, 73.2839966F), new Vector2(-50F, 65F));
                    builder.AddCubicBezier(new Vector2(-50F, 65F), new Vector2(-50F, -65F), new Vector2(-50F, -65F));
                    builder.AddCubicBezier(new Vector2(-50F, -73.2839966F), new Vector2(-43.2840004F, -80F), new Vector2(-35F, -80F));
                    builder.AddCubicBezier(new Vector2(-35F, -80F), new Vector2(35F, -80F), new Vector2(35F, -80F));
                    builder.EndFigure(CanvasFigureLoop.Closed);
                    result = CanvasGeometry.CreatePath(builder);
                }
                return result;
            }

            // - - - - Shape tree root for layer: icon
            // - - ShapeGroup: Group 1
            CanvasGeometry Geometry_2()
            {
                CanvasGeometry result;
                using (var builder = new CanvasPathBuilder(null))
                {
                    builder.BeginFigure(new Vector2(90F, 145F));
                    builder.AddLine(new Vector2(110F, 145F));
                    builder.EndFigure(CanvasFigureLoop.Open);
                    result = CanvasGeometry.CreatePath(builder);
                }
                return result;
            }

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 1
            CompositionColorBrush ColorBrush_White()
            {
                return _c.CreateColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 2 Offset:<100, 100>
            // Stop 0
            CompositionColorGradientStop GradientStop_0_AlmostLightSkyBlue_FF7DE2FB()
            {
                return _c.CreateColorGradientStop(0F, Color.FromArgb(0xFF, 0x7D, 0xE2, 0xFB));
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 2 Offset:<100, 100>
            // Stop 1
            CompositionColorGradientStop GradientStop_1_AlmostDarkSlateBlue_FF284E81()
            {
                return _c.CreateColorGradientStop(1F, Color.FromArgb(0xFF, 0x28, 0x4E, 0x81));
            }

            // Shape tree root for layer: icon
            CompositionContainerShape ContainerShape()
            {
                var result = _c.CreateContainerShape();
                result.CenterPoint = new Vector2(100F, 100F);
                var shapes = result.Shapes;
                // ShapeGroup: Group 2 Offset:<100, 100>
                shapes.Add(SpriteShape_0());
                // ShapeGroup: Group 1
                shapes.Add(SpriteShape_1());
                StartProgressBoundAnimation(result, "Scale", ScaleVector2Animation(), _rootProgress);
                return result;
            }

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 2 Offset:<100, 100>
            CompositionLinearGradientBrush LinearGradientBrush()
            {
                var result = _c.CreateLinearGradientBrush();
                var colorStops = result.ColorStops;
                colorStops.Add(GradientStop_0_AlmostLightSkyBlue_FF7DE2FB());
                colorStops.Add(GradientStop_1_AlmostDarkSlateBlue_FF284E81());
                result.MappingMode = CompositionMappingMode.Absolute;
                result.StartPoint = new Vector2(-0.43900001F, -79.5309982F);
                result.EndPoint = new Vector2(-0.354999989F, 79.7639999F);
                return result;
            }

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 2 Offset:<100, 100>
            CompositionPathGeometry PathGeometry_0()
            {
                var result = _c.CreatePathGeometry();
                StartProgressBoundAnimation(result, "Path", PathKeyFrameAnimation_0(), RootProgress());
                return result;
            }

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 1
            CompositionPathGeometry PathGeometry_1()
            {
                var result = _c.CreatePathGeometry(new CompositionPath(Geometry_2()));
                StartProgressBoundAnimation(result, "TrimStart", TrimStartScalarAnimation_0_to_0(), _rootProgress);
                StartProgressBoundAnimation(result, "TrimEnd", TrimEndScalarAnimation_1_to_1(), _rootProgress);
                return result;
            }

            // - Shape tree root for layer: icon
            // Path 1
            CompositionSpriteShape SpriteShape_0()
            {
                // Offset:<100, 100>
                var geometry = PathGeometry_0();
                var result = CreateSpriteShape(geometry, new Matrix3x2(1F, 0F, 0F, 1F, 100F, 100F), LinearGradientBrush());
                return result;
            }

            // - Shape tree root for layer: icon
            // Path 1
            CompositionSpriteShape SpriteShape_1()
            {
                var result = _c.CreateSpriteShape(PathGeometry_1());
                result.StrokeBrush = ColorBrush_White();
                result.StrokeDashCap = CompositionStrokeCap.Round;
                result.StrokeStartCap = CompositionStrokeCap.Round;
                result.StrokeEndCap = CompositionStrokeCap.Round;
                result.StrokeMiterLimit = 5F;
                StartProgressBoundAnimation(result, "StrokeThickness", StrokeThicknessScalarAnimation_10_to_10(), _rootProgress);
                return result;
            }

            // The root of the composition.
            ContainerVisual Root()
            {
                var result = _root = _c.CreateContainerVisual();
                var propertySet = result.Properties;
                propertySet.InsertScalar("Progress", 0F);
                // Shape tree root for layer: icon
                result.Children.InsertAtTop(ShapeVisual_0());
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

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 2 Offset:<100, 100>
            // Path
            PathKeyFrameAnimation PathKeyFrameAnimation_0()
            {
                // Frame 0.
                var result = CreatePathKeyFrameAnimation(0F, new CompositionPath(Geometry_0()), HoldThenStepEasingFunction());
                // Frame 29.
                result.InsertKeyFrame(0.966666639F, new CompositionPath(Geometry_1()), CubicBezierEasingFunction_0());
                return result;
            }

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 1
            // StrokeThickness
            ScalarKeyFrameAnimation StrokeThicknessScalarAnimation_10_to_10()
            {
                // Frame 0.
                var result = CreateScalarKeyFrameAnimation(0F, 10F, StepThenHoldEasingFunction());
                // Frame 8.
                result.InsertKeyFrame(0.266666681F, 10F, _holdThenStepEasingFunction);
                // Frame 12.
                result.InsertKeyFrame(0.400000006F, 0F, _cubicBezierEasingFunction_0);
                // Frame 17.
                result.InsertKeyFrame(0.566666663F, 0F, _cubicBezierEasingFunction_0);
                // Frame 21.
                result.InsertKeyFrame(0.699999988F, 10F, _cubicBezierEasingFunction_0);
                return result;
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 1
            // TrimEnd
            ScalarKeyFrameAnimation TrimEndScalarAnimation_1_to_1()
            {
                // Frame 0.
                var result = CreateScalarKeyFrameAnimation(0F, 1F, _holdThenStepEasingFunction);
                // Frame 12.
                result.InsertKeyFrame(0.400000006F, 0.5F, _cubicBezierEasingFunction_0);
                // Frame 17.
                result.InsertKeyFrame(0.566666663F, 0.5F, _cubicBezierEasingFunction_0);
                // Frame 29.
                result.InsertKeyFrame(0.966666639F, 1F, _cubicBezierEasingFunction_0);
                return result;
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 1
            // TrimStart
            ScalarKeyFrameAnimation TrimStartScalarAnimation_0_to_0()
            {
                // Frame 0.
                var result = CreateScalarKeyFrameAnimation(0F, 0F, _holdThenStepEasingFunction);
                // Frame 12.
                result.InsertKeyFrame(0.400000006F, 0.5F, _cubicBezierEasingFunction_0);
                // Frame 17.
                result.InsertKeyFrame(0.566666663F, 0.5F, _cubicBezierEasingFunction_0);
                // Frame 29.
                result.InsertKeyFrame(0.966666639F, 0F, _cubicBezierEasingFunction_0);
                return result;
            }

            // Shape tree root for layer: icon
            ShapeVisual ShapeVisual_0()
            {
                var result = _c.CreateShapeVisual();
                result.Size = new Vector2(200F, 200F);
                result.Shapes.Add(ContainerShape());
                return result;
            }

            StepEasingFunction HoldThenStepEasingFunction()
            {
                var result = _holdThenStepEasingFunction = _c.CreateStepEasingFunction();
                result.IsFinalStepSingleFrame = true;
                return result;
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 1
            // StrokeThickness
            StepEasingFunction StepThenHoldEasingFunction()
            {
                var result = _c.CreateStepEasingFunction();
                result.IsInitialStepSingleFrame = true;
                return result;
            }

            // - Shape tree root for layer: icon
            // Scale
            Vector2KeyFrameAnimation ScaleVector2Animation()
            {
                // Frame 0.
                var result = CreateVector2KeyFrameAnimation(0F, new Vector2(1F, 1F), _holdThenStepEasingFunction);
                // Frame 8.
                result.InsertKeyFrame(0.266666681F, new Vector2(1.12F, 1.12F), _cubicBezierEasingFunction_0);
                // Frame 16.
                result.InsertKeyFrame(0.533333361F, new Vector2(0.949999988F, 0.949999988F), _cubicBezierEasingFunction_0);
                // Frame 24.
                result.InsertKeyFrame(0.800000012F, new Vector2(1F, 1F), _cubicBezierEasingFunction_0);
                return result;
            }

            internal Phone_AnimatedVisual_UAPv11(
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
                return Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 11);
            }
        }

        sealed partial class Phone_AnimatedVisual_UAPv7 : Microsoft.UI.Xaml.Controls.IAnimatedVisual
        {
            const long c_durationTicks = 5000000;
            readonly Compositor _c;
            readonly ExpressionAnimation _reusableExpressionAnimation;
            ContainerVisual _root;
            CubicBezierEasingFunction _cubicBezierEasingFunction_0;
            ExpressionAnimation _rootProgress;
            StepEasingFunction _holdThenStepEasingFunction;

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

            CompositionSpriteShape CreateSpriteShape(CompositionGeometry geometry, Matrix3x2 transformMatrix, CompositionBrush fillBrush)
            {
                var result = _c.CreateSpriteShape(geometry);
                result.TransformMatrix = transformMatrix;
                result.FillBrush = fillBrush;
                return result;
            }

            // - - - - Shape tree root for layer: icon
            // - - ShapeGroup: Group 2 Offset:<100, 100>
            CanvasGeometry Geometry_0()
            {
                CanvasGeometry result;
                using (var builder = new CanvasPathBuilder(null))
                {
                    builder.SetFilledRegionDetermination(CanvasFilledRegionDetermination.Winding);
                    builder.BeginFigure(new Vector2(-35F, -80F));
                    builder.AddCubicBezier(new Vector2(-43.2840004F, -80F), new Vector2(-50F, -73.2839966F), new Vector2(-50F, -65F));
                    builder.AddLine(new Vector2(-50F, 65F));
                    builder.AddCubicBezier(new Vector2(-50F, 73.2839966F), new Vector2(-43.2840004F, 80F), new Vector2(-35F, 80F));
                    builder.AddLine(new Vector2(35F, 80F));
                    builder.AddCubicBezier(new Vector2(43.2840004F, 80F), new Vector2(50F, 73.2839966F), new Vector2(50F, 65F));
                    builder.AddLine(new Vector2(50F, -65F));
                    builder.AddCubicBezier(new Vector2(50F, -73.2839966F), new Vector2(43.2840004F, -80F), new Vector2(35F, -80F));
                    builder.AddLine(new Vector2(-35F, -80F));
                    builder.EndFigure(CanvasFigureLoop.Closed);
                    result = CanvasGeometry.CreatePath(builder);
                }
                return result;
            }

            // - - - - Shape tree root for layer: icon
            // - - ShapeGroup: Group 1
            CanvasGeometry Geometry_1()
            {
                CanvasGeometry result;
                using (var builder = new CanvasPathBuilder(null))
                {
                    builder.BeginFigure(new Vector2(90F, 145F));
                    builder.AddLine(new Vector2(110F, 145F));
                    builder.EndFigure(CanvasFigureLoop.Open);
                    result = CanvasGeometry.CreatePath(builder);
                }
                return result;
            }

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 1
            CompositionColorBrush ColorBrush_White()
            {
                return _c.CreateColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 2 Offset:<100, 100>
            // Stop 0
            CompositionColorGradientStop GradientStop_0_AlmostLightSkyBlue_FF7DE2FB()
            {
                return _c.CreateColorGradientStop(0F, Color.FromArgb(0xFF, 0x7D, 0xE2, 0xFB));
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 2 Offset:<100, 100>
            // Stop 1
            CompositionColorGradientStop GradientStop_1_AlmostDarkSlateBlue_FF284E81()
            {
                return _c.CreateColorGradientStop(1F, Color.FromArgb(0xFF, 0x28, 0x4E, 0x81));
            }

            // Shape tree root for layer: icon
            CompositionContainerShape ContainerShape()
            {
                var result = _c.CreateContainerShape();
                result.CenterPoint = new Vector2(100F, 100F);
                var shapes = result.Shapes;
                // ShapeGroup: Group 2 Offset:<100, 100>
                shapes.Add(SpriteShape_0());
                // ShapeGroup: Group 1
                shapes.Add(SpriteShape_1());
                StartProgressBoundAnimation(result, "Scale", ScaleVector2Animation(), _rootProgress);
                return result;
            }

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 2 Offset:<100, 100>
            CompositionLinearGradientBrush LinearGradientBrush()
            {
                var result = _c.CreateLinearGradientBrush();
                var colorStops = result.ColorStops;
                colorStops.Add(GradientStop_0_AlmostLightSkyBlue_FF7DE2FB());
                colorStops.Add(GradientStop_1_AlmostDarkSlateBlue_FF284E81());
                result.MappingMode = CompositionMappingMode.Absolute;
                result.StartPoint = new Vector2(-0.43900001F, -79.5309982F);
                result.EndPoint = new Vector2(-0.354999989F, 79.7639999F);
                return result;
            }

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 2 Offset:<100, 100>
            CompositionPathGeometry PathGeometry_0()
            {
                return _c.CreatePathGeometry(new CompositionPath(Geometry_0()));
            }

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 1
            CompositionPathGeometry PathGeometry_1()
            {
                var result = _c.CreatePathGeometry(new CompositionPath(Geometry_1()));
                StartProgressBoundAnimation(result, "TrimStart", TrimStartScalarAnimation_0_to_0(), RootProgress());
                StartProgressBoundAnimation(result, "TrimEnd", TrimEndScalarAnimation_1_to_1(), _rootProgress);
                return result;
            }

            // - Shape tree root for layer: icon
            // Path 1
            CompositionSpriteShape SpriteShape_0()
            {
                // Offset:<100, 100>
                var geometry = PathGeometry_0();
                var result = CreateSpriteShape(geometry, new Matrix3x2(1F, 0F, 0F, 1F, 100F, 100F), LinearGradientBrush());
                return result;
            }

            // - Shape tree root for layer: icon
            // Path 1
            CompositionSpriteShape SpriteShape_1()
            {
                var result = _c.CreateSpriteShape(PathGeometry_1());
                result.StrokeBrush = ColorBrush_White();
                result.StrokeDashCap = CompositionStrokeCap.Round;
                result.StrokeStartCap = CompositionStrokeCap.Round;
                result.StrokeEndCap = CompositionStrokeCap.Round;
                result.StrokeMiterLimit = 5F;
                StartProgressBoundAnimation(result, "StrokeThickness", StrokeThicknessScalarAnimation_10_to_10(), _rootProgress);
                return result;
            }

            // The root of the composition.
            ContainerVisual Root()
            {
                var result = _root = _c.CreateContainerVisual();
                var propertySet = result.Properties;
                propertySet.InsertScalar("Progress", 0F);
                // Shape tree root for layer: icon
                result.Children.InsertAtTop(ShapeVisual_0());
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

            // - - Shape tree root for layer: icon
            // ShapeGroup: Group 1
            // StrokeThickness
            ScalarKeyFrameAnimation StrokeThicknessScalarAnimation_10_to_10()
            {
                // Frame 0.
                var result = CreateScalarKeyFrameAnimation(0F, 10F, StepThenHoldEasingFunction());
                // Frame 8.
                result.InsertKeyFrame(0.266666681F, 10F, _holdThenStepEasingFunction);
                // Frame 12.
                result.InsertKeyFrame(0.400000006F, 0F, _cubicBezierEasingFunction_0);
                // Frame 17.
                result.InsertKeyFrame(0.566666663F, 0F, _cubicBezierEasingFunction_0);
                // Frame 21.
                result.InsertKeyFrame(0.699999988F, 10F, _cubicBezierEasingFunction_0);
                return result;
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 1
            // TrimEnd
            ScalarKeyFrameAnimation TrimEndScalarAnimation_1_to_1()
            {
                // Frame 0.
                var result = CreateScalarKeyFrameAnimation(0F, 1F, _holdThenStepEasingFunction);
                // Frame 12.
                result.InsertKeyFrame(0.400000006F, 0.5F, _cubicBezierEasingFunction_0);
                // Frame 17.
                result.InsertKeyFrame(0.566666663F, 0.5F, _cubicBezierEasingFunction_0);
                // Frame 29.
                result.InsertKeyFrame(0.966666639F, 1F, _cubicBezierEasingFunction_0);
                return result;
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 1
            // TrimStart
            ScalarKeyFrameAnimation TrimStartScalarAnimation_0_to_0()
            {
                // Frame 0.
                var result = CreateScalarKeyFrameAnimation(0F, 0F, HoldThenStepEasingFunction());
                // Frame 12.
                result.InsertKeyFrame(0.400000006F, 0.5F, CubicBezierEasingFunction_0());
                // Frame 17.
                result.InsertKeyFrame(0.566666663F, 0.5F, _cubicBezierEasingFunction_0);
                // Frame 29.
                result.InsertKeyFrame(0.966666639F, 0F, _cubicBezierEasingFunction_0);
                return result;
            }

            // Shape tree root for layer: icon
            ShapeVisual ShapeVisual_0()
            {
                var result = _c.CreateShapeVisual();
                result.Size = new Vector2(200F, 200F);
                result.Shapes.Add(ContainerShape());
                return result;
            }

            StepEasingFunction HoldThenStepEasingFunction()
            {
                var result = _holdThenStepEasingFunction = _c.CreateStepEasingFunction();
                result.IsFinalStepSingleFrame = true;
                return result;
            }

            // - - - Shape tree root for layer: icon
            // - ShapeGroup: Group 1
            // StrokeThickness
            StepEasingFunction StepThenHoldEasingFunction()
            {
                var result = _c.CreateStepEasingFunction();
                result.IsInitialStepSingleFrame = true;
                return result;
            }

            // - Shape tree root for layer: icon
            // Scale
            Vector2KeyFrameAnimation ScaleVector2Animation()
            {
                // Frame 0.
                var result = CreateVector2KeyFrameAnimation(0F, new Vector2(1F, 1F), _holdThenStepEasingFunction);
                // Frame 8.
                result.InsertKeyFrame(0.266666681F, new Vector2(1.12F, 1.12F), _cubicBezierEasingFunction_0);
                // Frame 16.
                result.InsertKeyFrame(0.533333361F, new Vector2(0.949999988F, 0.949999988F), _cubicBezierEasingFunction_0);
                // Frame 24.
                result.InsertKeyFrame(0.800000012F, new Vector2(1F, 1F), _cubicBezierEasingFunction_0);
                return result;
            }

            internal Phone_AnimatedVisual_UAPv7(
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
