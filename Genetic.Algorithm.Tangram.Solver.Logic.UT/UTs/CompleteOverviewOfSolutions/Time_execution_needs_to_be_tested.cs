using Genetic.Algorithm.Tangram.Configurator;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitness;
using Genetic.Algorithm.Tangram.Solver.Logic.Populations.Generators;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities;
using GeneticSharp;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
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
            var gameParts = GamePartConfiguratorBuilder
                .AvalaibleGameSets
                .CreateBigBoard(withAllowedLocations: true);

            var algorithm = GamePartConfiguratorBuilder
                .AvalaibleTunedAlgorithms
                .CreateBigBoardSettings(
                    gameParts.Board,
                    gameParts.Blocks,
                    gameParts.AllowedAngles);

            // reuse async here
            TangramFitness? fitnessFunction = algorithm.Fitness as TangramFitness;

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
                    var result = await fitnessFunction.EvaluateAsync(p);
                    if (result <= 0 && result >= expectedFitnessValue)
                    {
                        solutions.Add((TangramChromosome)p);
                    }
                });
            }).ToArray();

            await Task.WhenAll(tasks);

            sw.Stop();

            // then
            TimeSpan ts = TimeSpan.FromTicks(sw.ElapsedTicks);
            string elapsed = ts.TotalMilliseconds.ToString();
            base.Display("Elapsed time in milliseconds: " + elapsed + "ms");

            var amountOfSolutions = solutions.Count;
            base.Display("Solution counts: " + amountOfSolutions);

            base.Display("Solutions:");
            foreach (var item in solutions.ToImmutableList())
            {
                AlgorithmUTConsoleHelper.ShowChromosome(item);
            }
        }
    }
}