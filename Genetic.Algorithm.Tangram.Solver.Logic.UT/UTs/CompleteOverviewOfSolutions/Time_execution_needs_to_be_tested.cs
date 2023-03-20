using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses;
using Genetic.Algorithm.Tangram.Solver.Logic.Populations.Generators;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;
using Solver.Tangram.Configurator;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.CompleteOverviewOfSolutions
{
    public class Time_execution_needs_to_be_tested : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public Time_execution_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public async Task Check_time_execution_for_big_board()
        {
            // given
            var gameParts = GameConfiguratorBuilder
                .AvalaibleGameSets
                .CreateBigBoard(withAllowedLocations: true);

            var algorithm = GameConfiguratorBuilder
                .AvalaibleGATunedAlgorithms
                .CreateBigBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            // reuse async here
            TangramService? fitnessFunction = algorithm.Fitness as TangramService;

            // when
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var initialPopulationGenerator = new InitialPopulationGenerator();
            var chromosomes = initialPopulationGenerator
                .Generate(
                    gameParts.Blocks,
                    gameParts.Board,
                    gameParts.AllowedAngles,
                    100d)
                .ToList();

            var expectedFitnessValue = -0.01f;
            var solutions = new ConcurrentBag<TangramChromosome>();

            var tasks = chromosomes.Select(p =>
            {
                return Task
                .Factory
                .StartNew(async () =>
                {
                    await fitnessFunction
                        .EvaluateAsync(p)
                        .ContinueWith(result =>
                        {
                            if (result.IsCompleted
                                && result.Result <= 0
                                && result.Result >= expectedFitnessValue)
                            {
                                p.Fitness = result.Result;
                                solutions.Add((TangramChromosome)p);
                            }
                        });
                });
            }).ToArray();

            await Task.WhenAll(tasks);

            sw.Stop();

            // then
            TimeSpan ts = TimeSpan.FromTicks(sw.ElapsedTicks);
            string elapsed = ts.TotalMilliseconds.ToString();
            Display("Elapsed time in milliseconds: " + elapsed + "ms");

            var amountOfSolutions = solutions.Count;
            Display("Solution counts: " + amountOfSolutions);

            Display("Solutions:");
            foreach (var item in solutions.ToImmutableList())
            {
                AlgorithmUTConsoleHelper.ShowChromosome(item);
            }
        }
    }
}