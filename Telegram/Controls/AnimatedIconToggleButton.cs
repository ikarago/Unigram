//
// Copyright Fela Ameghino 2015-2024
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Telegram.Controls
{
    public partial class AnimatedIconToggleButton : AnimatedGlyphToggleButton
    {
        public AnimatedIconToggleButton()
        {
            DefaultStyleKey = typeof(AnimatedIconToggleButton);
        }

        protected override bool IsRuntimeCompatible()
        {
            return Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 11);
        }

        #region Source

        public IAnimatedVisualSource2 Source
        {
            get { return (IAnimatedVisualSource2)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(IAnimatedVisualSource2), typeof(AnimatedIconToggleButton), new PropertyMetadata(null));

        #endregion
    }
}
