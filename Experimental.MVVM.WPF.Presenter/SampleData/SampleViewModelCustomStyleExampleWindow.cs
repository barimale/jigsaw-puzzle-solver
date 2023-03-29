using System;
using System.Collections.ObjectModel;
using System.Linq;
using Demo.ViewModel;
using Solver.Tangram.Game.Logic;

namespace Demo.SampleData
{
    class SampleViewModelCustomStyleExampleWindow : ViewModelExampleBase, IViewModelCustomStyleExampleWindow
    {
        private ObservableCollection<TabBase> _itemCollection;

        public new ObservableCollection<TabBase> ItemCollection
        {
            get
            {
                if (_itemCollection != null) return _itemCollection;

                base._gameInstance = CreateGame();

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

        // TODO: do this automatically custom here
        protected override Game CreateGame()
        {
            var gameParts = GameBuilder
                    .AvalaibleGameSets
                    .CreateMediumBoard(withAllowedLocations: true);

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
