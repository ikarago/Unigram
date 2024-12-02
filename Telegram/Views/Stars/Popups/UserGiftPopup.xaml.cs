//
// Copyright Fela Ameghino 2015-2024
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using System;
using Telegram.Common;
using Telegram.Controls;
using Telegram.Controls.Media;
using Telegram.Converters;
using Telegram.Navigation;
using Telegram.Navigation.Services;
using Telegram.Services;
using Telegram.Streams;
using Telegram.Td.Api;
using Telegram.Views.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;

namespace Telegram.Views.Stars.Popups
{
    public sealed partial class UserGiftPopup : ContentPopup
    {
        private readonly IClientService _clientService;
        private readonly INavigationService _navigationService;

        private readonly StarTransaction _transaction;

        private readonly string _transactionId;

        private readonly UserGift _gift;

        public UserGiftPopup(IClientService clientService, INavigationService navigationService, UserGift gift, long userId)
        {
            InitializeComponent();

            _clientService = clientService;
            _navigationService = navigationService;

            _gift = gift;

            if (clientService.TryGetUser(gift.SenderUserId, out User user))
            {
                FromPhoto.SetUser(clientService, user, 24);
                FromPhoto.Visibility = Visibility.Visible;
                FromTitle.Text = user.FullName();
            }
            else
            {
                FromPhoto.Source = PlaceholderImage.GetGlyph(Icons.AuthorHiddenFilled, 5);
                FromPhoto.Visibility = Visibility.Visible;
                FromTitle.Text = Strings.StarsTransactionHidden;
            }

            From.Header = Strings.Gift2From;
            Title.Text = Strings.Gift2TitleReceived;

            if (userId != clientService.Options.MyId)
            {
                Subtitle.Visibility = Visibility.Collapsed;
                Convert.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (gift.IsSaved)
                {
                    if (gift.Date + clientService.Options.GiftSellPeriod > DateTime.Now.ToTimestamp())
                    {
                        TextBlockHelper.SetMarkdown(Subtitle, Strings.Gift2InfoPinned);
                        Convert.Glyph = Locale.Declension(Strings.R.Gift2ButtonSell, gift.SellStarCount);
                    }
                    else
                    {
                        TextBlockHelper.SetMarkdown(Subtitle, Locale.Declension(Strings.R.Gift2Info2Expired, gift.Gift.StarCount));
                        Convert.Visibility = Visibility.Collapsed;
                    }

                    Info.Text = Strings.Gift2ProfileVisible;
                    PurchaseCommand.Content = Strings.Gift2ProfileMakeInvisible;
                }
                else
                {
                    if (gift.SellStarCount > 0 && gift.Date + clientService.Options.GiftSellPeriod > DateTime.Now.ToTimestamp())
                    {
                        TextBlockHelper.SetMarkdown(Subtitle, Locale.Declension(Strings.R.Gift2Info, gift.SellStarCount));
                        Convert.Glyph = Locale.Declension(Strings.R.Gift2ButtonSell, gift.SellStarCount);
                    }
                    else
                    {
                        TextBlockHelper.SetMarkdown(Subtitle, Locale.Declension(Strings.R.Gift2Info2Expired, gift.Gift.StarCount));
                        Convert.Visibility = Visibility.Collapsed;
                    }

                    Info.Text = Strings.Gift2ProfileInvisible;
                    PurchaseCommand.Content = Strings.Gift2ProfileMakeVisible;
                }

                Info.Visibility = Visibility.Visible;
            }

            AnimatedPhoto.LoopCount = 0;
            AnimatedPhoto.Source = new DelayedFileSource(clientService, gift.Gift.Sticker);

            Date.Content = Formatter.DateAt(gift.Date);

            StarCount.Text = gift.Gift.StarCount.ToString("N0");

            if (gift.Gift.TotalCount > 0)
            {
                Availability.Visibility = Visibility.Visible;
                Availability.Content = gift.Gift.RemainingText();
            }

            if (gift.Text?.Text.Length > 0)
            {
                TableRoot.BorderThickness = new Thickness(1, 1, 1, 0);
                TableRoot.CornerRadius = new CornerRadius(4, 4, 0, 0);

                CaptionRoot.Visibility = Visibility.Visible;
                Caption.SetText(clientService, gift.Text);
            }
        }

        public UserGiftPopup(IClientService clientService, INavigationService navigationService, Gift gift)
        {
            InitializeComponent();

            _clientService = clientService;
            _navigationService = navigationService;

            From.Visibility = Visibility.Collapsed;

            Title.Text = Strings.Gift2SoldOutSheetTitle;
            Subtitle.Text = Strings.Gift2SoldOutSheetSubtitle;
            Subtitle.Foreground = BootStrapper.Current.Resources["SystemFillColorCriticalBrush"] as Brush;

            FirstSale.Visibility = Visibility.Visible;
            LastSale.Visibility = Visibility.Visible;

            FirstSale.Content = Formatter.DateAt(gift.FirstSendDate);
            LastSale.Content = Formatter.DateAt(gift.LastSendDate);

            Convert.Visibility = Visibility.Collapsed;
            Info.Visibility = Visibility.Collapsed;

            AnimatedPhoto.LoopCount = 0;
            AnimatedPhoto.Source = new DelayedFileSource(clientService, gift.Sticker);

            Date.Visibility = Visibility.Collapsed;

            StarCount.Text = gift.StarCount.ToString("N0");

            Availability.Visibility = Visibility.Visible;
            Availability.Content = gift.RemainingText();

            PurchaseCommand.Content = Strings.OK;
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            Hide(ContentDialogResult.Primary);
        }

        private async void ShareLink_Click(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            Hide();
            await _navigationService.ShowPopupAsync(new ChooseChatsPopup(), new ChooseChatsConfigurationPostLink(new HttpUrl("https://")));
        }

        private async void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (_clientService.TryGetUser(_gift.SenderUserId, out User user))
            {
                var expiration = Formatter.ToLocalTime(_gift.Date + _clientService.Options.GiftSellPeriod);
                var diff = expiration - DateTime.Now;

                var message = Locale.Declension(Strings.R.Gift2ConvertText2, (long)diff.TotalDays, user.FirstName, Locale.Declension(Strings.R.StarsCount, _gift.Gift.StarCount));

                var confirm = await MessagePopup.ShowAsync(XamlRoot, target: null, message, Strings.Gift2ConvertTitle, Strings.Gift2ConvertButton, Strings.Cancel);
                if (confirm == ContentDialogResult.Primary)
                {
                    var response = await _clientService.SendAsync(new SellGift(user.Id, _gift.MessageId));
                    if (response is Ok)
                    {
                        Hide(ContentDialogResult.Secondary);

                        _navigationService.Navigate(typeof(StarsPage));
                        ToastPopup.Show(XamlRoot, string.Format("**{0}**\n{1}", Strings.Gift2ConvertedTitle, Locale.Declension(Strings.R.Gift2Converted, _gift.Gift.StarCount)), ToastPopupIcon.StarsTopup);
                    }
                }
            }
        }
    }
}
