using Algorithm.Tangram.Common.Extensions;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using GeneticSharp;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.PreloadedPopulationLogic
{
    public class Permutation_needs_to_be_tested : PrintToConsoleUTBase
    {
        public Permutation_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            // intentionally left blank
        }

        [Fact]
        public void Check_permutation_for_integers()
        {
            // given
            int[] list1 = { 1, 2 };
            int[] list2 = { 3, 4, 5 };

            int[][] input = new[] {
                list1,
                list2
            };

            // when
            var result = input.Permutate();

            // then
            Display("expected values: six of them=> 1,3 1,4 1,5 2,3 2,4 2,5");
            Display("actual values:");
            foreach (var item in result)
            {
                var solution = string.Join(",", item);
                Display(solution);
            }
        }

        [Fact]
        public void Check_fast_randomization()
        {
            // given
            var randomizer = new FastRandomRandomization();

            // when
            var result = randomizer.GetInt(0, 1);
            var check = result == 0 || result == 1;

            // then
            Assert.Equal(check, true);
        }
    }
}