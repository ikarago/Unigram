//
// Copyright Fela Ameghino 2015-2025
//
// Distributed under the GNU General Public License v3.0. (See accompanying
// file LICENSE or copy at https://www.gnu.org/licenses/gpl-3.0.txt)
//
using System.Linq;
using System.Threading.Tasks;
using Telegram.Collections;
using Telegram.Navigation;
using Telegram.Navigation.Services;
using Telegram.Services;
using Telegram.Td.Api;
using Telegram.Views.Stars.Popups;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

namespace Telegram.ViewModels.Profile
{
    public partial class ProfileGiftsTabViewModel : ViewModelBase, IHandle, IIncrementalCollectionOwner
    {
        private long _userId;
        private string _nextOffsetId = string.Empty;

        public ProfileGiftsTabViewModel(IClientService clientService, ISettingsService settingsService, IEventAggregator aggregator)
            : base(clientService, settingsService, aggregator)
        {
            Items = new IncrementalCollection<UserGift>(this);
        }

        protected override Task OnNavigatedToAsync(object parameter, NavigationMode mode, NavigationState state)
        {
            if (parameter is long chatId)
            {
                var chat = ClientService.GetChat(chatId);
                if (chat == null)
                {
                    return Task.CompletedTask;
                }

                var user = ClientService.GetUser(chat);
                if (user == null)
                {
                    return Task.CompletedTask;
                }

                _userId = user.Id;
            }

            return Task.CompletedTask;
        }

        public override void Subscribe()
        {
            Aggregator.Subscribe<UpdateGiftIsSaved>(this, Handle)
                .Subscribe<UpdateGiftIsSold>(Handle);
        }

        private void Handle(UpdateGiftIsSaved update)
        {
            if (_userId == update.SenderUserId)
            {
                BeginOnUIThread(() =>
                {
                    var userGift = Items.FirstOrDefault(x => x.MessageId == update.MessageId);
                    if (userGift == null)
                    {
                        return;
                    }

                    userGift.IsSaved = update.IsSaved;

                    var index = Items.IndexOf(userGift);
                    Items.Remove(userGift);
                    Items.Insert(index, userGift);
                });
            }
        }

        private void Handle(UpdateGiftIsSold update)
        {
            if (_userId == update.SenderUserId)
            {
                BeginOnUIThread(() =>
                {
                    var userGift = Items.FirstOrDefault(x => x.MessageId == update.MessageId);
                    if (userGift == null)
                    {
                        return;
                    }

                    Items.Remove(userGift);
                });
            }
        }

        public IncrementalCollection<UserGift> Items { get; private set; }

        public async Task<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            var total = 0u;

            var response = await ClientService.SendAsync(new GetUserGifts(_userId, _nextOffsetId, 50));
            if (response is UserGifts gifts)
            {
                _nextOffsetId = gifts.NextOffset;

                foreach (var gift in gifts.Gifts)
                {
                    Items.Add(gift);
                    total++;
                }
            }

            HasMoreItems = !string.IsNullOrEmpty(_nextOffsetId);

            return new LoadMoreItemsResult
            {
                Count = total
            };
        }

        public bool HasMoreItems { get; private set; } = true;

        public void OpenGift(UserGift userGift)
        {
            if (userGift == null)
            {
                return;
            }

            ShowPopup(new UserGiftPopup(ClientService, NavigationService, userGift, _userId));
        }
    }
}
