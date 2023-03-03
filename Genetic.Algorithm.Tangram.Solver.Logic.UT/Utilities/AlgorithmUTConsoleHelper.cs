using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using GeneticSharp;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities
{
    public class AlgorithmUTConsoleHelper: PrintToConsoleUTBase
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
            base.Display(string.Empty);
            base.Display("Algorithm_TerminationReached");
            base.Display(string.Empty);

            var algorithmResult = sender as GeneticAlgorithm;

            if (algorithmResult != null)
            {
                var bestChromosome = algorithmResult
                    .BestChromosome as TangramChromosome;

                ShowChromosome(bestChromosome);
            }
        }

        private void ShowChromosome(TangramChromosome? c)
        {
            if (c == null)
                return;

            if (!c.Fitness.HasValue)
                return;

            var fitnessValue = c.Fitness.Value;

            base.Display("Solution fitness: " + Math.Round(fitnessValue, 4));
            base.Display("Blocks:");

            var board = c.BoardShapeDefinition.ToString();

            var solution = c
                    .GetGenes()
                    .Select(p => (BlockBase)p.Value)
                    .ToList();

            foreach (var block in solution)
            {
                base.Display(
                    block.Color.ToString() + " coords: " + block.ToString());
            }
        }
    }
}
