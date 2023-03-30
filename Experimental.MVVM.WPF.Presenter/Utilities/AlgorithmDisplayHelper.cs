using Algorithm.Tangram.TreeSearch.Logic;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Demo.Utilities
{
    public class AlgorithmDisplayHelper
    {
        private Canvas Canvas;
        private Dispatcher Dispatcher;

        public double LatestFitness { private set; get; } = double.MinValue;
        private bool isSolved = false;

        public AlgorithmDisplayHelper(ref Canvas canvas, Dispatcher dispatcher)
        {
            Canvas = canvas;
            Dispatcher = dispatcher;
            Dispatcher.Invoke(() =>
            {
                Canvas.RenderTransform = new ScaleTransform(30, 30);
            });
        }

        public void Reset()
        {
            this.LatestFitness = double.MinValue;
            this.isSolved = false;
            Canvas.Children.Clear();
        }

        public void Algorithm_Ran(object sender, SourceEventArgs e)
        {
            // TODO WIP 
            // Do not show more results if at least one algorithm is terminated.
            // later on display as many canvases as algorithms
            if (isSolved)
                return;

            try
            {
                var algorithmResult = sender as AlgorithmResult;

                if (algorithmResult == null)
                    return;

                switch (algorithmResult.Solution)
                {
                    case TangramChromosome _:
                        var bestChromosome = algorithmResult
                            .GetSolution<TangramChromosome>();
                        ShowChromosome(bestChromosome);
                        break;
                    case FindFittestSolution _:
                        var choicesMade = algorithmResult
                            .GetSolution<FindFittestSolution>();
                        ShowTreeSearchSolution(choicesMade);
                        break;
                    default:
                        // do nothing
                        break;
                }
            }
            catch (Exception)
            {
                // intentionally left blank
            }
        }

        public void Algorithm_TerminationReached(object sender, SourceEventArgs e)
        {
            var isTerminated = false;

            try
            {
                var algorithmResult = sender as AlgorithmResult;

                if (algorithmResult == null)
                    return;

                switch (algorithmResult.Solution)
                {
                    case TangramChromosome _:
                        var bestChromosome = algorithmResult
                            .GetSolution<TangramChromosome>();
                        ShowChromosome(bestChromosome);
                        break;
                    case FindFittestSolution _:
                        var choicesMade = algorithmResult
                            .GetSolution<FindFittestSolution>();
                        ShowTreeSearchSolution(choicesMade);
                        break;
                    default:
                        // do nothing
                        break;
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
                    PlayApplause();
                    isSolved = true;
                }
            }
        }

        private static void PlayApplause()
        {
            var pathToApplause = System.IO.Path.Join(
                                    Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName,
                                    @"Resources\Sounds\applause.wav");

            var player = new System.Media.SoundPlayer(pathToApplause);
            player.Play();
        }

        private void ShowChromosome(TangramChromosome c)
        {
            Dispatcher.Invoke(() =>
            {
                if (c == null)
                    return;

                // clear canvas
                Canvas.Children.Clear();

                // show board
                var board = new Polygon();
                board.Points = new PointCollection();
                board.Fill = Brushes.GhostWhite;
                board.Stroke = Brushes.DarkGray;
                board.StrokeThickness = 0.05d;

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
                        shape.Stroke = Brushes.Silver;
                        shape.StrokeThickness = 0.05d;
                        return (UIElement)shape;
                    })
                    .ToList();

                shapes.ForEach(p => Canvas.Children.Add(p));
            });
        }

        private void ShowTreeSearchSolution(FindFittestSolution c)
        {
            Dispatcher.Invoke(() =>
            {
                if (c == null)
                    return;

                // clear canvas
                Canvas.Children.Clear();

                // show board
                var board = new Polygon();
                board.Points = new PointCollection();
                board.Fill = Brushes.GhostWhite;
                board.Stroke = Brushes.DarkGray;
                board.StrokeThickness = 0.05d;

                c.Board
                    .Polygon
                    .Coordinates
                    .ToList()
                    .ForEach(p => board
                        .Points
                        .Add(new Point(p.X, p.Y)));

                Canvas.Children.Add(board);

                // show blocks
                var solution = c.Solution
                        .ToList()
                        .Select(p => p.TransformedBlock!)
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
                        shape.Stroke = Brushes.Silver;
                        shape.StrokeThickness = 0.05d;
                        return (UIElement)shape;
                    })
                    .ToList();

                shapes.ForEach(p => Canvas.Children.Add(p));
            });
        }

        public static SolidColorBrush ConvertColor(System.Drawing.Color value)
        {
            System.Drawing.Color color = value;

            Color converted = Color.FromArgb(
                color.A,
                color.R,
                color.G,
                color.B);

            return new SolidColorBrush(converted);
        }
    }
}