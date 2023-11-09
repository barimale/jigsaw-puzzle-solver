using Algorithm.Tangram.TreeSearch.Logic;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using NetTopologySuite.Geometries;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.EventArgs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.Solver.UI.Utilities
{
    public class PolygonPair
    {
        public string X;
        public string Y;
    }

    public class Polygon
    {
        public List<PolygonPair> polygonPairs;
        public string color;
    }

    public class PolygonPairsResult
    {
        public List<Polygon> blocks = new List<Polygon>();
    }

    public class AlgorithmDisplayHelper
    {
        public double LatestFitness { private set; get; } = double.MinValue;
        private bool isSolved = false;

        public AlgorithmDisplayHelper()
        {
            // intentionally left blank
        }

        public void Reset()
        {
            LatestFitness = double.MinValue;
            isSolved = false;
        }

        public void Algorithm_Ran(object sender, SourceEventArgs e)
        {
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
                    case FindBinaryFittestSolution _:
                        var binaryChoicesMade = algorithmResult
                            .GetSolution<FindBinaryFittestSolution>();
                        ShowTreeSearchSolution(binaryChoicesMade);
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

        public PolygonPairsResult? MapToPolygonPairsResult(object sender, SourceEventArgs e)
        {
            try
            {
                var algorithmResult = sender as AlgorithmResult;

                if (algorithmResult == null)
                    return null;

                switch (algorithmResult.Solution)
                {
                    case TangramChromosome _:
                        var bestChromosome = algorithmResult
                            .GetSolution<TangramChromosome>();
                        return MapSolution(bestChromosome);
                    case FindFittestSolution _:
                        var choicesMade = algorithmResult
                            .GetSolution<FindFittestSolution>();
                        return MapSolution(choicesMade);
                    case FindBinaryFittestSolution _:
                        var binaryChoicesMade = algorithmResult
                            .GetSolution<FindBinaryFittestSolution>();
                        return MapSolution(binaryChoicesMade);
                    default:
                        // do nothing
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private PolygonPairsResult MapSolution(TangramChromosome c)
        {
            var solution = c
                   .GetGenes()
                   .Select(p => (BlockBase)p.Value)
                   .ToList();

            return DoMapChromosome(solution);
        }

        private PolygonPairsResult MapSolution(FindFittestSolution c)
        {
            var solution = c.Solution
                       .ToList()
                       .Select(p => p.TransformedBlock!)
                       .ToList();

            return DoMapChromosome(solution);
        }

        private PolygonPairsResult MapSolution(FindBinaryFittestSolution c)
        {
            var solution = c.Solution
                        .ToList()
                        .Select(p => p.TransformedBlock!)
                        .ToList();

            return DoMapChromosome(solution);
        }

        private PolygonPairsResult DoMapChromosome(List<BlockBase> solution)
        {
            var shapes = solution
                .Select(p =>
                {
                    var polygonPairs = p
                        .Polygon
                        .Coordinates
                        .Select(pp => new PolygonPair()
                        {
                            X = pp.X.ToString(),
                            Y = pp.Y.ToString()
                        })
                        .ToList();
                    var color = p.Color;

                    return new Polygon()
                    {
                        polygonPairs = polygonPairs,
                        color = color.Name.ToString()
                    };
                })
                .ToList();

            PolygonPairsResult result = new PolygonPairsResult()
            {
                blocks = shapes
            };

            return result;
        }
    }
}