using Genetic.Algorithm.Tangram.Common.Extensions.Extensions;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitness;
using GeneticSharp;
using NetTopologySuite.Geometries;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Populations.Generators
{
    public class InitialPopulationGenerator
    {
        public IList<IChromosome> Generate(
            IList<BlockBase> blocksWithAllowedLocations,
            TangramFitness fitnessDefinition,
            int[] angles,
            double percentOfAllPermutationsInProcents = 20d)
        {
            if (percentOfAllPermutationsInProcents > 100d)
            {
                throw new ArgumentException("percentOfAllPermutationsInProcents cannot be higher than 100");
            }

            var allLocations = blocksWithAllowedLocations
               .Select(p => p.AllowedLocations.ToArray())
               .ToArray();

            var allPermutations = allLocations
                .Permutate()
                .ToList();

            var chromosomes = new ConcurrentBag<TangramChromosome>();
            IEnumerable<IEnumerable<Geometry>> finalPermutations;

            if (percentOfAllPermutationsInProcents != 100d)
            {
                var randomizer = new FastRandomRandomization();
                var length = (int)(allPermutations.Count * percentOfAllPermutationsInProcents / 100d);
                var uniqueIndexes = randomizer.GetUniqueInts(length, 0, (allPermutations.Count - 1));

                var narrowedPermutations = allPermutations.Where(p => uniqueIndexes
                    .Contains(allPermutations.IndexOf(p)));
                finalPermutations = narrowedPermutations;
            }
            else
            {
                finalPermutations = allPermutations;
            }

            finalPermutations.AsParallel().ForAll(permutation =>
            {
                var newChromosome = new TangramChromosome(
                    blocksWithAllowedLocations,
                    fitnessDefinition.Board,
                    angles);

                foreach (var (gene, index) in permutation.WithIndex())
                {
                    var newBlockAsGene = new BlockBase(
                        gene,
                        blocksWithAllowedLocations[index].Color,
                        false);

                    newChromosome.ReplaceGene(
                        index,
                        new Gene(newBlockAsGene));
                }

                chromosomes.Add(newChromosome);
            });

            return chromosomes
                .Select(p => (IChromosome)p)
                .ToImmutableList();
        }
    }
}
