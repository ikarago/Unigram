//
// Copyright Fela Ameghino 2015-2024
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using System.Collections.ObjectModel;
using Telegram.Common;
using Telegram.Controls;
using Telegram.Controls.Media;
using Telegram.Navigation;
using Telegram.Navigation.Services;
using Telegram.Services;
using Telegram.Td.Api;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Telegram.Views.Stars.Popups
{
    public sealed partial class ConnectedProgramPopup : ContentPopup
    {
        private readonly IClientService _clientService;
        private readonly INavigationService _navigationService;
        private readonly ChatAffiliateProgram _program;

        private MessageSender _selectedAlias;

        private readonly ObservableCollection<MessageSender> _items;

        public ConnectedProgramPopup(IClientService clientService, INavigationService navigationService, ChatAffiliateProgram program, MessageSender alias)
        {
            InitializeComponent();

            _clientService = clientService;
            _navigationService = navigationService;

            _program = program;

            _clientService.Send(new GetUserFullInfo(program.BotUserId));
            _items = new ObservableCollection<MessageSender>();

            InitializeOwnedChats();

            Photo1.Source = PlaceholderImage.GetGlyph(Icons.LinkDiagonal);

            Link.Text = program.Url.Replace("https://", string.Empty);

            _selectedAlias = alias;

            if (_clientService.TryGetUser(alias, out User senderUser))
            {
                Photo.SetUser(_clientService, senderUser, 28);
                TitleText.Text = senderUser.FullName();
            }
            else if (_clientService.TryGetChat(alias, out Chat senderChat))
            {
                Photo.SetChat(_clientService, senderChat, 28);
                TitleText.Text = senderChat.Title;
            }

            if (_clientService.TryGetUser(_program.BotUserId, out User botUser) && botUser.Type is UserTypeBot userTypeBot)
            {
                var percent = _program.Parameters.CommissionPercent();
                var duration = _program.Parameters.MonthCount > 0
                    ? _program.Parameters.MonthCount >= 12
                    ? Locale.Declension(Strings.R.ChannelAffiliateProgramJoinText_Years, _program.Parameters.MonthCount / 12)
                    : Locale.Declension(Strings.R.ChannelAffiliateProgramJoinText_Months, _program.Parameters.MonthCount)
                    : Strings.ChannelAffiliateProgramJoinText_Lifetime;

                var message = alias is MessageSenderChat
                    ? Strings.ChannelAffiliateProgramLinkTextChannel
                    : Strings.ChannelAffiliateProgramLinkTextUser;

                TextBlockHelper.SetMarkdown(Subtitle, string.Format(message, percent, botUser.FirstName, duration));

                Usage.Text = program.UserCount > 0
                    ? Locale.Declension(Strings.R.ChannelAffiliateProgramLinkOpened, program.UserCount, botUser.FirstName)
                    : string.Format(Strings.ChannelAffiliateProgramLinkOpenedNone, botUser.FirstName);
            }
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

        private async void UpdateAlias(MessageSender sender)
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
                return;
            }

            var response = await _clientService.SendAsync(new GetChatAffiliateProgram(chatId, _program.BotUserId));
            if (response is ChatAffiliateProgram program)
            {
                Hide();
                _navigationService.ShowPopup(new ConnectedProgramPopup(_clientService, _navigationService, _program, sender));
            }
            else if (_clientService.TryGetUserFull(_program.BotUserId, out UserFullInfo fullInfo))
            {
                Hide();
                _navigationService.ShowPopup(new AffiliateProgramPopup(_clientService, _navigationService, new FoundAffiliateProgram(_program.BotUserId, fullInfo.BotInfo.AffiliateProgram), sender));
            }
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            MessageHelper.CopyLink(XamlRoot, _program.Url);
        }
    }
}
