//
// Copyright Fela Ameghino 2915-2025
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using Microsoft.UI.Xaml.Controls;
using System;
using Telegram.Common;
using Telegram.Controls;
using Telegram.Controls.Media;
using Telegram.Converters;
using Telegram.Navigation;
using Telegram.Navigation.Services;
using Telegram.Services;
using Telegram.Streams;
using Telegram.Td;
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
        private readonly IEventAggregator _aggregator;

        private readonly StarTransaction _transaction;

        private readonly string _transactionId;

        private readonly UserGift _gift;
        private readonly long _userId;

        public UserGiftPopup(IClientService clientService, INavigationService navigationService, UserGift gift, long receiverUserId)
        {
            InitializeComponent();

            _clientService = clientService;
            _navigationService = navigationService;
            _aggregator = TypeResolver.Current.Resolve<IEventAggregator>(clientService.SessionId);

            _gift = gift;
            _userId = receiverUserId;

            if (gift.Gift is SentGiftRegular regular)
            {
                InitializeRegular(clientService, gift, regular.Gift, receiverUserId);
            }
            else if (gift.Gift is SentGiftUpgraded upgraded)
            {
                InitializeUpgraded(clientService, gift, upgraded.Gift, receiverUserId);
            }
        }

        private void InitializeRegular(IClientService clientService, UserGift userGift, Gift gift, long receiverUserId)
        {
            UpgradedHeader.Visibility = Visibility.Collapsed;
            UpgradedTableRoot.Visibility = Visibility.Collapsed;
            UpgradedCaptionRoot.Visibility = Visibility.Collapsed;

            if (clientService.TryGetUser(userGift.SenderUserId, out User user))
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

            if (receiverUserId != clientService.Options.MyId)
            {
                Subtitle.Visibility = Visibility.Collapsed;
                Convert.Visibility = Visibility.Collapsed;
                Status.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (userGift.IsSaved)
                {
                    if (userGift.Date + clientService.Options.GiftSellPeriod > DateTime.Now.ToTimestamp())
                    {
                        TextBlockHelper.SetMarkdown(Subtitle, Strings.Gift2InfoPinned);
                        Convert.Glyph = Locale.Declension(Strings.R.Gift2ButtonSell, userGift.SellStarCount);
                    }
                    else
                    {
                        TextBlockHelper.SetMarkdown(Subtitle, Locale.Declension(Strings.R.Gift2Info2Expired, gift.StarCount));
                        Convert.Visibility = Visibility.Collapsed;
                    }

                    Info.Text = Strings.Gift2ProfileVisible;
                    PurchaseCommand.Content = Strings.Gift2ProfileMakeInvisible;
                }
                else
                {
                    if (userGift.SellStarCount > 0 && userGift.Date + clientService.Options.GiftSellPeriod > DateTime.Now.ToTimestamp())
                    {
                        TextBlockHelper.SetMarkdown(Subtitle, Locale.Declension(Strings.R.Gift2Info, userGift.SellStarCount));
                        Convert.Glyph = Locale.Declension(Strings.R.Gift2ButtonSell, userGift.SellStarCount);
                    }
                    else
                    {
                        TextBlockHelper.SetMarkdown(Subtitle, Locale.Declension(Strings.R.Gift2Info2Expired, gift.StarCount));
                        Convert.Visibility = Visibility.Collapsed;
                    }

                    Info.Text = Strings.Gift2ProfileInvisible;
                    PurchaseCommand.Content = Strings.Gift2ProfileMakeVisible;
                }

                if (userGift.CanBeUpgraded && userGift.PrepaidUpgradeStarCount > 0)
                {
                    TextBlockHelper.SetMarkdown(Subtitle, Strings.Gift2InfoInFreeUpgrade);

                    PurchaseCommand.Content = Strings.Gift2UpgradeButtonFree;
                }

                Info.Visibility = Visibility.Visible;

                VisibilityRoot.Visibility = Visibility.Visible;
                VisibilityText.Text = userGift.IsSaved
                    ? Strings.Gift2Visible
                    : Strings.Gift2Invisible;
                Toggle.Glyph = userGift.IsSaved
                    ? Strings.Gift2VisibleHide
                    : Strings.Gift2InvisibleShow;
            }

            AnimatedPhoto.LoopCount = 0;

            AnimatedPhoto.Source = new DelayedFileSource(clientService, gift.Sticker);

            StarCount.Text = gift.StarCount.ToString("N0");

            if (gift.TotalCount > 0)
            {
                Availability.Visibility = Visibility.Visible;
                Availability.Content = gift.RemainingText();
            }

            if (userGift.CanBeUpgraded)
            {
                Status.Visibility = Visibility.Visible;
            }

            Date.Content = Formatter.DateAt(userGift.Date);

            if (userGift.Text?.Text.Length > 0)
            {
                TableRoot.BorderThickness = new Thickness(1, 1, 1, 0);
                TableRoot.CornerRadius = new CornerRadius(4, 4, 0, 0);

                CaptionRoot.Visibility = Visibility.Visible;
                Caption.SetText(clientService, userGift.Text);
            }
        }

        private void InitializeUpgraded(IClientService clientService, UserGift userGift, UpgradedGift gift, long receiverUserId)
        {
            Header.Visibility = Visibility.Collapsed;
            TableRoot.Visibility = Visibility.Collapsed;
            CaptionRoot.Visibility = Visibility.Collapsed;

            var source = DelayedFileSource.FromSticker(clientService, gift.Symbol.Sticker);
            var centerColor = gift.Backdrop.CenterColor.ToColor();
            var edgeColor = gift.Backdrop.EdgeColor.ToColor();

            UpgradedHeader.Update(source, centerColor, edgeColor);
            UpgradedAnimatedPhoto.Source = DelayedFileSource.FromSticker(clientService, gift.Model.Sticker);
            UpgradedTitle.Text = gift.Title;
            UpgradedSubtitle.Text = Locale.Declension(Strings.R.Gift2CollectionNumber, gift.Number);

            if (clientService.TryGetUser(gift.OwnerUserId, out User user))
            {
                UpgradedFromPhoto.SetUser(clientService, user, 24);
                UpgradedFromPhoto.Visibility = Visibility.Visible;
                UpgradedFromTitle.Text = user.FullName();
            }
            else
            {
                FromPhoto.Source = PlaceholderImage.GetGlyph(Icons.AuthorHiddenFilled, 5);
                FromPhoto.Visibility = Visibility.Visible;
                FromTitle.Text = Strings.StarsTransactionHidden;
            }

            From.Header = Strings.Gift2From;
            Title.Text = Strings.Gift2TitleReceived;

            UpgradedModel.Text = gift.Model.Name;
            UpgradedModelRarity.Glyph = (gift.Model.RarityPerMille / 10d).ToString("0.##") + "%";
            UpgradedBackdrop.Text = gift.Backdrop.Name;
            UpgradedBackdropRarity.Glyph = (gift.Backdrop.RarityPerMille / 10d).ToString("0.##") + "%";
            UpgradedSymbol.Text = gift.Symbol.Name;
            UpgradedSymbolRarity.Glyph = (gift.Symbol.RarityPerMille / 10d).ToString("0.##") + "%";

            UpgradedQuantity.Content =
                Locale.Declension(Strings.R.Gift2QuantityIssued1, gift.TotalUpgradedCount) +
                Locale.Declension(Strings.R.Gift2QuantityIssued2, gift.MaxUpgradedCount);

            if (gift.OriginalDetails != null)
            {
                UpgradedTableRoot.BorderThickness = new Thickness(1, 1, 1, 0);
                UpgradedTableRoot.CornerRadius = new CornerRadius(4, 4, 0, 0);

                UpgradedCaptionRoot.Visibility = Visibility.Visible;

                var sender = clientService.GetUser(gift.OriginalDetails.SenderUserId);
                var receiver = clientService.GetUser(gift.OriginalDetails.ReceiverUserId);

                var senderName = sender?.FullName();
                var senderText = senderName != null
                    ? new FormattedText(senderName, new[] { new TextEntity(0, senderName.Length, new TextEntityTypeMentionName(sender.Id)) })
                    : null;

                var receiverName = receiver?.FullName();
                var receiverText = receiverName != null
                    ? new FormattedText(receiverName, new[] { new TextEntity(0, receiverName.Length, new TextEntityTypeMentionName(receiver.Id)) })
                    : null;

                var date = Formatter.Date(gift.OriginalDetails.Date);
                var dateText = date.AsFormattedText();

                FormattedText text = null;

                if (gift.OriginalDetails.Text.Text.Length > 0)
                {
                    if (sender != null && receiver != null)
                    {
                        text = ClientEx.Format(Strings.Gift2AttributeOriginalDetailsComment, senderText, receiverText, dateText, gift.OriginalDetails.Text);
                    }
                    else if (sender != null)
                    {
                        text = ClientEx.Format(Strings.Gift2AttributeOriginalDetailsSelfComment, senderText, dateText, gift.OriginalDetails.Text);
                    }
                    else if (receiver != null)
                    {
                        text = ClientEx.Format(Strings.Gift2AttributeOriginalDetailsNoSenderComment, receiverText, dateText, gift.OriginalDetails.Text);
                    }
                }
                else if (sender != null && receiver != null)
                {
                    text = ClientEx.Format(Strings.Gift2AttributeOriginalDetails, senderText, receiverText, dateText);
                }
                else if (sender != null)
                {
                    text = ClientEx.Format(Strings.Gift2AttributeOriginalDetailsSelf, senderText, dateText);
                }
                else if (receiver != null)
                {
                    text = ClientEx.Format(Strings.Gift2AttributeOriginalDetailsNoSender, receiverText, dateText);
                }

                UpgradedCaption.SetText(clientService, text);
            }

            if (gift.OwnerUserId == clientService.Options.MyId)
            {
                if (userGift.IsSaved)
                {
                    Info.Text = Strings.Gift2ProfileVisible;
                    PurchaseCommand.Content = Strings.Gift2ProfileMakeInvisible;
                }
                else
                {
                    Info.Text = Strings.Gift2ProfileInvisible;
                    PurchaseCommand.Content = Strings.Gift2ProfileMakeVisible;
                }
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

            if (_gift.PrepaidUpgradeStarCount > 0 && _gift.Gift is not SentGiftUpgraded)
            {
                _navigationService.ShowPopup(new UpgradeGiftPopup(_clientService, _navigationService, _gift, _userId));
            }
            else
            {
                Toggle_Click(sender, e);
            }
        }

        private async void ShareLink_Click(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            Hide();
            await _navigationService.ShowPopupAsync(new ChooseChatsPopup(), new ChooseChatsConfigurationPostLink(new HttpUrl("https://")));
        }

        private async void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (_gift?.Gift is SentGiftRegular regular && _clientService.TryGetUser(_gift.SenderUserId, out User user))
            {
                var expiration = Formatter.ToLocalTime(_gift.Date + _clientService.Options.GiftSellPeriod);
                var diff = expiration - DateTime.Now;

                var message = Locale.Declension(Strings.R.Gift2ConvertText2, (long)diff.TotalDays, user.FirstName, Locale.Declension(Strings.R.StarsCount, regular.Gift.StarCount));

                var confirm = await MessagePopup.ShowAsync(XamlRoot, target: null, message, Strings.Gift2ConvertTitle, Strings.Gift2ConvertButton, Strings.Cancel);
                if (confirm == ContentDialogResult.Primary)
                {
                    var response = await _clientService.SendAsync(new SellGift(user.Id, _gift.MessageId));
                    if (response is Ok)
                    {
                        Hide(ContentDialogResult.Secondary);

                        _aggregator.Publish(new UpdateGiftIsSold(_gift.SenderUserId, _gift.MessageId));
                        _navigationService.Navigate(typeof(StarsPage));

                        ToastPopup.Show(XamlRoot, string.Format("**{0}**\n{1}", Strings.Gift2ConvertedTitle, Locale.Declension(Strings.R.Gift2Converted, regular.Gift.StarCount)), ToastPopupIcon.StarsTopup);
                    }
                }
            }
        }

        private void Upgrade_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            _navigationService.ShowPopup(new UpgradeGiftPopup(_clientService, _navigationService, _gift, _userId));
        }

        private async void Toggle_Click(object sender, RoutedEventArgs e)
        {
            var response = await _clientService.SendAsync(new ToggleGiftIsSaved(_gift.SenderUserId, _gift.MessageId, !_gift.IsSaved));
            if (response is Ok)
            {
                _gift.IsSaved = !_gift.IsSaved;
                _aggregator.Publish(new UpdateGiftIsSaved(_gift.SenderUserId, _gift.MessageId, _gift.IsSaved));

                if (_gift.IsSaved)
                {
                    ToastPopup.Show(XamlRoot, string.Format("**{0}**\n{1}", Strings.Gift2MadePublicTitle, Strings.Gift2MadePublic), new DelayedFileSource(_clientService, _gift.GetSticker()));
                }
                else
                {
                    ToastPopup.Show(XamlRoot, string.Format("**{0}**\n{1}", Strings.Gift2MadePrivateTitle, Strings.Gift2MadePrivate), new DelayedFileSource(_clientService, _gift.GetSticker()));
                }
            }
        }

        private void UpgradedModelRarity_Click(object sender, RoutedEventArgs e)
        {
            if (_gift.Gift is SentGiftUpgraded upgraded)
            {
                ToastPopup.Show(UpgradedModelRarity, string.Format(Strings.Gift2RarityHint, (upgraded.Gift.Model.RarityPerMille / 10d).ToString("0.##") + "%"), TeachingTipPlacementMode.Top);
            }
        }

        private void UpgradedBackdropRarity_Click(object sender, RoutedEventArgs e)
        {
            if (_gift.Gift is SentGiftUpgraded upgraded)
            {
                ToastPopup.Show(UpgradedBackdropRarity, string.Format(Strings.Gift2RarityHint, (upgraded.Gift.Backdrop.RarityPerMille / 10d).ToString("0.##") + "%"), TeachingTipPlacementMode.Top);
            }
        }

        private void UpgradedSymbolRarity_Click(object sender, RoutedEventArgs e)
        {
            if (_gift.Gift is SentGiftUpgraded upgraded)
            {
                ToastPopup.Show(UpgradedSymbolRarity, string.Format(Strings.Gift2RarityHint, (upgraded.Gift.Symbol.RarityPerMille / 10d).ToString("0.##") + "%"), TeachingTipPlacementMode.Top);
            }
        }
    }
}
