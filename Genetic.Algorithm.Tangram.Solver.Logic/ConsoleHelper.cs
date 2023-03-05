using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using GeneticSharp;
using System.Runtime.Serialization.Formatters.Binary;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public static class ConsoleHelper
    {
        // TODO: WIP 
        public static void SerializeChromosomes(IList<IChromosome> chromosomes)
        {
            using (var stream = File.Create("chromosomes.bin"))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(stream, chromosomes);
            }
        }

        public static IList<IChromosome> DerializeChromosomes()
        {
            using (var stream = File.OpenRead("chromosomes.bin"))
            {
                var bf = new BinaryFormatter();
                return bf.Deserialize(stream) as IList<IChromosome>;
            }
        }
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
