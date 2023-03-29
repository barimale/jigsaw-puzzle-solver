using System;
using System.Collections.ObjectModel;
using System.Linq;
using Demo.ViewModel;
using GalaSoft.MvvmLight.Command;
using Solver.Tangram.Game.Logic;

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
                _gameInstance = CreateGame();
                _itemCollection = new ObservableCollection<TabBase>();
                _itemCollection.Add(CreateSolutionCircuitTab(ref _gameInstance));
                _itemCollection.Add(CreateGameElementsTab(ref _gameInstance));
                _itemCollection.Add(CreateBoardDetailsTab(ref _gameInstance));
                return _itemCollection;
            }
            set => _itemCollection = value;
        }

        public new TabBase SelectedTab
        {
            get => ItemCollection.FirstOrDefault();
            set => throw new NotImplementedException();
        }

        // TODO: do this automatically
        protected override Game CreateGame()
        {
            var gameParts = GameBuilder
                    .AvalaibleGameSets
                    .CreatePolishMediumBoard(withAllowedLocations: true);

            var algorithm = GameBuilder
                .AvalaibleGAVaryRatiosTunedAlgorithms
                .CreateMediumBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles,
                    10000);

            var konfiguracjaGry = new GameBuilder()
                .WithGamePartsConfigurator(gameParts)
                .WithAlgorithm(algorithm)
                .Build();

            return konfiguracjaGry;
        }
    }
}
