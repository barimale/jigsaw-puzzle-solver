using GeneticSharp;
using System.Runtime.Serialization.Formatters.Binary;

namespace Genetic.Algorithm.Tangram.Common.Extensions
{
    public static class TangramChromosomeExtensions
    {
        // for example: results.bin
        public static void SerializeChromosomes(
            this IList<IChromosome> chromosomes,
            string fileName)
        {
            using (var stream = File.Create(fileName))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(stream, chromosomes);
            }
        }

        public static IList<IChromosome> DerializeChromosomes(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                var bf = new BinaryFormatter();
                return bf.Deserialize(stream) as IList<IChromosome>;
            }
        }
    }
}
