//
// Copyright Fela Ameghino 2015-2024
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Telegram.Common;
using Telegram.Controls;
using Telegram.Navigation;
using Telegram.Navigation.Services;
using Telegram.Services;
using Telegram.Td.Api;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Hosting;

namespace Telegram.Views.Stars.Popups
{
    public sealed partial class AffiliateProgramPopup : ContentPopup
    {
        private readonly IClientService _clientService;
        private readonly INavigationService _navigationService;
        private readonly FoundAffiliateProgram _program;

        private MessageSender _selectedAlias;

        private readonly ObservableCollection<MessageSender> _items;

        public AffiliateProgramPopup(IClientService clientService, INavigationService navigationService, FoundAffiliateProgram program, MessageSender alias)
        {
            InitializeComponent();

            _clientService = clientService;
            _navigationService = navigationService;

            _program = program;

            _items = new ObservableCollection<MessageSender>();

            InitializeOwnedChats();
            UpdateAlias(alias);

            TableRoot.Visibility = program.Parameters.DailyRevenuePerUserAmount != null
                ? Visibility.Visible
                : Visibility.Collapsed;

            if (clientService.TryGetUser(program.BotUserId, out User botUser) && botUser.Type is UserTypeBot userTypeBot)
            {
                var percent = program.Parameters.Parameters.CommissionPercent();
                var duration = program.Parameters.Parameters.MonthCount > 0
                    ? program.Parameters.Parameters.MonthCount >= 12
                    ? Locale.Declension(Strings.R.ChannelAffiliateProgramJoinText_Years, program.Parameters.Parameters.MonthCount / 12)
                    : Locale.Declension(Strings.R.ChannelAffiliateProgramJoinText_Months, program.Parameters.Parameters.MonthCount)
                    : Strings.ChannelAffiliateProgramJoinText_Lifetime;

                TextBlockHelper.SetMarkdown(Subtitle, string.Format(Strings.ChannelAffiliateProgramJoinText, botUser.FirstName, percent, duration));

                Photo2.SetUser(clientService, botUser, 64);

                MonthlyUsers.Content = userTypeBot.ActiveUserCount;
            }

            StarCount.Text = program.Parameters.DailyRevenuePerUserAmount.ToValue();
        }

        private async void InitializeOwnedChats()
        {
            _items.Add(_clientService.MyId);

            var response1 = await _clientService.SendAsync(new GetOwnedBots());
            if (response1 is Td.Api.Users users)
            {
                foreach (var userId in users.UserIds)
                {
                    _items.Add(new MessageSenderUser(userId));
                }
            }

            var response2 = await _clientService.SendAsync(new GetCreatedPublicChats(new PublicChatTypeHasUsername()));
            if (response2 is Td.Api.Chats chats)
            {
                foreach (var chatId in chats.ChatIds)
                {
                    _items.Add(new MessageSenderChat(chatId));
                }
            }
        }

        private void SettingsFooter_Click(object sender, TextUrlClickEventArgs e)
        {
            MessageHelper.OpenUrl(null, null, Strings.ChannelAffiliateProgramJoinButtonInfoLink);
        }

        private void Alias_Click(object sender, RoutedEventArgs e)
        {
            var flyout = new MenuFlyout();

            void handler(object sender, RoutedEventArgs _)
            {
                if (sender is MenuFlyoutItem item && item.CommandParameter is MessageSender messageSender)
                {
                    item.Click -= handler;
                    UpdateAlias(messageSender);
                }
            }

            foreach (var messageSender in _items)
            {
                var picture = new ProfilePicture();
                picture.Width = 36;
                picture.Height = 36;
                picture.Margin = new Thickness(-4, -2, 0, -2);

                var item = new MenuFlyoutProfile();
                item.Click += handler;
                item.CommandParameter = messageSender;
                item.Style = BootStrapper.Current.Resources["SendAsMenuFlyoutItemStyle"] as Style;
                item.Icon = new FontIcon();
                item.Tag = picture;

                if (_clientService.TryGetUser(messageSender, out User senderUser))
                {
                    picture.SetUser(_clientService, senderUser, 36);

                    item.Text = senderUser.FullName();
                    item.Info = senderUser.Type is UserTypeRegular
                        ? Strings.VoipGroupPersonalAccount
                        : Strings.Bot;
                }
                else if (_clientService.TryGetChat(messageSender, out Chat senderChat))
                {
                    picture.SetChat(_clientService, senderChat, 36);

                    item.Text = senderChat.Title;
                    item.Info = Strings.DiscussChannel;
                }

                flyout.Items.Add(item);
            }

            flyout.ShowAt(Title, FlyoutPlacementMode.Bottom);
        }

        private void UpdateAlias(MessageSender sender)
        {
            _selectedAlias = sender;

            if (_clientService.TryGetUser(sender, out User senderUser))
            {
                Photo1.SetUser(_clientService, senderUser, 64);

                Photo.SetUser(_clientService, senderUser, 28);
                TitleText.Text = senderUser.FullName();
            }
            else if (_clientService.TryGetChat(sender, out Chat senderChat))
            {
                Photo1.SetChat(_clientService, senderChat, 64);

                Photo.SetChat(_clientService, senderChat, 28);
                TitleText.Text = senderChat.Title;
            }
        }
        private bool _submitted;

        private async void Purchase_Click(object sender, RoutedEventArgs e)
        {
            if (_submitted)
            {
                return;
            }

            _submitted = true;

            PurchaseRing.Visibility = Visibility.Visible;

            var visual1 = ElementComposition.GetElementVisual(PurchaseText);
            var visual2 = ElementComposition.GetElementVisual(PurchaseRing);

            ElementCompositionPreview.SetIsTranslationEnabled(PurchaseText, true);
            ElementCompositionPreview.SetIsTranslationEnabled(PurchaseRing, true);

            var translate1 = visual1.Compositor.CreateScalarKeyFrameAnimation();
            translate1.InsertKeyFrame(0, 0);
            translate1.InsertKeyFrame(1, -32);

            var translate2 = visual1.Compositor.CreateScalarKeyFrameAnimation();
            translate2.InsertKeyFrame(0, 32);
            translate2.InsertKeyFrame(1, 0);

            visual1.StartAnimation("Translation.Y", translate1);
            visual2.StartAnimation("Translation.Y", translate2);

            var result = await SubmitAsync();
            if (result)
            {
                return;
            }

            _submitted = false;

            translate1.InsertKeyFrame(0, 32);
            translate1.InsertKeyFrame(1, 0);

            translate2.InsertKeyFrame(0, 0);
            translate2.InsertKeyFrame(1, -32);

            visual1.StartAnimation("Translation.Y", translate1);
            visual2.StartAnimation("Translation.Y", translate2);
        }

        private async Task<bool> SubmitAsync()
        {
            var chatId = 0L;

            if (_selectedAlias is MessageSenderUser senderUser)
            {
                var response1 = await _clientService.SendAsync(new CreatePrivateChat(senderUser.UserId, false));
                if (response1 is Chat chat)
                {
                    chatId = chat.Id;
                }
            }
            else if (_selectedAlias is MessageSenderChat senderChat)
            {
                chatId = senderChat.ChatId;
            }

            if (chatId == 0)
            {
                return false;
            }

            var response = await _clientService.SendAsync(new ConnectChatAffiliateProgram(chatId, _program.BotUserId));
            if (response is ChatAffiliateProgram program)
            {
                Hide();

                var popup = new ConnectedProgramPopup(_clientService, _navigationService, program, _selectedAlias);
                var aggregator = TypeResolver.Current.Resolve<IEventAggregator>(_clientService.SessionId);

                aggregator.Publish(new UpdateChatAffiliatePrograms(chatId));

                void handler(object sender, object args)
                {
                    _navigationService.ShowToast(string.Format("**{0}**\n{1}", Strings.AffiliateProgramJoinedTitle, Strings.AffiliateProgramJoinedText), ToastPopupIcon.Success);
                    popup.Opened -= handler;
                }

                popup.Opened += handler;

                _navigationService.ShowPopup(popup);
                return true;
            }
            else if (response is Error error)
            {
                ToastPopup.ShowError(XamlRoot, error);
            }

            return false;
        }
    }
}
