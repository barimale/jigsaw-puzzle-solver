using Genetic.Algorithm.Tangram.Configurator;
using Genetic.Algorithm.Tangram.GameParts;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using GeneticSharp;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class I_would_like_to_solve_medium_board: PrintToConsoleUTBase
    {
        private AlgorithmDebugHelper AlgorithmDebugHelper;
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public I_would_like_to_solve_medium_board(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmDebugHelper = new AlgorithmDebugHelper();
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        private int CurrentGeneration { get; set; } = -1;
        private int GenerationsNumber { get; set; } = -1;

        private GamePartsConfigurator konfiguracjaGry;

        [Fact]
        public void With_shape_of_4x5_fields_by_using_4_blocks_and_no_unused_fields()
        {
            // given
            var gameParts = GamePartConfiguratorBuilder
                .AvalaibleGameSets
                .CreateMediumBoard(withAllowedLocations: true);

            var algorithm = GamePartConfiguratorBuilder
                .AvalaibleTunedAlgorithms
                .CreateMediumBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            var konfiguracjaGry = new GamePartConfiguratorBuilder()
                .WithAlgorithm(algorithm)
                .WithGamePartsConfigurator(gameParts)
                .Build();

            if (konfiguracjaGry.Algorithm == null)
                return;

            GenerationsNumber = konfiguracjaGry.Algorithm.Population.MaxSize;

            konfiguracjaGry
                .Algorithm
                .TerminationReached += AlgorithmDebugHelper
                    .Algorithm_TerminationReached;
            
            konfiguracjaGry
                .Algorithm
                .GenerationRan += AlgorithmDebugHelper
                    .Algorithm_Ran;

            konfiguracjaGry
                .Algorithm
                .GenerationRan += Algorithm_Ran;

            konfiguracjaGry
                .Algorithm
                .Stopped += AlgorithmDebugHelper
                    .Algorithm_TerminationReached;

            // when
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