using GeneticSharp.Extensions;
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
        public static void ShowChromosome(TspChromosome c)
        {
            Console.WriteLine("Fitness: {0:n2}", c.Fitness);
            Console.WriteLine("Cities: {0:n0}", c.Length);
            Console.WriteLine("Distance: {0:n2}", c.Distance);

            var cities = c.GetGenes().Select(g => g.Value.ToString()).ToArray();
            Console.WriteLine("City tour: {0}", string.Join(", ", cities));
        }
    }
}
