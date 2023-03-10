using Genetic.Algorithm.Tangram.Configurator;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Genetic.Algorithm.Tangram.Solver.WPF;
using GeneticSharp;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public static readonly DependencyProperty MyTitleProperty = DependencyProperty.Register("MyTitle", typeof(String), typeof(MainWindow));

        public String MyTitle
        {
            get { return (String)GetValue(MainWindow.MyTitleProperty); }
            set
            {
                SetValue(MainWindow.MyTitleProperty, value);
                OnPropertyChanged("MyTitle");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            MyTitle = string.Empty;
        }

        private void GameExecutor_GenerationRan(object? sender, EventArgs e)
        {
            var ga = sender as GeneticAlgorithm;

            if (ga == null)
                return;

            Dispatcher.Invoke(() =>
            {
                MyTitle = ga.State.ToString();
            });
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
               .CreateBigBoard(withAllowedLocations: true); // TODO WIP correct for false

            var algorithm = GamePartConfiguratorBuilder
                .AvalaibleTunedAlgorithms
                .CreateBigBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            var konfiguracjaGry = new GamePartConfiguratorBuilder()
                .WithAlgorithm(algorithm)
                .WithGamePartsConfigurator(gameParts)
                .Build();

            if (gameExecutor == null)
            {
                gameExecutor = new GameExecutor(
                    algorithmDisplayHelper,
                    konfiguracjaGry);

                gameExecutor.GenerationRan += GameExecutor_GenerationRan;
            }
        }

        private static void Execute(object? obj)
        {
            gameExecutor?.Execute();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(thread.ThreadState == ThreadState.Unstarted)
                thread.Start();
            //gameExecutor?.Execute();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(thread.ThreadState == ThreadState.Running)
                thread.Suspend();
            //gameExecutor?.Pause();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (thread.ThreadState == ThreadState.Suspended)
                thread.Resume();
            //gameExecutor?.Resume();
        }
    }
}
