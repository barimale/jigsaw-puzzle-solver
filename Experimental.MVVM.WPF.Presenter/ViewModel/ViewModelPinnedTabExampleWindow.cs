using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;

namespace Demo.ViewModel
{
    public class ViewModelPinnedTabExampleWindow : ViewModelExampleBase, IViewModelPinnedTabExampleWindow
    {
        public RelayCommand<TabBase> PinTabCommand { get; set; }


        public ViewModelPinnedTabExampleWindow()
        {
            TabBase vm1 = CreateSolutionCircuitTab(ref base._gameInstance);
            vm1.IsPinned = true;
            ItemCollection.Add(vm1);
            ItemCollection.Add(CreateGameElementsTab(ref base._gameInstance));
            ItemCollection.Add(CreateBoardDetailsTab(ref base._gameInstance));
            SelectedTab = ItemCollection.FirstOrDefault();
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemCollection);
            //This sort description is what keeps the source collection sorted, based on tab number. 
            //You can also use the sort description to manually sort the tabs, based on your own criterias,
            //as show below by sorting both by tab number and Pinned status.
            view.SortDescriptions.Add(new SortDescription("IsPinned", ListSortDirection.Descending));
            view.SortDescriptions.Add(new SortDescription("TabNumber", ListSortDirection.Ascending));

            PinTabCommand = new RelayCommand<TabBase>(PinTabCommandAction);
        }

        private void PinTabCommandAction(TabBase tab)
        {
            tab.IsPinned = !tab.IsPinned;
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemCollection);
            view.Refresh();
        }
    }
}
