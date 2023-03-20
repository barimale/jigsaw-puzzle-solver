using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Tangram.GameParts.Logic.Block;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Helpers
{
    public static class ConsoleHelper
    {
        public static void ShowChromosome(TangramChromosome? c)
        {
            if (c == null)
                return;

            if (!c.Fitness.HasValue)
                return;

            var fitnessValue = c.Fitness.Value;

            Console.WriteLine("Solution fitness: {0:n2}", Math.Round(fitnessValue, 4));
            Console.WriteLine("Blocks:");

            var board = c.BoardShapeDefinition.ToString();

            var solution = c
                    .GetGenes()
                    .Select(p => (BlockBase)p.Value)
                    .ToList();

            foreach (var block in solution)
            {
                Console.WriteLine(
                    block.Color.ToString() + " coords: " + block.ToString());
            }
        }
    }
}
