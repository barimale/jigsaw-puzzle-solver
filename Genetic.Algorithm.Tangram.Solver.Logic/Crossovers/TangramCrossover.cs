using Generic.Algorithm.Tangram.Common.Extensions;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using GeneticSharp;
using System.ComponentModel;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Crossovers
{
    [DisplayName("Two Parents Tangram")]
    public class TangramCrossover : CrossoverBase
    {
        public TangramCrossover()
            : base(2, 2)
        {
            // intentionally left blank
        }

        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            var probMin = 0;
            var probMax = 1;

            var offspring = (TangramChromosome)parents[0].CreateNew();

            var parent1Genes = parents[0].GetGenes();
            var parent2Genes = parents[1].GetGenes();

            var zipped = parent1Genes.ToList().Zip(parent2Genes.ToList());

            foreach (var (pair, index) in zipped.WithIndex())
            {
                var prob = new FastRandomRandomization()
                    .GetFloat(
                        probMin,
                        probMax);

                if (prob < probMax / 2)
                {
                    offspring.ReplaceGene(index, pair.First);
                }
                else
                {
                    offspring.ReplaceGene(index, pair.Second);
                }
            }

            return new List<IChromosome>() { offspring };
        }
    }
}