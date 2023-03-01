using GeneticSharp;
using System.ComponentModel;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    [DisplayName("Tangram Mutation")]
    public class TangramMutation : MutationBase
    {
        public TangramMutation()
        {
            base.IsOrdered = true;
        }

        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            var prob = RandomizationProvider.Current.GetFloat(0, 1);

            if( prob <= probability)
            {
                var mutated = (TangramChromosome)chromosome.CreateNew();

                // TODO: maybe specific or random genes only
                chromosome.ReplaceGenes(0, mutated.GetGenes());
            }
        }
    }
}