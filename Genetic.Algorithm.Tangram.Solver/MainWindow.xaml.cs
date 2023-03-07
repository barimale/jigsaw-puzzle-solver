using Genetic.Algorithm.Tangram.Configurator;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Genetic.Algorithm.Tangram.Solver.WPF;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Genetic.Algorithm.Tangram.Solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AlgorithmDisplayHelper? algorithmDisplayHelper;
        private static GameExecutor? gameExecutor;

        private Thread thread = new Thread(Execute);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            var canvas = sender as Canvas;

            if (canvas == null)
                throw new Exception("Canvas not loaded correctly");

            if(algorithmDisplayHelper == null)
                algorithmDisplayHelper = new AlgorithmDisplayHelper(canvas, this.Dispatcher);

            var gameParts = GamePartConfiguratorBuilder
               .AvalaibleGameSets
               .CreateMediumBoard(withAllowedLocations: true);

            var algorithm = GamePartConfiguratorBuilder
                .AvalaibleTunedAlgorithms
                .CreateMediumBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            var konfiguracjaGry = new GamePartConfiguratorBuilder()
                .WithAlgorithm(algorithm)
                .WithGamePartsConfigurator(gameParts)
                .Build();

            if (gameExecutor == null)
                gameExecutor = new GameExecutor(
                    algorithmDisplayHelper,
                    konfiguracjaGry);
        }

        private static void Execute(object? obj)
        {
            gameExecutor?.Execute();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            thread.Start();
            //gameExecutor?.Execute();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gameExecutor?.Pause();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            gameExecutor?.Resume();
        }
    }
}
