//
// Copyright Fela Ameghino 2015-2025
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using Telegram.Common;
using Telegram.Controls;
using Telegram.ViewModels.Authorization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Telegram.Views.Authorization
{
    public sealed partial class AuthorizationRegistrationPage : CorePage
    {
        public AuthorizationRegistrationViewModel ViewModel => DataContext as AuthorizationRegistrationViewModel;

        public AuthorizationRegistrationPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.NavigationService.Window.SetTitleBar(TitleBar);
            ViewModel.PropertyChanged += OnPropertyChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.NavigationService.Window.SetTitleBar(null);
            ViewModel.PropertyChanged -= OnPropertyChanged;
        }

        protected override void OnLayoutMetricsChanged(SystemOverlayMetrics metrics)
        {
            Back.HorizontalAlignment = metrics.LeftInset > 0
                ? HorizontalAlignment.Right
                : HorizontalAlignment.Left;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FIRSTNAME_INVALID":
                    VisualUtilities.ShakeView(PrimaryInput);
                    break;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            PrimaryInput.Focus(FocusState.Keyboard);
        }

        private void PrimaryInput_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (sender == PrimaryInput)
                {
                    SecondaryInput.Focus(FocusState.Keyboard);
                }
                else
                {
                    ViewModel.SendCommand.Execute(null);
                }

                e.Handled = true;
            }
        }
    }
}
