using System;
using System.Collections.ObjectModel;
using System.Linq;
using Demo.ViewModel;
using Solver.Tangram.Game.Logic;

namespace Demo.SampleData
{
    public class SampleViewModelMainWindow : ViewModelExampleBase, IViewModelMainWindow
    {
        public bool CanMoveTabs
        {
            get => true;
            set => throw new NotImplementedException();
        }

        private ObservableCollection<TabBase> _itemCollection;

        public new ObservableCollection<TabBase> ItemCollection
        {
            get
            {
                if (_itemCollection != null) return _itemCollection;

                _gameInstance = CreateGame();

                _itemCollection = new ObservableCollection<TabBase>
                       {
                           CreateSolutionCircuitTab(ref _gameInstance),
                           CreateGameElementsTab(ref _gameInstance),
                           CreateBoardDetailsTab(ref _gameInstance),
                       };

                return _itemCollection;
            }
            set => _itemCollection = value;
        }

        public new TabBase SelectedTab
        {
            get => ItemCollection.FirstOrDefault();
            set => throw new NotImplementedException();
        }

        public bool ShowAddButton
        {
            get => true;
            set => throw new NotImplementedException();
        }

        // TODO: do this automatically
        protected override Game CreateGame()
        {
            var gameParts = GameBuilder
                    .AvalaibleGameSets
                    .CreatePolishBigBoard(withAllowedLocations: true);

            // reorder gameparts
            var orderedBlocks = gameParts
                .Blocks
                .OrderByDescending(p => p.AllowedLocations.Length)
                .ToList();

            gameParts.Blocks.Clear();
            orderedBlocks.ForEach(pp => gameParts.Blocks.Add(pp));

            var algorithm = GameBuilder
                .AvalaibleTSTemplatesAlgorithms
                .CreateDepthFirstTreeSearchAlgorithm(
                    gameParts.Board,
                    gameParts.Blocks);

            var konfiguracjaGry = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(algorithm)
                .Build();

            return konfiguracjaGry;
        }
    }
}
