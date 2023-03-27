using System;
using System.Collections.ObjectModel;
using System.Linq;
using Demo.ViewModel;
using GalaSoft.MvvmLight.Command;

namespace Demo.SampleData
{
    class SampleViewModelPinnedTabExampleWindow:ViewModelExampleBase,IViewModelPinnedTabExampleWindow
    {

        public RelayCommand<TabBase> PinTabCommand
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        private ObservableCollection<TabBase> _itemCollection;

        public new ObservableCollection<TabBase> ItemCollection
        {
            get
            {
                if (_itemCollection != null) return _itemCollection;
                _itemCollection = new ObservableCollection<TabBase>();
                TabBase tab1 = CreateSolutionCircuitTab();
                tab1.IsPinned = true;
                _itemCollection.Add(tab1);
                _itemCollection.Add(CreateGameElementsTab());
                _itemCollection.Add(CreateBoardDetailsTab());
                return _itemCollection;
            }
            set => _itemCollection = value;
        }
        public new TabBase SelectedTab
        {
            get => ItemCollection.FirstOrDefault();
            set => throw new NotImplementedException();
        }
    }
}
