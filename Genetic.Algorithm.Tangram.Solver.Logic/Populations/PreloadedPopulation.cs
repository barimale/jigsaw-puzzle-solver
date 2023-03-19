using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Populations
{
    public class PreloadedPopulation : Population
    {
        private IList<IChromosome> m_preloadChromosome;

        public PreloadedPopulation(
            int minSize,
            int maxSize,
            IList<IChromosome> preloadChromosomes)
            : base(minSize, maxSize, preloadChromosomes.FirstOrDefault())
        {
            m_preloadChromosome = preloadChromosomes;
        }

        public override void CreateInitialGeneration()
        {
            Generations = new List<Generation>();
            GenerationsNumber = 0;

            foreach (var c in m_preloadChromosome)
            {
                c.ValidateGenes();
            }

            CreateNewGeneration(m_preloadChromosome);
        }
    }
}
