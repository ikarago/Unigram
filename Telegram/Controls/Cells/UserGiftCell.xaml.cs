using Telegram.Common;
using Telegram.Controls.Media;
using Telegram.Navigation;
using Telegram.Services;
using Telegram.Streams;
using Telegram.Td.Api;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Telegram.Controls.Cells
{
    public sealed partial class UserGiftCell : UserControl
    {
        public UserGiftCell()
        {
            InitializeComponent();
        }

        public void UpdateUserGift(IClientService clientService, UserGift gift)
        {
            StarCountRoot.Visibility = Visibility.Collapsed;

            if (gift.Gift is SentGiftRegular regular)
            {
                if (gift.IsPrivate)
                {
                    Photo.Source = PlaceholderImage.GetGlyph(Icons.AuthorHiddenFilled, 5);
                }
                else if (clientService.TryGetUser(gift.SenderUserId, out User user))
                {
                    Photo.SetUser(clientService, user, 24);
                }

                Photo.Visibility = Visibility.Visible;
                Pattern.Visibility = Visibility.Collapsed;

                Animated.Source = new DelayedFileSource(clientService, regular.Gift.Sticker);

                StarCount.Text = gift.SellStarCount > 0
                    ? gift.SellStarCount.ToString("N0")
                    : regular.Gift.StarCount.ToString("N0");

                if (regular.Gift.TotalCount > 0)
                {
                    RibbonRoot.Visibility = Visibility.Visible;
                    Ribbon.Text = string.Format(Strings.Gift2Limited1OfRibbon, regular.Gift.TotalText());

                    if (RibbonPath.Fill is not LinearGradientBrush)
                    {
                        RibbonPath.Fill = new LinearGradientBrush(new GradientStopCollection
                        {
                            new GradientStop
                            {
                                Color = Color.FromArgb(0xFF, 0x6E, 0xD2, 0xFF),
                                Offset = 0
                            },
                            new GradientStop
                            {
                                Color = Color.FromArgb(0xFF, 0x35, 0xA5, 0xFC),
                                Offset = 1
                            }
                        }, 0);
                    }
                }
                else
                {
                    RibbonRoot.Visibility = Visibility.Collapsed;
                }
            }
            else if (gift.Gift is SentGiftUpgraded upgraded)
            {
                var source = DelayedFileSource.FromSticker(clientService, upgraded.Gift.Symbol.Sticker);
                var centerColor = upgraded.Gift.Backdrop.CenterColor.ToColor();
                var edgeColor = upgraded.Gift.Backdrop.EdgeColor.ToColor();

                Pattern.Update(source, centerColor, edgeColor);

                Photo.Visibility = Visibility.Collapsed;
                Pattern.Visibility = Visibility.Visible;

                Animated.Source = new DelayedFileSource(clientService, upgraded.Gift.Model.Sticker);

                RibbonRoot.Visibility = Visibility.Visible;
                Ribbon.Text = string.Format(Strings.Gift2Limited1OfRibbon, upgraded.Gift.MaxUpgradedCount.ToString("N0"));

                if (RibbonPath.Fill is LinearGradientBrush)
                {
                    RibbonPath.Fill = BootStrapper.Current.Resources["MessageServiceBackgroundBrush"] as Brush;
                }
            }

            if (gift.IsSaved)
            {
                if (Hidden != null)
                {
                    Hidden.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                FindName(nameof(Hidden));
                Hidden.Visibility = Visibility.Visible;
            }
        }

        private readonly Color _ribbonLimitedTop = Color.FromArgb(0xFF, 0x6E, 0xD2, 0xFF);
        private readonly Color _ribbonLimitedBottom = Color.FromArgb(0xFF, 0x35, 0xA5, 0xFC);

        private readonly Color _ribbonSoldOutTop = Color.FromArgb(0xFF, 0xFF, 0x5B, 0x54);
        private readonly Color _ribbonSoldOutBottom = Color.FromArgb(0xFF, 0xED, 0x1D, 0x27);

        public void UpdateGift(IClientService clientService, Gift gift)
        {
            Photo.Visibility = Visibility.Collapsed;

            Animated.Source = new DelayedFileSource(clientService, gift.Sticker);

            StarCount.Text = gift.StarCount.ToString("N0");

            if (gift.TotalCount > 0)
            {
                RibbonRoot.Visibility = Visibility.Visible;
                Ribbon.Text = gift.RemainingCount > 0
                    ? Strings.Gift2LimitedRibbon
                    : Strings.Gift2SoldOut;

                RibbonTop.Color = gift.RemainingCount > 0 ? _ribbonLimitedTop : _ribbonSoldOutTop;
                RibbonBottom.Color = gift.RemainingCount > 0 ? _ribbonLimitedBottom : _ribbonSoldOutBottom;
            }
            else
            {
                RibbonRoot.Visibility = Visibility.Collapsed;
            }

            if (Hidden != null)
            {
                Hidden.Visibility = Visibility.Collapsed;
            }
        }
    }
}
