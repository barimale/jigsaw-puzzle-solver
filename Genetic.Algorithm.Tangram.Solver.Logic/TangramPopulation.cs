using GeneticSharp;
using System.ComponentModel;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    [DisplayName("Tangram Tracking")]
    public class TangramTrackingGenerationStrategy : IGenerationStrategy
    {
        #region Methods
        /// <summary>
        /// Register that a new generation has been created.
        /// </summary>
        /// <param name="population">The population where the new generation has been created.</param>
        public void RegisterNewGeneration(IPopulation population)
        {
            // Do nothing, because wants to keep all generations in the line.
        }
        #endregion
    }
}
