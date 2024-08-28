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
//           7.1.0-build.5+g109463c06a
//       
//       Command:
//           LottieGen -GenerateColorBindings -Language CSharp -MinimumUapVersion 8 -Public -WinUIVersion 2.7 -InputFile select.json
//       
//       Input file:
//           select.json (4668 bytes created 16:34+02:00 Apr 23 2022)
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
// | All CompositionObjects   |    65 |
// |--------------------------+-------|
// | Expression animators     |     4 |
// | KeyFrame animators       |     2 |
// | Reference parameters     |     4 |
// | Expression operations    |     8 |
// |--------------------------+-------|
// | Animated brushes         |     2 |
// | Animated gradient stops  |     - |
// | ExpressionAnimations     |     3 |
// | PathKeyFrameAnimations   |     - |
// |--------------------------+-------|
// | ContainerVisuals         |     1 |
// | ShapeVisuals             |     1 |
// |--------------------------+-------|
// | ContainerShapes          |     - |
// | CompositionSpriteShapes  |     4 |
// |--------------------------+-------|
// | Brushes                  |     3 |
// | Gradient stops           |    10 |
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
    // Name:        select
    // Frame rate:  60 fps
    // Frame count: 20
    // Duration:    333.3 mS
    // ___________________________________________________________
    // | Theme property |   Accessor   | Type  |  Default value  |
    // |________________|______________|_______|_________________|
    // | #FF0000        | Color_FF0000 | Color |  #FFFF0000 Red  |
    // | #FFFFFF        | Color_FFFFFF | Color | #FFFFFFFF White |
    // -----------------------------------------------------------
    public sealed partial class Select
        : Microsoft.UI.Xaml.Controls.IAnimatedVisualSource
        , Microsoft.UI.Xaml.Controls.IAnimatedVisualSource2
    {
        // Animation duration: 0.333 seconds.
        internal const long c_durationTicks = 2500000;

        // Theme property: Color_FF0000.
        internal static readonly Color c_themeColor_FF0000 = Color.FromArgb(0xFF, 0xFF, 0x00, 0x00);

        // Theme property: Color_FFFFFF.
        internal static readonly Color c_themeColor_FFFFFF = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);

        CompositionPropertySet _themeProperties;
        Color _themeColor_Background = c_themeColor_FF0000;
        Color _themeColor_Foreground = c_themeColor_FFFFFF;

        // Theme properties.
        public Color Background
        {
            get => _themeColor_Background;
            set
            {
                _themeColor_Background = value;
                if (_themeProperties != null)
                {
                    _themeProperties.InsertVector4("Color_FF0000", ColorAsVector4((Color)_themeColor_Background));
                }
            }
        }

        public Color Foreground
        {
            get => _themeColor_Foreground;
            set
            {
                _themeColor_Foreground = value;
                if (_themeProperties != null)
                {
                    _themeProperties.InsertVector4("Color_FFFFFF", ColorAsVector4((Color)_themeColor_Foreground));
                }
            }
        }

        static Vector4 ColorAsVector4(Color color) => new Vector4(color.R, color.G, color.B, color.A);

        CompositionPropertySet EnsureThemeProperties(Compositor compositor)
        {
            if (_themeProperties == null)
            {
                _themeProperties = compositor.CreatePropertySet();
                _themeProperties.InsertVector4("Color_FF0000", ColorAsVector4((Color)Background));
                _themeProperties.InsertVector4("Color_FFFFFF", ColorAsVector4((Color)Foreground));
            }
            return _themeProperties;
        }

        public Microsoft.UI.Xaml.Controls.IAnimatedVisual TryCreateAnimatedVisual(Compositor compositor)
        {
            object ignored = null;
            return TryCreateAnimatedVisual(compositor, out ignored);
        }

        public Microsoft.UI.Xaml.Controls.IAnimatedVisual TryCreateAnimatedVisual(Compositor compositor, out object diagnostics)
        {
            diagnostics = null;
            EnsureThemeProperties(compositor);

            if (Select_AnimatedVisual.IsRuntimeCompatible())
            {
                var res =
                    new Select_AnimatedVisual(
                        compositor,
                        _themeProperties
                        );
                res.CreateAnimations();
                return res;
            }

            return null;
        }

        /// <summary>
        /// Gets the number of frames in the animation.
        /// </summary>
        public double FrameCount => 20d;

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
            return frameNumber / 20d;
        }

        /// <summary>
        /// Returns a map from marker names to corresponding progress values.
        /// </summary>
        public IReadOnlyDictionary<string, double> Markers =>
            new DictionaryStringDouble
            {
                { "NormalToChecked_Start", 0.0 },
                { "NormalToChecked_End", 1 },
                { "CheckedToNormal_Start", 1 },
                { "CheckedToNormal_End", 0.0 },
            };

        /// <summary>
        /// Sets the color property with the given name, or does nothing if no such property
        /// exists.
        /// </summary>
        public void SetColorProperty(string propertyName, Color value)
        {
            if (propertyName == "Color_FF0000")
            {
                _themeColor_Background = value;
            }
            else if (propertyName == "Color_FFFFFF")
            {
                _themeColor_Foreground = value;
            }
            else
            {
                return;
            }

            if (_themeProperties != null)
            {
                _themeProperties.InsertVector4(propertyName, ColorAsVector4(value));
            }
        }

        /// <summary>
        /// Sets the scalar property with the given name, or does nothing if no such property
        /// exists.
        /// </summary>
        public void SetScalarProperty(string propertyName, double value)
        {
        }

        sealed partial class Select_AnimatedVisual : Microsoft.UI.Xaml.Controls.IAnimatedVisual
        {
            const long c_durationTicks = 2500000;
            readonly Compositor _c;
            readonly ExpressionAnimation _reusableExpressionAnimation;
            readonly CompositionPropertySet _themeProperties;
            CompositionColorBrush _themeColor_Color_FF0000;
            CompositionColorBrush _themeColor_Color_FFFFFF;
            CompositionPathGeometry _pathGeometry_1;
            CompositionSpriteShape _spriteShape_1;
            ContainerVisual _root;
            CubicBezierEasingFunction _cubicBezierEasingFunction_0;
            ExpressionAnimation _rootProgress;
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

            void BindProperty(
                CompositionObject target,
                string animatedPropertyName,
                string expression,
                string referenceParameterName,
                CompositionObject referencedObject)
            {
                _reusableExpressionAnimation.ClearAllParameters();
                _reusableExpressionAnimation.Expression = expression;
                _reusableExpressionAnimation.SetReferenceParameter(referenceParameterName, referencedObject);
                target.StartAnimation(animatedPropertyName, _reusableExpressionAnimation);
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

            CompositionSpriteShape CreateSpriteShape(CompositionGeometry geometry, Matrix3x2 transformMatrix)
            {
                var result = _c.CreateSpriteShape(geometry);
                result.TransformMatrix = transformMatrix;
                return result;
            }

            CompositionSpriteShape CreateSpriteShape(CompositionGeometry geometry, Matrix3x2 transformMatrix, CompositionBrush fillBrush)
            {
                if (_spriteShape_1 != null) { return _spriteShape_1; }
                var result = _spriteShape_1 = _c.CreateSpriteShape(geometry);
                result.TransformMatrix = transformMatrix;
                result.FillBrush = fillBrush;
                return result;
            }

            CanvasGeometry Geometry_0()
            {
                CanvasGeometry result;
                using (var builder = new CanvasPathBuilder(null))
                {
                    builder.BeginFigure(new Vector2(0F, -9.5F));
                    builder.AddCubicBezier(new Vector2(5.24700022F, -9.5F), new Vector2(9.5F, -5.24700022F), new Vector2(9.5F, 0F));
                    builder.AddCubicBezier(new Vector2(9.5F, 5.24700022F), new Vector2(5.24700022F, 9.5F), new Vector2(0F, 9.5F));
                    builder.AddCubicBezier(new Vector2(-5.24700022F, 9.5F), new Vector2(-9.5F, 5.24700022F), new Vector2(-9.5F, 0F));
                    builder.AddCubicBezier(new Vector2(-9.5F, -5.24700022F), new Vector2(-5.24700022F, -9.5F), new Vector2(0F, -9.5F));
                    builder.EndFigure(CanvasFigureLoop.Closed);
                    result = CanvasGeometry.CreatePath(builder);
                }
                return result;
            }

            // - - - Layer aggregator
            // - -  Offset:<12, 12>
            CanvasGeometry Geometry_1()
            {
                CanvasGeometry result;
                using (var builder = new CanvasPathBuilder(null))
                {
                    builder.BeginFigure(new Vector2(4.5F, -3F));
                    builder.AddLine(new Vector2(-1.31799996F, 3F));
                    builder.AddLine(new Vector2(-4.5F, 0F));
                    builder.EndFigure(CanvasFigureLoop.Open);
                    result = CanvasGeometry.CreatePath(builder);
                }
                return result;
            }

            // - Layer aggregator
            // Offset:<12, 12>
            // Color bound to theme property value: Color_FF0000
            CompositionColorBrush ThemeColor_Color_FF0000()
            {
                if (_themeColor_Color_FF0000 != null) { return _themeColor_Color_FF0000; }
                var result = _themeColor_Color_FF0000 = _c.CreateColorBrush();
                BindProperty(_themeColor_Color_FF0000, "Color", "ColorRGB(_theme.Color_FF0000.W,_theme.Color_FF0000.X,_theme.Color_FF0000.Y,_theme.Color_FF0000.Z)", "_theme", _themeProperties);
                return result;
            }

            // Color bound to theme property value: Color_FFFFFF
            CompositionColorBrush ThemeColor_Color_FFFFFF()
            {
                if (_themeColor_Color_FFFFFF != null) { return _themeColor_Color_FFFFFF; }
                var result = _themeColor_Color_FFFFFF = _c.CreateColorBrush();
                BindProperty(_themeColor_Color_FFFFFF, "Color", "ColorRGB(_theme.Color_FFFFFF.W,_theme.Color_FFFFFF.X,_theme.Color_FFFFFF.Y,_theme.Color_FFFFFF.Z)", "_theme", _themeProperties);
                return result;
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 0
            CompositionColorGradientStop GradientStop_0p583_Transparent_0()
            {
                return _c.CreateColorGradientStop(0.583000004F, Color.FromArgb(0x00, 0x00, 0x00, 0x00));
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 1
            CompositionColorGradientStop GradientStop_0p583_Transparent_1()
            {
                return _c.CreateColorGradientStop(0.583000004F, Color.FromArgb(0x00, 0x00, 0x00, 0x00));
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 2
            CompositionColorGradientStop GradientStop_0p688_SemiTransparentBlack_0()
            {
                return _c.CreateColorGradientStop(0.688000023F, Color.FromArgb(0x0C, 0x00, 0x00, 0x00));
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 3
            CompositionColorGradientStop GradientStop_0p688_SemiTransparentBlack_1()
            {
                return _c.CreateColorGradientStop(0.688000023F, Color.FromArgb(0x0C, 0x00, 0x00, 0x00));
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 4
            CompositionColorGradientStop GradientStop_0p793_SemiTransparentBlack_0()
            {
                return _c.CreateColorGradientStop(0.792999983F, Color.FromArgb(0x19, 0x00, 0x00, 0x00));
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 5
            CompositionColorGradientStop GradientStop_0p793_SemiTransparentBlack_1()
            {
                return _c.CreateColorGradientStop(0.792999983F, Color.FromArgb(0x19, 0x00, 0x00, 0x00));
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 6
            CompositionColorGradientStop GradientStop_0p896_SemiTransparentBlack_0()
            {
                return _c.CreateColorGradientStop(0.896000028F, Color.FromArgb(0x0C, 0x00, 0x00, 0x00));
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 7
            CompositionColorGradientStop GradientStop_0p896_SemiTransparentBlack_1()
            {
                return _c.CreateColorGradientStop(0.896000028F, Color.FromArgb(0x0C, 0x00, 0x00, 0x00));
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 8
            CompositionColorGradientStop GradientStop_1_Transparent_0()
            {
                return _c.CreateColorGradientStop(1F, Color.FromArgb(0x00, 0x00, 0x00, 0x00));
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // Stop 9
            CompositionColorGradientStop GradientStop_1_Transparent_1()
            {
                return _c.CreateColorGradientStop(1F, Color.FromArgb(0x00, 0x00, 0x00, 0x00));
            }

            // - Layer aggregator
            // Offset:<12, 12>
            // Ellipse Path 1.EllipseGeometry
            CompositionEllipseGeometry Ellipse_9()
            {
                var result = _c.CreateEllipseGeometry();
                result.Radius = new Vector2(9F, 9F);
                return result;
            }

            CompositionEllipseGeometry PathGeometry_0()
            {
                var result = _c.CreateEllipseGeometry();
                result.Radius = new Vector2(9.5F, 9.5F);
                return result;
            }

            // - Layer aggregator
            // Offset:<12, 12>
            CompositionPathGeometry PathGeometry_1()
            {
                if (_pathGeometry_1 != null) { return _pathGeometry_1; }
                var result = _pathGeometry_1 = _c.CreatePathGeometry(new CompositionPath(Geometry_1()));
                return result;
            }

            // - Layer aggregator
            // Offset:<12, 12>
            CompositionRadialGradientBrush RadialGradientBrush()
            {
                var result = _c.CreateRadialGradientBrush();
                var colorStops = result.ColorStops;
                colorStops.Add(GradientStop_0p583_Transparent_0());
                colorStops.Add(GradientStop_0p583_Transparent_1());
                colorStops.Add(GradientStop_0p688_SemiTransparentBlack_0());
                colorStops.Add(GradientStop_0p688_SemiTransparentBlack_1());
                colorStops.Add(GradientStop_0p793_SemiTransparentBlack_0());
                colorStops.Add(GradientStop_0p793_SemiTransparentBlack_1());
                colorStops.Add(GradientStop_0p896_SemiTransparentBlack_0());
                colorStops.Add(GradientStop_0p896_SemiTransparentBlack_1());
                colorStops.Add(GradientStop_1_Transparent_0());
                colorStops.Add(GradientStop_1_Transparent_1());
                result.MappingMode = CompositionMappingMode.Absolute;
                result.EllipseCenter = new Vector2(-0.0160000008F, 0F);
                result.EllipseRadius = new Vector2(12F, 12F);
                return result;
            }

            // Layer aggregator
            // Path 1
            CompositionSpriteShape SpriteShape_0()
            {
                // Offset:<12, 12>
                var result = CreateSpriteShape(PathGeometry_0(), new Matrix3x2(1F, 0F, 0F, 1F, 12F, 12F)); ;
                result.StrokeBrush = RadialGradientBrush();
                result.StrokeMiterLimit = 2F;
                result.StrokeThickness = 5F;
                return result;
            }

            // Layer aggregator
            // Ellipse Path 1
            CompositionSpriteShape SpriteShape_1()
            {
                // Offset:<12, 12>
                var geometry = Ellipse_9();
                if (_spriteShape_1 != null) { return _spriteShape_1; }
                var result = _spriteShape_1 = CreateSpriteShape(geometry, new Matrix3x2(1F, 0F, 0F, 1F, 12F, 12F), ThemeColor_Color_FF0000()); ;
                return result;
            }

            // Layer aggregator
            // Path 1
            CompositionSpriteShape SpriteShape_2()
            {
                // Offset:<12, 12>
                var result = CreateSpriteShape(PathGeometry_0(), new Matrix3x2(1F, 0F, 0F, 1F, 12F, 12F)); ;
                result.StrokeBrush = ThemeColor_Color_FFFFFF();
                result.StrokeMiterLimit = 2F;
                result.StrokeThickness = 1.5F;
                return result;
            }

            // Layer aggregator
            // Path 1
            CompositionSpriteShape SpriteShape_3()
            {
                // Offset:<12, 12>
                var result = CreateSpriteShape(PathGeometry_1(), new Matrix3x2(1F, 0F, 0F, 1F, 12F, 12F)); ;
                result.StrokeBrush = ThemeColor_Color_FFFFFF();
                result.StrokeDashCap = CompositionStrokeCap.Round;
                result.StrokeStartCap = CompositionStrokeCap.Round;
                result.StrokeEndCap = CompositionStrokeCap.Round;
                result.StrokeLineJoin = CompositionStrokeLineJoin.Round;
                result.StrokeMiterLimit = 2F;
                result.StrokeThickness = 1.5F;
                return result;
            }

            // The root of the composition.
            ContainerVisual Root()
            {
                if (_root != null) { return _root; }
                var result = _root = _c.CreateContainerVisual();
                var propertySet = result.Properties;
                propertySet.InsertScalar("Progress", 0F);
                // Layer aggregator
                result.Children.InsertAtTop(ShapeVisual_0());
                return result;
            }

            CubicBezierEasingFunction CubicBezierEasingFunction_0()
            {
                return (_cubicBezierEasingFunction_0 == null)
                    ? _cubicBezierEasingFunction_0 = _c.CreateCubicBezierEasingFunction(new Vector2(0.600000024F, 0F), new Vector2(0.400000006F, 1F))
                    : _cubicBezierEasingFunction_0;
            }

            ExpressionAnimation RootProgress()
            {
                if (_rootProgress != null) { return _rootProgress; }
                var result = _rootProgress = _c.CreateExpressionAnimation("_.Progress");
                result.SetReferenceParameter("_", _root);
                return result;
            }

            // - - Layer aggregator
            // -  Offset:<12, 12>
            // TrimStart
            ScalarKeyFrameAnimation TrimStartScalarAnimation_1_to_0()
            {
                // Frame 0.
                var result = CreateScalarKeyFrameAnimation(0F, 1F, StepThenHoldEasingFunction());
                // Frame 9.
                result.InsertKeyFrame(0.449999988F, 1F, HoldThenStepEasingFunction());
                // Frame 19.
                result.InsertKeyFrame(0.949999988F, 0F, CubicBezierEasingFunction_0());
                return result;
            }

            // Layer aggregator
            ShapeVisual ShapeVisual_0()
            {
                var result = _c.CreateShapeVisual();
                result.Size = new Vector2(24F, 24F);
                var shapes = result.Shapes;
                // Offset:<12, 12>
                shapes.Add(SpriteShape_0());
                // Offset:<12, 12>
                shapes.Add(SpriteShape_1());
                // Offset:<12, 12>
                shapes.Add(SpriteShape_2());
                // Offset:<12, 12>
                shapes.Add(SpriteShape_3());
                return result;
            }

            StepEasingFunction HoldThenStepEasingFunction()
            {
                if (_holdThenStepEasingFunction != null) { return _holdThenStepEasingFunction; }
                var result = _holdThenStepEasingFunction = _c.CreateStepEasingFunction();
                result.IsFinalStepSingleFrame = true;
                return result;
            }

            StepEasingFunction StepThenHoldEasingFunction()
            {
                if (_stepThenHoldEasingFunction != null) { return _stepThenHoldEasingFunction; }
                var result = _stepThenHoldEasingFunction = _c.CreateStepEasingFunction();
                result.IsInitialStepSingleFrame = true;
                return result;
            }

            // - Layer aggregator
            // Offset:<12, 12>
            // Scale
            Vector2KeyFrameAnimation ScaleVector2Animation()
            {
                // Frame 0.
                var result = CreateVector2KeyFrameAnimation(0F, new Vector2(0F, 0F), StepThenHoldEasingFunction());
                // Frame 1.
                result.InsertKeyFrame(0.0500000007F, new Vector2(0F, 0F), HoldThenStepEasingFunction());
                // Frame 19.
                result.InsertKeyFrame(0.949999988F, new Vector2(1F, 1F), CubicBezierEasingFunction_0());
                return result;
            }

            internal Select_AnimatedVisual(
                Compositor compositor,
                CompositionPropertySet themeProperties
                )
            {
                _c = compositor;
                _themeProperties = themeProperties;
                _reusableExpressionAnimation = compositor.CreateExpressionAnimation();
                Root();
            }

            public Visual RootVisual => _root;
            public TimeSpan Duration => TimeSpan.FromTicks(c_durationTicks);
            public Vector2 Size => new Vector2(24F, 24F);
            void IDisposable.Dispose() => _root?.Dispose();

            public void CreateAnimations()
            {
                StartProgressBoundAnimation(_pathGeometry_1, "TrimStart", TrimStartScalarAnimation_1_to_0(), RootProgress());
                StartProgressBoundAnimation(_spriteShape_1, "Scale", ScaleVector2Animation(), RootProgress());
            }

            public void DestroyAnimations()
            {
                _pathGeometry_1.StopAnimation("TrimStart");
                _spriteShape_1.StopAnimation("Scale");
            }

            internal static bool IsRuntimeCompatible()
            {
                return Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 8);
            }
        }
    }
}
