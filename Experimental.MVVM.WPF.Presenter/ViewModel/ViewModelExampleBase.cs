using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using ChromeTabs;
using Demo.ViewModel.SolverTabs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.Game.Logic;

namespace Demo.ViewModel
{
    public class ViewModelExampleBase : ViewModelBase
    {
        //since we don't know what kind of objects are bound, so the sorting happens outside with the ReorderTabsCommand.
        public RelayCommand<TabReorder> ReorderTabsCommand { get; set; }
        public RelayCommand AddTabCommand { get; set; }
        public RelayCommand<TabBase> CloseTabCommand { get; set; }
        public ObservableCollection<TabBase> ItemCollection { get; set; }

        //This is the current selected tab, if you change it, the tab is selected in the tab control.
        private TabBase _selectedTab;
        public TabBase SelectedTab
        {
            get => _selectedTab;
            set
            {
                if (_selectedTab != value)
                {
                    Set(() => SelectedTab, ref _selectedTab, value);
                }
            }
        }

        private bool _canAddTabs;
        public bool CanAddTabs
        {
            get => _canAddTabs;
            set
            {
                if (_canAddTabs == value) return;
                Set(() => CanAddTabs, ref _canAddTabs, value);
                AddTabCommand.RaiseCanExecuteChanged();
            }
        }

        protected Game _gameInstance;

        public ViewModelExampleBase()
        {
            ItemCollection = new ObservableCollection<TabBase>();
            ItemCollection.CollectionChanged += ItemCollection_CollectionChanged;
            ReorderTabsCommand = new RelayCommand<TabReorder>(ReorderTabsCommandAction);
            AddTabCommand = new RelayCommand(AddTabCommandAction,()=>CanAddTabs);
            CloseTabCommand = new RelayCommand<TabBase>(CloseTabCommandAction);
            CanAddTabs = false;

            _gameInstance = CreateGame();
        }

        protected virtual Game CreateGame()
        {
            // to use the default value pass -1
            int maxDegreeOfParallelism = Environment.ProcessorCount; // 2048 * 6 * 4; // -1

            var gameParts = GameBuilder
                .AvalaibleGameSets
                .CreatePolishBigBoard(withAllowedLocations: true);

            // algorithms
            var binDepthTS = GameBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateBinaryDepthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks,
                    maxDegreeOfParallelism: maxDegreeOfParallelism);

            var ga = GameBuilder
                .AvalaibleGATunedAlgorithms
                .CreateBinaryBigBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles,
                    maxDegreeOfParallelism: maxDegreeOfParallelism);

            var konfiguracjaGry = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithManyAlgorithms()
                .WithAlgorithms(binDepthTS) //, ga)
                .WithExecutionMode(ExecutionMode.WhenAny)
                .Build();

            return konfiguracjaGry;
        }

        protected SolutionCircuit CreateSolutionCircuitTab(ref Game gameInstance)
        {
            var tab = new SolutionCircuit(ref gameInstance) {
                TabName = "Solution circuit",
                IsPinned = true,
                TabIcon = new BitmapImage(new Uri("/Resources/1.png", UriKind.Relative))
            };

            return tab;
        }

        protected ElementsDetails CreateGameElementsTab(ref Game gameInstance)
        {
            var tab = new ElementsDetails(ref gameInstance) {
                TabName = "Elements",
                IsPinned = true,
                TabIcon = new BitmapImage(new Uri("/Resources/2.png", UriKind.Relative))
            };

            return tab;

        }

        protected BoardDetails CreateBoardDetailsTab(ref Game gameInstance)
        {
            var tab = new BoardDetails(ref gameInstance) {
                TabName = "Board",
                IsPinned = true,
                TabIcon = new BitmapImage(new Uri("/Resources/3.png", UriKind.Relative))
            };

            return tab;
        }

        /// <summary>
        /// Reorder the tabs and refresh collection sorting.
        /// </summary>
        /// <param name="reorder"></param>
        protected virtual void ReorderTabsCommandAction(TabReorder reorder)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ItemCollection);
            int from = reorder.FromIndex;
            int to = reorder.ToIndex;
            var tabCollection = view.Cast<TabBase>().ToList();//Get the ordered collection of our tab control

            tabCollection[from].TabNumber = tabCollection[to].TabNumber; //Set the new index of our dragged tab

            if (to > from)
            {
                for (int i = from + 1; i <= to; i++)
                {
                    tabCollection[i].TabNumber--; //When we increment the tab index, we need to decrement all other tabs.
                }
            }
            else if (from > to)//when we decrement the tab index
            {
                for (int i = to; i < from; i++)
                {
                    tabCollection[i].TabNumber++;//When we decrement the tab index, we need to increment all other tabs.
                }
            }

            view.Refresh();//Refresh the view to force the sort description to do its work.
        }

        //We need to set the TabNumber property on the viewmodels when the item source changes to keep it in sync.
        void ItemCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TabBase tab in e.NewItems)
                {
                    if (ItemCollection.Count > 1)
                    {
                        //If the new tab don't have an existing number, we increment one to add it to the end.
                        if (tab.TabNumber == 0)
                            tab.TabNumber = ItemCollection.OrderBy(x => x.TabNumber).LastOrDefault().TabNumber + 1;
                    }
                }
            }
            else
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(ItemCollection);
                view.Refresh();
                var tabCollection = view.Cast<TabBase>().ToList();
                foreach (var item in tabCollection)
                    item.TabNumber = tabCollection.IndexOf(item);
            }
        }

        //To close a tab, we simply remove the viewmodel from the source collection.
        private void CloseTabCommandAction(TabBase vm)
        {
            ItemCollection.Remove(vm);
        }

        //Adds a random tab
        private void AddTabCommandAction()
        {
            Random r = new Random();
            int num = r.Next(1, 100);
            if (num < 33)
                ItemCollection.Add(CreateSolutionCircuitTab(ref _gameInstance));
            else if (num < 66)
                ItemCollection.Add(CreateGameElementsTab(ref _gameInstance));
            else
                ItemCollection.Add(CreateBoardDetailsTab(ref _gameInstance));
        }
    }
}
