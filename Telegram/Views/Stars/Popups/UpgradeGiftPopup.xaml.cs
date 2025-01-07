//
// Copyright Fela Ameghino 2915-2025
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.UI.Xaml.Media;
using System;
using System.Linq;
using System.Numerics;
using Telegram.Common;
using Telegram.Controls;
using Telegram.Converters;
using Telegram.Navigation;
using Telegram.Navigation.Services;
using Telegram.Services;
using Telegram.Streams;
using Telegram.Td.Api;
using Telegram.Views.Popups;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Point = Windows.Foundation.Point;

namespace Telegram.Views.Stars.Popups
{
    public sealed partial class UpgradeGiftPopup : ContentPopup
    {
        private readonly IClientService _clientService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _aggregator;

        private readonly StarTransaction _transaction;

        private readonly string _transactionId;

        private readonly UserGift _gift;

        private GiftUpgradePreview _preview;
        private int _index;

        private bool _ready1;
        private bool _ready2;

        public UpgradeGiftPopup(IClientService clientService, INavigationService navigationService, UserGift gift, long userId)
        {
            InitializeComponent();

            _clientService = clientService;
            _navigationService = navigationService;
            _aggregator = TypeResolver.Current.Resolve<IEventAggregator>(clientService.SessionId);

            _gift = gift;

            Animated1.LoopCount = 1;
            Animated2.LoopCount = 1;
            Animated1.AutoPlay = false;
            Animated2.AutoPlay = false;
            Animated1.LoopCompleted += OnLoopCompleted;

            InitializeGift();
        }

        private void OnReady(object sender, EventArgs e)
        {
            if (sender == Animated1)
            {
                _ready1 = true;
            }
            else if (sender == Animated2)
            {
                _ready2 = true;
            }

            if (_ready1 && _ready2)
            {
                Animated1.Play();
                Animated2.Play();
            }
        }

        private void OnLoopCompleted(object sender, AnimatedImageLoopCompletedEventArgs e)
        {
            _ready1 = false;
            _ready2 = false;

            this.BeginOnUIThread(UpdateGift);
        }

        private async void InitializeGift()
        {
            var gift = _gift.Gift as SentGiftRegular;

            var response = await _clientService.SendAsync(new GetGiftUpgradePreview(gift.Gift.Id));
            if (response is GiftUpgradePreview preview)
            {
                foreach (var item in preview.Models.Reverse())
                {
                    _clientService.DownloadFile(item.Sticker.StickerValue.Id, 32);
                }

                foreach (var item in preview.Symbols.Reverse())
                {
                    _clientService.DownloadFile(item.Sticker.StickerValue.Id, 31);
                }

                _preview = preview;

                UpdateGift();
            }
        }

        private void UpdateGift()
        {
            var model = _preview.Models[_index % _preview.Models.Count];
            var symbol = _preview.Symbols[_index % _preview.Symbols.Count];
            var backdrop = _preview.Backdrops[_index % _preview.Backdrops.Count];

            _index++;

            Identity.Foreground = new SolidColorBrush(Colors.White);
            BotVerified.ReplacementColor = new SolidColorBrush(Colors.White);

            HeaderRoot.RequestedTheme = ElementTheme.Dark;

            var gradient = new LinearGradientBrush();
            gradient.StartPoint = new Point(0, 0);
            gradient.EndPoint = new Point(0, 1);
            gradient.GradientStops.Add(new GradientStop
            {
                Color = backdrop.CenterColor.ToColor(),
                Offset = 0
            });

            gradient.GradientStops.Add(new GradientStop
            {
                Color = backdrop.EdgeColor.ToColor(),
                Offset = 1
            });

            HeaderRoot.Background = gradient;

            Pattern.Source = new DelayedFileSource(_clientService, symbol.Sticker);
            Animated1.Source = new DelayedFileSource(_clientService, model.Sticker);
            Animated2.Source = new DelayedFileSource(_clientService, _preview.Models[_index % _preview.Models.Count].Sticker);

            var compositor = BootStrapper.Current.Compositor;

            // Create a VisualSurface positioned at the same location as this control and feed that
            // through the color effect.
            var surfaceBrush = compositor.CreateSurfaceBrush();
            surfaceBrush.Stretch = CompositionStretch.None;
            var surface = compositor.CreateVisualSurface();

            // Select the source visual and the offset/size of this control in that element's space.
            surface.SourceVisual = ElementComposition.GetElementVisual(Pattern);
            surface.SourceOffset = new Vector2(0, 0);
            surface.SourceSize = new Vector2(1000, 320);
            surfaceBrush.Surface = surface;
            surfaceBrush.Stretch = CompositionStretch.None;

            CompositionBrush brush;
            var linear = compositor.CreateLinearGradientBrush();
            linear.StartPoint = new Vector2();
            linear.EndPoint = new Vector2(0, 1);
            linear.ColorStops.Add(compositor.CreateColorGradientStop(0, backdrop.CenterColor.ToColor()));
            linear.ColorStops.Add(compositor.CreateColorGradientStop(1, backdrop.EdgeColor.ToColor()));

            brush = linear;

            var radial = compositor.CreateRadialGradientBrush();
            //radial.CenterPoint = new Vector2(0.5f, 0.0f);
            radial.EllipseCenter = new Vector2(0.5f, 0.3f);
            radial.EllipseRadius = new Vector2(0.4f, 0.6f);
            radial.ColorStops.Add(compositor.CreateColorGradientStop(0, Color.FromArgb(200, 0, 0, 0)));
            radial.ColorStops.Add(compositor.CreateColorGradientStop(1, Color.FromArgb(0, 0, 0, 0)));

            var blend = new BlendEffect
            {
                Background = new CompositionEffectSourceParameter("Background"),
                Foreground = new CompositionEffectSourceParameter("Foreground"),
                Mode = BlendEffectMode.SoftLight
            };

            var borderEffectFactory = BootStrapper.Current.Compositor.CreateEffectFactory(blend);
            var borderEffectBrush = borderEffectFactory.CreateBrush();
            borderEffectBrush.SetSourceParameter("Foreground", brush);
            borderEffectBrush.SetSourceParameter("Background", radial); // compositor.CreateColorBrush(Color.FromArgb(80, 0x00, 0x00, 0x00)));

            CompositionMaskBrush maskBrush = compositor.CreateMaskBrush();
            maskBrush.Source = borderEffectBrush; // Set source to content that is to be masked 
            maskBrush.Mask = surfaceBrush; // Set mask to content that is the opacity mask 

            var visual = compositor.CreateSpriteVisual();
            visual.Size = new Vector2(1000, 320);
            visual.Offset = new Vector3(0, 0, 0);
            visual.Brush = maskBrush;

            ElementCompositionPreview.SetElementChildVisual(HeaderGlow, visual);

            var radial2 = new RadialGradientBrush();
            //radial.CenterPoint = new Vector2(0.5f, 0.0f);
            radial2.Center = new Point(0.5f, 0.3f);
            radial2.RadiusX = 0.4;
            radial2.RadiusY = 0.6;
            radial2.GradientStops.Add(new GradientStop { Color = Color.FromArgb(50, 255, 255, 255) });
            radial2.GradientStops.Add(new GradientStop { Color = Color.FromArgb(0, 255, 255, 255), Offset = 1 });

            HeaderGlow.Background = radial2;
        }

        private async void Purchase_Click(object sender, RoutedEventArgs e)
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
            if (_gift.Gift is SentGiftRegular regular && _clientService.TryGetUser(_gift.SenderUserId, out User user))
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
    }
}
