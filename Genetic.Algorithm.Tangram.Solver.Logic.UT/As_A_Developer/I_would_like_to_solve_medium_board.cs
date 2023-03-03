using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Data.BigBoard;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_medium_board
    {
        private AlgorithmDebugHelper AlgorithmResultsHelper = new AlgorithmDebugHelper();
        private int CurrentGeneration { get; set; } = -1;
        private int GenerationsNumber { get; set; } = 200000;

        private GamePartsConfigurator konfiguracjaGry;

        [Fact]
        public void With_shape_of_4x5_fields_by_using_4_blocks_and_no_unused_fields()
        {
            // given
            konfiguracjaGry = MediumBoardData.DemoData();

            konfiguracjaGry
                .Algorithm
                .TerminationReached += AlgorithmResultsHelper
                    .Algorithm_TerminationReached;
            
            konfiguracjaGry
                .Algorithm
                .GenerationRan += AlgorithmResultsHelper
                    .Algorithm_Ran;

            konfiguracjaGry
                .Algorithm
                .GenerationRan += Algorithm_Ran;

            konfiguracjaGry
                .Algorithm
                .Stopped += AlgorithmResultsHelper
                    .Algorithm_TerminationReached;

            // when
            //konfiguracjaGry.Algorithm.TaskExecutor = new ParallelTaskExecutor
            //{
            //    MinThreads = 100,
            //    MaxThreads = 300
            //};
            konfiguracjaGry.Algorithm.Start();

            // then
        }

        private void Algorithm_Ran(object? sender, EventArgs e)
        {
            var algorithmResult = sender as GeneticAlgorithm;

            if (algorithmResult == null)
                return;

            if (CurrentGeneration > GenerationsNumber)
            { 
                konfiguracjaGry.Algorithm.Stop();
                return;
            }

            CurrentGeneration = algorithmResult.Population.CurrentGeneration.Number;

            konfiguracjaGry
                .Algorithm
                .CrossoverProbability = GetCrossoverProbabilityRatio(konfiguracjaGry
                    .Algorithm
                    .CrossoverProbability);

            konfiguracjaGry
                .Algorithm
                .MutationProbability = GetMutationProbabilityRatio(konfiguracjaGry
                    .Algorithm
                    .MutationProbability);
        }

        private float GetCrossoverProbabilityRatio(float crossoverProbability)
        {
            if (CurrentGeneration < 0 || GenerationsNumber < 0)
                return crossoverProbability;

            return GetDynamicRatios().Item2;
        }

        private float GetMutationProbabilityRatio(float mutationProbability)
        {
            if (CurrentGeneration < 0 || GenerationsNumber < 0)
                return mutationProbability;

            return GetDynamicRatios().Item1;
        }

        private Tuple<float, float> GetDynamicRatios()
        {
            float factor = (float)CurrentGeneration / (float)GenerationsNumber;

            // mutation, crossover
            var ratios = new Tuple<float, float>(1f - factor, factor);

            return ratios;
        }
    }
}