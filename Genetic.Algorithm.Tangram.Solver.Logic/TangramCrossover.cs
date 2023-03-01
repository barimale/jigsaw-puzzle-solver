using Genetic.Algorithm.Tangram.Solver.Logic.Extensions;
using GeneticSharp;
using System.ComponentModel;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    [DisplayName("Two Parents Tangram")]
    public class TangramCrossover : CrossoverBase
    {
        private float mutation_probability;

        public TangramCrossover(float mutation_probability)
            : base(2, 2)
        {
            this.mutation_probability = mutation_probability;
        }

        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            var offspring = (TangramChromosome)parents[0].CreateNew();

            var parent1Genes = parents[0].GetGenes();
            var parent2Genes = parents[1].GetGenes();

            var zipped = parent1Genes.ToList().Zip(parent2Genes.ToList());

            foreach(var (pair, index) in zipped.WithIndex())
            {
                var prob = RandomizationProvider.Current.GetFloat(0, 1);

                if(prob < (1 - this.mutation_probability) / 2)
                {
                    offspring.ReplaceGene(index, pair.First);
                }
                else if(prob < 1 - this.mutation_probability)
                {
                    offspring.ReplaceGene(index, pair.Second);
                }
                else
                {
                    // do nothing
                }
            }

            return new List<IChromosome>() { offspring };
        }
    }
}