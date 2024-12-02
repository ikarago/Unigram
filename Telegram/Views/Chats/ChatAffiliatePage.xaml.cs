using Telegram.Common;
using Telegram.Controls;
using Telegram.Controls.Cells;
using Telegram.Td.Api;
using Telegram.ViewModels.Chats;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Documents;

namespace Telegram.Views.Chats
{
    public sealed partial class ChatAffiliatePage : HostedPage
    {
        public ChatAffiliateViewModel ViewModel => DataContext as ChatAffiliateViewModel;

        public ChatAffiliatePage()
        {
            InitializeComponent();
            Title = Strings.ChannelAffiliateProgramTitle;

            Headline.Text = Strings.ChannelAffiliateProgramText;
        }

        private void OnChoosingItemContainer(ListViewBase sender, ChoosingItemContainerEventArgs args)
        {
            if (args.ItemContainer == null)
            {
                args.ItemContainer = new TableListViewItem();
                args.ItemContainer.Style = sender.ItemContainerStyle;
                args.ItemContainer.ContentTemplate = sender.ItemTemplate;
            }

            args.IsContainerPrepared = true;
        }

        private void OnContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue)
            {
                return;
            }
            else if (args.ItemContainer.ContentTemplateRoot is ProfileCell cell)
            {
                if (args.Item is FoundAffiliateProgram)
                {
                    cell.UpdateFoundAffiliateProgram(ViewModel.ClientService, args, OnContainerContentChanging);
                }
                else
                {
                    cell.UpdateAffiliateProgram(ViewModel.ClientService, args, OnContainerContentChanging);
                }
            }

            args.Handled = true;
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private string ConvertSortOrder(AffiliateProgramSortOrder sortOrder)
        {
            return sortOrder switch
            {
                AffiliateProgramSortOrderCreationDate => Strings.ChannelAffiliateProgramProgramsSortDate,
                AffiliateProgramSortOrderRevenue => Strings.ChannelAffiliateProgramProgramsSortRevenue,
                _ => Strings.ChannelAffiliateProgramProgramsSortProfitability
            };
        }

        private void SortOrder_Click(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            var type = ViewModel.SortOrder.GetType();

            var date = new ToggleMenuFlyoutItem
            {
                Text = Strings.ChannelAffiliateProgramProgramsSortDate,
                Tag = new AffiliateProgramSortOrderCreationDate(),
                IsChecked = type == typeof(AffiliateProgramSortOrderCreationDate)
            };

            var revenue = new ToggleMenuFlyoutItem
            {
                Text = Strings.ChannelAffiliateProgramProgramsSortRevenue,
                Tag = new AffiliateProgramSortOrderRevenue(),
                IsChecked = type == typeof(AffiliateProgramSortOrderRevenue)
            };

            var profitability = new ToggleMenuFlyoutItem
            {
                Text = Strings.ChannelAffiliateProgramProgramsSortProfitability,
                Tag = new AffiliateProgramSortOrderProfitability(),
                IsChecked = type == typeof(AffiliateProgramSortOrderProfitability)
            };

            date.Click += SortOrder_Click;
            revenue.Click += SortOrder_Click;
            profitability.Click += SortOrder_Click;

            var flyout = new MenuFlyout();

            flyout.Items.Add(date);
            flyout.Items.Add(revenue);
            flyout.Items.Add(profitability);

            flyout.ShowAt(sender.ElementStart.VisualParent, FlyoutPlacementMode.BottomEdgeAlignedRight);
        }

        private void SortOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleMenuFlyoutItem { IsChecked: true, Tag: AffiliateProgramSortOrder sortOrder })
            {
                ViewModel.SortOrder = sortOrder;
            }
        }
    }
}
