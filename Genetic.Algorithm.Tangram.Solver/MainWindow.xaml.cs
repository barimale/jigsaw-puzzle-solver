using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using Genetic.Algorithm.Tangram.Solver.WPF;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Genetic.Algorithm.Tangram.Solver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AlgorithmDisplayHelper algorithmDisplayHelper;
        private GameExecutor gameExecutor;

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
                algorithmDisplayHelper = new AlgorithmDisplayHelper(canvas);

            if(gameExecutor == null)
                gameExecutor = new GameExecutor(
                    algorithmDisplayHelper,
                    SimpleBoardData.DemoData());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gameExecutor.Execute();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gameExecutor.Pause();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            gameExecutor.Resume();
        }
    }
}
