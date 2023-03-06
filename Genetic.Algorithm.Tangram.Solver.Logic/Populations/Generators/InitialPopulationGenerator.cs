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
            double percentOfAllPermutationsInPercents = 20d)
        {
            if (percentOfAllPermutationsInPercents > 100d)
            {
                throw new ArgumentException("percentOfAllPermutationsInPercents cannot be bigger than 100");
            }

            var allLocations = blocksWithAllowedLocations
               .Select(p => p.AllowedLocations.ToArray())
               .ToArray();

            var chromosomes = new ConcurrentBag<TangramChromosome>();
            IEnumerable<IEnumerable<Geometry>> finalPermutations;

            if (percentOfAllPermutationsInPercents != 100d)
            {
                var narrowedLocations = allLocations
                    .Select(p => 
                        p.Shuffle(new FastRandomRandomization())
                        .Take(Convert.ToInt32(
                            Math.Round(p.Length * percentOfAllPermutationsInPercents / 100d, MidpointRounding.ToPositiveInfinity)))
                        .ToList()
                     );

                finalPermutations = narrowedLocations
                    .Permutate()
                    .ToList();
            }
            else
            {
                finalPermutations = allLocations
                    .Permutate()
                    .ToList();
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
