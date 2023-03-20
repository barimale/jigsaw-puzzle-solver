using Algorithm.Tangram.TreeSearch.Logic;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using GeneticSharp;
using Tangram.GameParts.Logic.Block;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers
{
    public class AlgorithmUTConsoleHelper : PrintToConsoleUTBase
    {
        public AlgorithmUTConsoleHelper(ITestOutputHelper output)
            : base(output)
        {
            LatestFitness = double.MinValue;
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
            Display(string.Empty);
            Display("Algorithm_TerminationReached");
            Display(string.Empty);

            var algorithmResult = sender as GeneticAlgorithm;

            if (algorithmResult != null)
            {
                var bestChromosome = algorithmResult
                    .BestChromosome as TangramChromosome;

                ShowChromosome(bestChromosome);
            }
        }

        public void ShowChromosome(TangramChromosome? c)
        {
            if (c == null)
                return;

            if (!c.Fitness.HasValue)
                return;

            var fitnessValue = c.Fitness.Value;
            Display("Solution fitness: " + Math.Round(fitnessValue, 4));

            var board = c.BoardShapeDefinition.ToString();
            Display("Board:");
            Display(board);

            var solution = c
                    .GetGenes()
                    .Select(p => (BlockBase)p.Value)
                    .ToList();

            Display("Blocks:");
            foreach (var block in solution)
            {
                Display(
                    block.Color.ToString() + " coords: " + block.ToString());
            }
        }

        public void ShowMadeChoices(FindFittestSolution? solution)
        {
            if (solution == null)
                return;

            var fitnessValue = solution.Quality.HasValue ? solution.Quality.Value.ToString() : "unknown";
            Display("Solution fitness: " + fitnessValue);

            var board = solution.Board.ToString();
            Display("Board:");
            Display(board);

            var blocks = solution
                .Solution
                .Select(p => p.TransformedBlock)
                .ToList();

            Display("Blocks:");
            foreach (var block in blocks)
            {
                Display(
                    block.Color.ToString() + " coords: " + block.ToString());
            }
        }
    }
}
