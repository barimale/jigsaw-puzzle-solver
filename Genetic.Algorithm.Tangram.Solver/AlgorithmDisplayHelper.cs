using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using GeneticSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Algorithm.Executor.WPF
{
    public class AlgorithmDisplayHelper
    {
        private Canvas Canvas;
        private Dispatcher Dispatcher;
        private AlgorithmDisplayHelper()
        {
            LatestFitness = double.MinValue;
        }

        public AlgorithmDisplayHelper(Canvas canvas, Dispatcher dispatcher)
            : this()
        {
            Canvas = canvas;
            Dispatcher = dispatcher;
            Dispatcher.Invoke(() =>
            {
                Canvas.RenderTransform = new ScaleTransform(30, 30);
            });
        }

        public double LatestFitness { private set; get; }

        public void Algorithm_Ran(object? sender, EventArgs e)
        {
            var algorithmResult = sender as GeneticAlgorithm;

            if (algorithmResult == null)
                return;

            var bestChromosome = algorithmResult
                .BestChromosome as TangramChromosome;

            if (bestChromosome == null || !bestChromosome.Fitness.HasValue)
                return;

            var bestFitness = bestChromosome
                .Fitness
                .Value;

            if (bestFitness >= LatestFitness)
            {
                LatestFitness = bestFitness;
                ShowChromosome(bestChromosome);
            }
        }

        public void Algorithm_TerminationReached(object? sender, EventArgs e)
        {
            var isTerminated = false;

            try
            {
                var algorithmResult = sender as GeneticAlgorithm;

                if (algorithmResult != null)
                {
                    var bestChromosome = algorithmResult
                        .BestChromosome as TangramChromosome;

                    ShowChromosome(bestChromosome);
                }
            }
            catch (Exception)
            {
                isTerminated = true;
            }
            finally
            {
                if (!isTerminated)
                {
                    var pathToApplause = System.IO.Path.Join(
                        Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName,
                        @"Sounds\applause.wav");

                    var player = new System.Media.SoundPlayer(pathToApplause);
                    player.Play();
                }
            }
        }

        private void ShowChromosome(TangramChromosome? c)
        {
            Dispatcher.Invoke(() =>
            {
                if (c == null)
                    return;

                if (!c.Fitness.HasValue)
                    return;

                var fitnessValue = c.Fitness.Value;

                // clear canvas
                Canvas.Children.Clear();

                // show board
                var board = new Polygon();
                board.Points = new PointCollection();
                board.Fill = Brushes.White;
                board.Stroke = Brushes.Black;
                board.StrokeThickness = 0.1d;

                c.BoardShapeDefinition
                    .Polygon
                    .Coordinates
                    .ToList()
                    .ForEach(p => board
                        .Points
                        .Add(new Point(p.X, p.Y)));

                Canvas.Children.Add(board);

                // show blocks
                var solution = c
                        .GetGenes()
                        .Select(p => (BlockBase)p.Value)
                        .ToList();

                var shapes = solution
                    .Select(p =>
                    {
                        var shape = new Polygon();
                        shape.Points = new PointCollection(p
                            .Polygon
                            .Coordinates
                            .Select(pp => new Point(pp.X, pp.Y))
                            .ToList());
                        shape.Fill = ConvertColor(p.Color);
                        //shape.Stroke = Brushes.Black;
                        shape.StrokeThickness = 0;
                        return (UIElement)shape;
                    })
                    .ToList();

                shapes.ForEach(p => Canvas.Children.Add(p));
            });
        }

        private SolidColorBrush ConvertColor(System.Drawing.Color value)
        {
            System.Drawing.Color color = value;
            Color converted = Color.FromArgb(color.A, color.R, color.G, color.B);
            return new SolidColorBrush(converted);
        }
    }
}
