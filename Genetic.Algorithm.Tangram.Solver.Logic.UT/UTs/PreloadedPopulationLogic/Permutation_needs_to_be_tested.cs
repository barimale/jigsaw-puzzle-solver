using Genetic.Algorithm.Tangram.Common.Extensions.Extensions;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Base;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.As_A_Developer
{
    public class Permutation_needs_to_be_tested: PrintToConsoleUTBase
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
            int[] list2 = { 3, 4, 5};

            int[][] input = new[] {
                list1,
                list2
            };

            // when
            var result = input.Permutate();

            // then
            base.Display("expected values: six of them=> 1,3 1,4 1,5 2,3 2,4 2,5");
            base.Display("actual values:");
            foreach (var item in result)
            {
                var solution = string.Join(",", item);
                base.Display(solution);
            }
        }
    }
}