using GeneticSharp;
using System.Runtime.Serialization.Formatters.Binary;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Utilities
{
    public static class ConsoleHelper
    {
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
        public static void ShowChromosome(TangramChromosome c)
        {
            Console.WriteLine("Fitness: {0:n2}", c.Fitness.Value);
            var aa = Math.Round(c.Fitness.Value, 4);

            Console.WriteLine("Shapes:");
            var blocksAmount = c.GenesCount;
            for(int i = 0; i < blocksAmount; i++)
            {
                var blockAsString = c[i].ToString();
                Console.WriteLine(blockAsString);
            }
        }
    }
}
