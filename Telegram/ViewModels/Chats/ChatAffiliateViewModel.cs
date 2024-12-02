using Rg.DiffUtils;
using System.Threading.Tasks;
using Telegram.Collections;
using Telegram.Navigation;
using Telegram.Navigation.Services;
using Telegram.Services;
using Telegram.Td.Api;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

namespace Telegram.ViewModels.Chats
{
    public partial class ChatAffiliateViewModel : ViewModelBase, IIncrementalCollectionOwner, IDiffHandler<FoundAffiliateProgram>
    {
        private long _chatId;

        private ChatProgramsCollection _programs;
        private FoundProgramsCollection _foundPrograms;

        public ChatAffiliateViewModel(IClientService clientService, ISettingsService settingsService, IEventAggregator aggregator)
            : base(clientService, settingsService, aggregator)
        {
            FoundPrograms = new SearchCollection<FoundAffiliateProgram, IncrementalCollection<FoundAffiliateProgram>>(UpdateFoundPrograms, this);
        }

        protected override Task OnNavigatedToAsync(object parameter, NavigationMode mode, NavigationState _)
        {
            if (parameter is long chatId)
            {
                _chatId = chatId;
                _programs = new ChatProgramsCollection(ClientService, chatId);

                FoundPrograms.UpdateSender(new AffiliateProgramSortOrderProfitability());
            }

            return Task.CompletedTask;
        }

        public IncrementalCollection<ChatAffiliateProgram> Programs => _programs?.Items;

        public SearchCollection<FoundAffiliateProgram, IncrementalCollection<FoundAffiliateProgram>> FoundPrograms { get; }

        private IncrementalCollection<FoundAffiliateProgram> UpdateFoundPrograms(object arg1, string arg2)
        {
            if (arg1 is AffiliateProgramSortOrder sortOrder)
            {
                _foundPrograms = new FoundProgramsCollection(ClientService, _chatId, this, sortOrder);
            }

            return _foundPrograms?.Items;
        }

        public AffiliateProgramSortOrder SortOrder
        {
            get => _foundPrograms?.SortOrder;
            set
            {
                FoundPrograms.UpdateSender(value);
                RaisePropertyChanged(nameof(SortOrder));
            }
        }

        public bool CompareItems(FoundAffiliateProgram oldItem, FoundAffiliateProgram newItem)
        {
            return oldItem.BotUserId == newItem.BotUserId;
        }

        public void UpdateItem(FoundAffiliateProgram oldItem, FoundAffiliateProgram newItem)
        {
            //throw new NotImplementedException();
        }

        public Task<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (_programs.HasMoreItems)
            {
                return _programs.LoadMoreItemsAsync(count);
            }

            return _foundPrograms.LoadMoreItemsAsync(count);
        }

        public bool HasMoreItems => _foundPrograms.HasMoreItems || _programs.HasMoreItems;

        class ChatProgramsCollection : IIncrementalCollectionOwner
        {
            private readonly IClientService _clientService;
            private readonly long _chatId;

            private string _nextOffset = string.Empty;

            public ChatProgramsCollection(IClientService clientService, long chatId)
            {
                _clientService = clientService;
                _chatId = chatId;

                Items = new IncrementalCollection<ChatAffiliateProgram>(this);
            }

            public IncrementalCollection<ChatAffiliateProgram> Items { get; }

            public async Task<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
            {
                var totalCount = 0u;

                var response = await _clientService.SendAsync(new GetChatAffiliatePrograms(_chatId, _nextOffset, 20));
                if (response is ChatAffiliatePrograms programs)
                {
                    foreach (var item in programs.Programs)
                    {
                        Items.Add(item);
                        totalCount++;
                    }

                    _nextOffset = programs.NextOffset.Length > 0
                        ? programs.NextOffset
                        : null;
                }

                return new LoadMoreItemsResult
                {
                    Count = totalCount
                };
            }

            public bool HasMoreItems => _nextOffset != null;
        }

        class FoundProgramsCollection : IIncrementalCollectionOwner
        {
            private readonly IClientService _clientService;
            private readonly long _chatId;
            private readonly AffiliateProgramSortOrder _sortOrder;

            private string _nextOffset = string.Empty;

            public FoundProgramsCollection(IClientService clientService, long chatId, IIncrementalCollectionOwner owner, AffiliateProgramSortOrder sortOrder)
            {
                _clientService = clientService;
                _chatId = chatId;
                _sortOrder = sortOrder;

                Items = new IncrementalCollection<FoundAffiliateProgram>(owner);
            }

            public IncrementalCollection<FoundAffiliateProgram> Items { get; }

            public AffiliateProgramSortOrder SortOrder => _sortOrder;

            public async Task<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
            {
                var totalCount = 0u;

                var response = await _clientService.SendAsync(new SearchAffiliatePrograms(_chatId, _sortOrder, _nextOffset, 20));
                if (response is FoundAffiliatePrograms programs)
                {
                    foreach (var item in programs.Programs)
                    {
                        Items.Add(item);
                        totalCount++;
                    }

                    _nextOffset = programs.NextOffset.Length > 0
                        ? programs.NextOffset
                        : null;
                }

                return new LoadMoreItemsResult
                {
                    Count = totalCount
                };
            }

            public bool HasMoreItems => _nextOffset != null;
        }
    }
}
