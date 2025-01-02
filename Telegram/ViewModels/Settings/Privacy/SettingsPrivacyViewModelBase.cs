//
// Copyright Fela Ameghino 2915-2025
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Common;
using Telegram.Controls;
using Telegram.Controls.Cells;
using Telegram.Controls.Media;
using Telegram.Navigation;
using Telegram.Navigation.Services;
using Telegram.Services;
using Telegram.Td.Api;
using Telegram.Views.Popups;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Telegram.ViewModels.Settings
{
    public partial class SettingsPrivacyViewModelBase : MultiViewModelBase, IHandle
    {
        private readonly UserPrivacySetting _inputKey;

        public SettingsPrivacyViewModelBase(IClientService clientService, ISettingsService settingsService, IEventAggregator aggregator, UserPrivacySetting inputKey)
            : base(clientService, settingsService, aggregator)
        {
            _inputKey = inputKey;
        }

        protected override Task OnNavigatedToAsync(object parameter, NavigationMode mode, NavigationState state)
        {
            UpdatePrivacy();
            return Task.CompletedTask;
        }

        public override void Subscribe()
        {
            Aggregator.Subscribe<UpdateUserPrivacySettingRules>(this, Handle);
        }

        public void Handle(UpdateUserPrivacySettingRules update)
        {
            if (update.Setting.TypeEquals(_inputKey))
            {
                UpdatePrivacyImpl(update.Rules);
            }
        }

        private void UpdatePrivacy()
        {
            ClientService.Send(new GetUserPrivacySettingRules(_inputKey), result =>
            {
                if (result is UserPrivacySettingRules rules)
                {
                    UpdatePrivacyImpl(rules);
                }
            });
        }

        private void UpdatePrivacyImpl(UserPrivacySettingRules rules)
        {
            PrivacyValue? primary = null;
            var badge = string.Empty;

            var restricted = 0;
            var restrictedBots = false;
            var allowed = 0;
            var allowedPremium = false;
            var allowedBots = false;
            UserPrivacySettingRuleAllowUsers allowedUsers = null;
            UserPrivacySettingRuleAllowChatMembers allowedChatMembers = null;
            UserPrivacySettingRuleRestrictUsers restrictedUsers = null;
            UserPrivacySettingRuleRestrictChatMembers restrictedChatMembers = null;
            foreach (var current in rules.Rules)
            {
                if (current is UserPrivacySettingRuleAllowAll && primary == null)
                {
                    primary = PrivacyValue.AllowAll;
                    badge = Strings.LastSeenEverybody;
                }
                else if (current is UserPrivacySettingRuleAllowContacts && primary == null)
                {
                    primary = PrivacyValue.AllowContacts;
                    badge = Strings.LastSeenContacts;
                }
                else if (current is UserPrivacySettingRuleRestrictAll && primary == null)
                {
                    primary = PrivacyValue.DisallowAll;
                    badge = Strings.LastSeenNobody;
                }
                else if (current is UserPrivacySettingRuleRestrictUsers disallowUsers)
                {
                    restrictedUsers = disallowUsers;
                    restricted += disallowUsers.UserIds.Count;
                }
                else if (current is UserPrivacySettingRuleAllowUsers allowUsers)
                {
                    allowedUsers = allowUsers;
                    allowed += allowUsers.UserIds.Count;
                }
                else if (current is UserPrivacySettingRuleAllowPremiumUsers)
                {
                    allowedPremium = true;
                }
                else if (current is UserPrivacySettingRuleAllowBots)
                {
                    allowedBots = true;
                }
                else if (current is UserPrivacySettingRuleRestrictBots)
                {
                    restrictedBots = true;
                }
                else if (current is UserPrivacySettingRuleRestrictChatMembers restrictChatMembers)
                {
                    restrictedChatMembers = restrictChatMembers;

                    foreach (var chatId in restrictChatMembers.ChatIds)
                    {
                        var chat = ClientService.GetChat(chatId);
                        if (chat == null)
                        {
                            continue;
                        }

                        if (ClientService.TryGetBasicGroup(chat, out BasicGroup basicGroup))
                        {
                            restricted += basicGroup.MemberCount;
                        }
                        else if (ClientService.TryGetSupergroup(chat, out Supergroup supergroup))
                        {
                            restricted += supergroup.MemberCount;
                        }
                    }
                }
                else if (current is UserPrivacySettingRuleAllowChatMembers allowChatMembers)
                {
                    allowedChatMembers = allowChatMembers;

                    foreach (var chatId in allowChatMembers.ChatIds)
                    {
                        var chat = ClientService.GetChat(chatId);
                        if (chat == null)
                        {
                            continue;
                        }

                        if (ClientService.TryGetBasicGroup(chat, out BasicGroup basicGroup))
                        {
                            allowed += basicGroup.MemberCount;
                        }
                        else if (ClientService.TryGetSupergroup(chat, out Supergroup supergroup))
                        {
                            allowed += supergroup.MemberCount;
                        }
                    }
                }
            }

            if (primary == null)
            {
                primary = PrivacyValue.DisallowAll;
                badge = allowedPremium
                    ? Strings.LastSeenNobodyPremium
                    : allowedBots
                    ? Strings.PrivacyValueBots
                    : Strings.LastSeenNobody;
            }
            else if (primary == PrivacyValue.AllowContacts && allowedPremium)
            {
                badge = Strings.LastSeenContactsPremium;
            }
            else if (primary == PrivacyValue.AllowContacts && allowedBots)
            {
                badge = Strings.PrivacyContactsAndBotUsers;
            }
            else if (primary == PrivacyValue.DisallowAll && allowedPremium)
            {
                badge = Strings.LastSeenNobodyPremium;
            }
            else if (primary == PrivacyValue.DisallowAll && allowedBots)
            {
                badge = Strings.PrivacyValueBots;
            }
            else if (primary == PrivacyValue.AllowAll && restrictedBots)
            {
                badge = Strings.PrivacyValueEveryoneExceptBots;
            }

            var list = new List<string>();
            if (restricted > 0)
            {
                list.Add("-" + restricted);
            }
            if (allowed > 0)
            {
                list.Add("+" + allowed);
            }

            if (list.Count > 0)
            {
                badge = string.Format("{0} ({1})", badge, string.Join(", ", list));
            }

            _restrictedUsers = restrictedUsers ?? new UserPrivacySettingRuleRestrictUsers(Array.Empty<long>());
            _restrictedChatMembers = restrictedChatMembers ?? new UserPrivacySettingRuleRestrictChatMembers(Array.Empty<long>());

            _restrictedBots = restrictedBots;

            _allowedUsers = allowedUsers ?? new UserPrivacySettingRuleAllowUsers(Array.Empty<long>());
            _allowedChatMembers = allowedChatMembers ?? new UserPrivacySettingRuleAllowChatMembers(Array.Empty<long>());

            _allowedPremium = allowedPremium;
            _allowedBots = allowedBots;

            BeginOnUIThread(() =>
            {
                SelectedItem = primary ?? PrivacyValue.DisallowAll;

                Badge = badge;

                if (restrictedBots)
                {
                    RestrictedBadge = restricted > 0 ? string.Format(Strings.PrivacyValueBotsAnd, Locale.Declension(Strings.R.Users, restricted)) : Strings.PrivacyValueBots;
                }
                else
                {
                    RestrictedBadge = restricted > 0 ? Locale.Declension(Strings.R.Users, restricted) : Strings.EmpryUsersPlaceholder;
                }

                if (allowedPremium)
                {
                    AllowedBadge = allowed > 0 ? string.Format(Strings.PrivacyPremiumAnd, Locale.Declension(Strings.R.Users, allowed)) : Strings.PrivacyPremium;
                }
                else if (allowedBots)
                {
                    AllowedBadge = allowed > 0 ? string.Format(Strings.PrivacyValueBotsAnd, Locale.Declension(Strings.R.Users, allowed)) : Strings.PrivacyValueBots;
                }
                else
                {
                    AllowedBadge = allowed > 0 ? Locale.Declension(Strings.R.Users, allowed) : Strings.EmpryUsersPlaceholder;
                }
            });

        }

        private string _badge;
        public string Badge
        {
            get => _badge;
            set => Set(ref _badge, value);
        }

        private PrivacyValue _selectedItem;
        public PrivacyValue SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }

        private string _allowedBadge;
        public string AllowedBadge
        {
            get => _allowedBadge;
            set => Set(ref _allowedBadge, value);
        }

        private bool _allowedPremium;
        private bool _allowedBots;

        private UserPrivacySettingRuleAllowUsers _allowedUsers;
        private UserPrivacySettingRuleAllowChatMembers _allowedChatMembers;



        private string _restrictedBadge;
        public string RestrictedBadge
        {
            get => _restrictedBadge;
            set => Set(ref _restrictedBadge, value);
        }

        private bool _restrictedBots;

        private UserPrivacySettingRuleRestrictUsers _restrictedUsers;
        private UserPrivacySettingRuleRestrictChatMembers _restrictedChatMembers;

        public async void Always()
        {
            if (_allowedUsers == null ||
                _allowedChatMembers == null)
            {
                return;
            }

            var chats = new List<long>();
            var users = new List<long>();

            foreach (var id in _allowedUsers.UserIds)
            {
                var chat = await ClientService.SendAsync(new CreatePrivateChat(id, true)) as Chat;
                if (chat != null)
                {
                    chats.Add(chat.Id);
                }
            }

            foreach (var id in _allowedChatMembers.ChatIds)
            {
                chats.Add(id);
            }

            var popup = new ChooseChatsPopup();
            popup.Legacy(SessionId);
            popup.PrimaryButtonText = Strings.OK;
            popup.ViewModel.NavigationService = NavigationService;
            popup.ViewModel.AllowEmptySelection = true;

            var allowedPremium = false;
            var allowedBots = false;

            if (_inputKey is UserPrivacySettingAllowChatInvites && SelectedItem != PrivacyValue.AllowAll)
            {
                var cell = new ChatShareCell
                {
                    PhotoSource = new PlaceholderImage(Icons.Premium16, true, Color.FromArgb(0xFF, 0x97, 0x6F, 0xFF), Color.FromArgb(0xFF, 0xE4, 0x6A, 0xCE)),
                    PhotoShape = ProfilePictureShape.Superellipse,
                    Title = Strings.PrivacyPremium,
                    SelectionStroke = BootStrapper.Current.Resources["ContentDialogBackground"] as SolidColorBrush,
                    Stroke = BootStrapper.Current.Resources["ChatLastMessageStateBrush"] as SolidColorBrush,
                    Padding = new Thickness(12, 6, 12, 6)
                };

                allowedPremium = _allowedPremium;
                cell.UpdateState(allowedPremium, false, false);

                var button = new Button
                {
                    Style = BootStrapper.Current.Resources["ListEmptyButtonStyle"] as Style,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Padding = new Thickness(0),
                    Margin = new Thickness(12, 0, 12, 0),
                    Background = new SolidColorBrush(Colors.Transparent),
                    Content = cell
                };

                button.Click += (s, args) =>
                {
                    allowedPremium = !allowedPremium;
                    cell.UpdateState(allowedPremium, true, false);
                };

                popup.Header = button;
            }
            else if (_inputKey is UserPrivacySettingAutosaveGifts && SelectedItem == PrivacyValue.DisallowAll)
            {
                var cell = new ChatShareCell
                {
                    PhotoSource = PlaceholderImage.GetGlyph(Icons.BotFilled),
                    PhotoShape = ProfilePictureShape.Superellipse,
                    Title = Strings.PrivacyMiniapps,
                    SelectionStroke = BootStrapper.Current.Resources["ContentDialogBackground"] as SolidColorBrush,
                    Stroke = BootStrapper.Current.Resources["ChatLastMessageStateBrush"] as SolidColorBrush,
                    Padding = new Thickness(12, 6, 12, 6)
                };

                allowedBots = _allowedBots;
                cell.UpdateState(allowedBots, false, false);

                var button = new Button
                {
                    Style = BootStrapper.Current.Resources["ListEmptyButtonStyle"] as Style,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Padding = new Thickness(0),
                    Margin = new Thickness(12, 0, 12, 0),
                    Background = new SolidColorBrush(Colors.Transparent),
                    Content = cell
                };

                button.Click += (s, args) =>
                {
                    allowedBots = !allowedBots;
                    cell.UpdateState(allowedBots, true, false);
                };

                popup.Header = button;
            }

            switch (_inputKey)
            {
                case UserPrivacySettingAllowCalls:
                case UserPrivacySettingAllowPeerToPeerCalls:
                case UserPrivacySettingAllowChatInvites:
                case UserPrivacySettingShowProfilePhoto:
                case UserPrivacySettingShowLinkInForwardedMessages:
                default:
                    popup.ViewModel.Title = Strings.AlwaysAllow;
                    break;
                case UserPrivacySettingShowStatus:
                    popup.ViewModel.Title = Strings.AlwaysShareWithTitle;
                    break;
            }

            var confirm = await popup.PickAsync(XamlRoot, chats, ChooseChatsOptions.Privacy);
            if (confirm != ContentDialogResult.Primary)
            {
                return;
            }

            chats.Clear();
            users.Clear();

            foreach (var chat in popup.ViewModel.SelectedItems)
            {
                if (chat.Type is ChatTypePrivate privata)
                {
                    users.Add(privata.UserId);
                }
                else
                {
                    chats.Add(chat.Id);
                }
            }

            _allowedPremium = allowedPremium;
            _allowedBots = allowedBots;
            _allowedUsers = new UserPrivacySettingRuleAllowUsers(users);
            _allowedChatMembers = new UserPrivacySettingRuleAllowChatMembers(chats);

            AllowedBadge = GetBadge(_allowedUsers.UserIds, _allowedChatMembers.ChatIds, _allowedPremium, _allowedBots);
        }

        public async void Never()
        {
            if (_restrictedUsers == null ||
                _restrictedChatMembers == null)
            {
                return;
            }

            var chats = new List<long>();
            var users = new List<long>();

            foreach (var id in _restrictedUsers.UserIds)
            {
                var chat = await ClientService.SendAsync(new CreatePrivateChat(id, true)) as Chat;
                if (chat != null)
                {
                    chats.Add(chat.Id);
                }
            }

            foreach (var id in _restrictedChatMembers.ChatIds)
            {
                chats.Add(id);
            }

            var popup = new ChooseChatsPopup();
            popup.Legacy(SessionId);
            popup.PrimaryButtonText = Strings.OK;
            popup.ViewModel.NavigationService = NavigationService;
            popup.ViewModel.AllowEmptySelection = true;

            var restrictedBots = false;

            if (_inputKey is UserPrivacySettingAutosaveGifts && SelectedItem == PrivacyValue.AllowAll)
            {
                var cell = new ChatShareCell
                {
                    PhotoSource = PlaceholderImage.GetGlyph(Icons.BotFilled),
                    PhotoShape = ProfilePictureShape.Superellipse,
                    Title = Strings.PrivacyMiniapps,
                    SelectionStroke = BootStrapper.Current.Resources["ContentDialogBackground"] as SolidColorBrush,
                    Stroke = BootStrapper.Current.Resources["ChatLastMessageStateBrush"] as SolidColorBrush,
                    Padding = new Thickness(12, 6, 12, 6)
                };

                restrictedBots = _restrictedBots;
                cell.UpdateState(restrictedBots, false, false);

                var button = new Button
                {
                    Style = BootStrapper.Current.Resources["ListEmptyButtonStyle"] as Style,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Padding = new Thickness(0),
                    Margin = new Thickness(12, 0, 12, 0),
                    Background = new SolidColorBrush(Colors.Transparent),
                    Content = cell
                };

                button.Click += (s, args) =>
                {
                    restrictedBots = !restrictedBots;
                    cell.UpdateState(restrictedBots, true, false);
                };

                popup.Header = button;
            }

            switch (_inputKey)
            {
                case UserPrivacySettingAllowCalls:
                case UserPrivacySettingAllowPeerToPeerCalls:
                case UserPrivacySettingAllowChatInvites:
                case UserPrivacySettingShowProfilePhoto:
                case UserPrivacySettingShowLinkInForwardedMessages:
                default:
                    popup.ViewModel.Title = Strings.NeverAllow;
                    break;
                case UserPrivacySettingShowStatus:
                    popup.ViewModel.Title = Strings.NeverShareWithTitle;
                    break;
            }

            var confirm = await popup.PickAsync(XamlRoot, chats, ChooseChatsOptions.Privacy);
            if (confirm != ContentDialogResult.Primary)
            {
                return;
            }

            chats.Clear();
            users.Clear();

            foreach (var chat in popup.ViewModel.SelectedItems)
            {
                if (chat.Type is ChatTypePrivate privata)
                {
                    users.Add(privata.UserId);
                }
                else
                {
                    chats.Add(chat.Id);
                }
            }

            _restrictedBots = restrictedBots;
            _restrictedUsers = new UserPrivacySettingRuleRestrictUsers(users);
            _restrictedChatMembers = new UserPrivacySettingRuleRestrictChatMembers(chats);

            RestrictedBadge = GetBadge(_restrictedUsers.UserIds, _restrictedChatMembers.ChatIds, false, _restrictedBots);
        }

        public virtual async void Save()
        {
            var response = await SendAsync();
            if (response is Ok)
            {
                NavigationService.GoBack();
            }
            else if (response is Error error)
            {

            }
        }

        public Task<BaseObject> SendAsync()
        {
            var rules = new List<UserPrivacySettingRule>();

            if (_allowedPremium && _inputKey is UserPrivacySettingAllowChatInvites && _selectedItem != PrivacyValue.AllowAll)
            {
                rules.Add(new UserPrivacySettingRuleAllowPremiumUsers());
            }
            else if (_allowedBots && _inputKey is UserPrivacySettingAutosaveGifts && _selectedItem == PrivacyValue.DisallowAll)
            {
                rules.Add(new UserPrivacySettingRuleAllowBots());
            }
            else if (_restrictedBots && _inputKey is UserPrivacySettingAutosaveGifts && _selectedItem == PrivacyValue.AllowAll)
            {
                rules.Add(new UserPrivacySettingRuleRestrictBots());
            }

            if (_allowedUsers != null && _allowedUsers.UserIds.Count > 0 && _selectedItem != PrivacyValue.AllowAll)
            {
                rules.Add(_allowedUsers);
            }
            if (_allowedChatMembers != null && _allowedChatMembers.ChatIds.Count > 0 && _selectedItem != PrivacyValue.AllowAll)
            {
                rules.Add(_allowedChatMembers);
            }

            if (_restrictedUsers != null && _restrictedUsers.UserIds.Count > 0 && _selectedItem != PrivacyValue.DisallowAll)
            {
                rules.Add(_restrictedUsers);
            }
            if (_restrictedChatMembers != null && _restrictedChatMembers.ChatIds.Count > 0 && _selectedItem != PrivacyValue.DisallowAll)
            {
                rules.Add(_restrictedChatMembers);
            }

            switch (_selectedItem)
            {
                case PrivacyValue.AllowAll:
                    rules.Add(new UserPrivacySettingRuleAllowAll());
                    break;
                case PrivacyValue.AllowContacts:
                    rules.Add(new UserPrivacySettingRuleAllowContacts());
                    break;
                case PrivacyValue.DisallowAll:
                    rules.Add(new UserPrivacySettingRuleRestrictAll());
                    break;
            }

            return ClientService.SendAsync(new SetUserPrivacySettingRules(_inputKey, new UserPrivacySettingRules(rules)));
        }

        private string GetBadge(IList<long> userIds, IList<long> chatIds, bool allowedPremium, bool allowedBots)
        {
            var count = userIds.Count;

            foreach (var chatId in chatIds)
            {
                var chat = ClientService.GetChat(chatId);
                if (chat == null)
                {
                    continue;
                }

                if (ClientService.TryGetBasicGroup(chat, out BasicGroup basicGroup))
                {
                    count += basicGroup.MemberCount;
                }
                else if (ClientService.TryGetSupergroup(chat, out Supergroup supergroup))
                {
                    count += supergroup.MemberCount;
                }
            }

            if (allowedPremium)
            {
                return count > 0 ? string.Format(Strings.PrivacyPremiumAnd, Locale.Declension(Strings.R.Users, count)) : Strings.PrivacyPremium;
            }
            else if (allowedBots)
            {
                return count > 0 ? string.Format(Strings.PrivacyValueBotsAnd, Locale.Declension(Strings.R.Users, count)) : Strings.PrivacyValueBots;
            }

            return count > 0 ? Locale.Declension(Strings.R.Users, count) : Strings.EmpryUsersPlaceholder;
        }
    }

    public enum PrivacyValue
    {
        AllowAll,
        AllowContacts,
        DisallowAll
    }
}
