using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using GeneticSharp;
using System.ComponentModel;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Mutations
{
    [DisplayName("Tangram Mutation")]
    public class TangramMutation : MutationBase
    {
        public TangramMutation()
        {
            IsOrdered = true;
        }

        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            var mutated = (TangramChromosome)chromosome.CreateNew();
            chromosome.ReplaceGenes(0, mutated.GetGenes());
        }
    }
}