//
// Copyright Fela Ameghino 2015-2024
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using Rg.DiffUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Collections;
using Telegram.Common;
using Telegram.Controls;
using Telegram.Controls.Cells;
using Telegram.Navigation.Services;
using Telegram.Services;
using Telegram.Td.Api;
using Telegram.Views.Stars.Popups;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media.Animation;

namespace Telegram.Views.Premium.Popups
{
    public partial class GiftGroup : KeyedList<GiftPopup.GiftGroupType, Gift>
    {
        public GiftGroup(GiftPopup.GiftGroupType key, IEnumerable<Gift> source)
            : base(key, source)
        {
            if (key == GiftPopup.GiftGroupType.StarCount)
            {
                StarCount = this[0].StarCount;
            }
        }

        public long StarCount { get; }
    }

    public sealed partial class GiftPopup : ContentPopup
    {
        private readonly IClientService _clientService;
        private readonly INavigationService _navigationService;

        private readonly long _userId;

        private readonly DiffObservableCollection<Gift> _gifts = new(Constants.DiffOptions);

        public GiftPopup(IClientService clientService, INavigationService navigationService, User user, UserFullInfo fullInfo)
        {
            InitializeComponent();

            _clientService = clientService;
            _navigationService = navigationService;

            _userId = user.Id;

            Photo.SetUser(clientService, user, 96);

            TextBlockHelper.SetMarkdown(PremiumInfo, string.Format(Strings.Gift2PremiumInfo, user.FirstName));
            TextBlockHelper.SetMarkdown(StarsInfo, string.Format(Strings.Gift2StarsInfo, user.FirstName));

            AddLink(PremiumInfo, Strings.Gift2PremiumInfoLink, PremiumInfoLink_Click);
            AddLink(StarsInfo, Strings.Gift2StarsInfoLink, StarsInfoLink_Click);

            ScrollingHost.ItemsSource = _gifts;

            InitializeOptions(clientService);
            InitializeGifts(clientService, fullInfo);
        }

        private void AddLink(TextBlock block, string text, TypedEventHandler<Hyperlink, HyperlinkClickEventArgs> handler)
        {
            var hyperlink = new Hyperlink();
            hyperlink.UnderlineStyle = UnderlineStyle.None;
            hyperlink.Inlines.Add(text);
            hyperlink.Click += handler;

            block.Inlines.Add(" ");
            block.Inlines.Add(hyperlink);
        }

        private void PremiumInfoLink_Click(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            Hide();
            _navigationService.ShowPromo();
        }

        private void StarsInfoLink_Click(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            // TODO: stars promo
        }

        public enum GiftGroupType
        {
            All,
            Limited,
            StarCount
        }

        private async void InitializeOptions(IClientService clientService)
        {
            PremiumOptions.ItemsSource = new[]
            {
                new PremiumGiftCodePaymentOption(string.Empty, 0, 0, 0, 0, string.Empty, 0, null),
                new PremiumGiftCodePaymentOption(string.Empty, 0, 0, 0, 0, string.Empty, 0, null),
                new PremiumGiftCodePaymentOption(string.Empty, 0, 0, 0, 0, string.Empty, 0, null),
            };

            var response = await clientService.SendAsync(new GetPremiumGiftCodePaymentOptions(0));
            if (response is PremiumGiftCodePaymentOptions options)
            {
                PremiumOptions.ItemsSource = options.Options
                    .Where(x => x.WinnerCount == 1)
                    .OrderBy(x => x.MonthCount)
                    .ToList();
            }
        }

        private async void InitializeGifts(IClientService clientService, UserFullInfo fullInfo)
        {
            var response = await clientService.SendAsync(new GetAvailableGifts());
            if (response is Gifts gifts)
            {
                var all = new List<Gift>();
                var remaining = new List<Gift>();

                var today = fullInfo.Birthdate?.Day == DateTime.Today.Day && fullInfo.Birthdate?.Month == DateTime.Today.Month;

                foreach (var gift in gifts.GiftsValue)
                {
                    if (gift.IsForBirthday && today)
                    {
                        all.Add(gift);
                    }
                    else
                    {
                        remaining.Add(gift);
                    }
                }

                all.AddRange(remaining);

                var navigation = new List<GiftGroup>();
                navigation.Add(new GiftGroup(GiftGroupType.All, all));

                if (all.Any(x => x.TotalCount > 0))
                {
                    navigation.Add(new GiftGroup(GiftGroupType.Limited, all.Where(x => x.TotalCount > 0)));
                }

                var groups = all
                    .GroupBy(x => x.StarCount)
                    .OrderBy(x => x.Key);

                foreach (var group in groups)
                {
                    navigation.Add(new GiftGroup(GiftGroupType.StarCount, group));
                }

                Navigation.ItemsSource = navigation;
                Navigation.SelectedIndex = 0;
            }
        }

        private async void OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Gift gift)
            {
                if (gift.TotalCount > 0 && gift.RemainingCount == 0)
                {
                    Hide();
                    await _navigationService.ShowPopupAsync(new Views.Stars.Popups.UserGiftPopup(_clientService, _navigationService, gift));
                    await this.ShowQueuedAsync(XamlRoot);
                }
                else
                {
                    Hide();
                    await _clientService.SendAsync(new CreatePrivateChat(_clientService.Options.MyId, true));
                    await _navigationService.ShowPopupAsync(new SendGiftPopup(_clientService, _navigationService, gift, _userId));
                }
            }
            else if (e.ClickedItem is PremiumGiftCodePaymentOption option)
            {
                Hide();
                await _navigationService.ShowPopupAsync(new SendGiftPopup(_clientService, _navigationService, option, _userId));
            }
        }

        private void OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue)
            {
                return;
            }
            else if (args.ItemContainer.ContentTemplateRoot is UserGiftCell userGiftCell && args.Item is Gift gift)
            {
                userGiftCell.UpdateGift(_clientService, gift);
            }
            else if (args.ItemContainer.ContentTemplateRoot is PremiumGiftCell premiumGiftCell && args.Item is PremiumGiftCodePaymentOption option)
            {
                premiumGiftCell.UpdatePremiumGift(_clientService, option);
            }

            args.Handled = true;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Navigation.SelectedItem is GiftGroup group)
            {
                _gifts.ReplaceDiff(group);
                return;

                ScrollingHost.ItemContainerTransitions.Clear();

                var diffResult = DiffUtil.CalculateDiff(_gifts, group, _gifts.DefaultDiffHandler, _gifts.DefaultOptions);
                if (diffResult.MovedItems.Count == 0)
                {
                    ScrollingHost.ItemContainerTransitions.Add(new AddDeleteThemeTransition());
                }

                ScrollingHost.ItemContainerTransitions.Add(new RepositionThemeTransition());

                _gifts.ReplaceDiff(diffResult);
            }
        }

        public static Visibility ConvertGiftGroupStartCountVisibility(GiftGroupType type)
        {
            return type == GiftGroupType.StarCount
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public static string ConvertGiftGroupStarCountText(GiftGroupType type, long starCount)
        {
            return type switch
            {
                GiftGroupType.All => Strings.Gift2TabAll,
                GiftGroupType.Limited => Strings.Gift2TabLimited,
                _ => starCount.ToString("N0")
            };
        }
    }
}
