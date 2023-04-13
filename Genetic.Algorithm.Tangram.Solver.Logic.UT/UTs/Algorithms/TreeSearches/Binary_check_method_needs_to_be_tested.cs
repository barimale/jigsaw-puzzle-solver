using Xunit.Abstractions;
using Assert = Xunit.Assert;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Helpers;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.Algorithms.TreeSearches
{
    public class Binary_check_method_needs_to_be_tested : PrintToConsoleUTBase
    {
        private AlgorithmUTConsoleHelper AlgorithmUTConsoleHelper;

        public Binary_check_method_needs_to_be_tested(ITestOutputHelper output)
            : base(output)
        {
            AlgorithmUTConsoleHelper = new AlgorithmUTConsoleHelper(output);
        }

        [Fact]
        public void Example()
        {
            // given
            var binaries = new List<int[]>()
            {
                new int[] { 1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0},
                new int[] { 0, 1, 1, 0 , 0, 0,1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0},
                new int[] { 0, 0, 0, 0 , 1, 1, 1, 0, 0, 1, 0, 0 , 1, 0, 0, 1, 0, 0 },
                new int[] { 1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0},
                new int[] { 0, 1, 1, 0 , 0, 0,1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0},
                new int[] { 0, 0, 0, 0 , 1, 1, 1, 0, 0, 1, 0, 0 , 1, 0, 0, 1, 0, 0 },
                new int[] { 1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0},
                new int[] { 0, 1, 1, 0 , 0, 0,1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0},
                new int[] { 0, 0, 0, 0 , 1, 1, 1, 0, 0, 1, 0, 0 , 1, 0, 0, 1, 0, 0 },
                new int[] { 1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0,1, 0, 0, 1 , 0, 0}
            };

            // when
            var sums =
                 from array in binaries
                 from valueIndex in array.Select((value, index) => new { Value = value, Index = index })
                 group valueIndex by valueIndex.Index into indexGroups
                 select indexGroups.Select(indexGroup => indexGroup.Value).Sum();

            var diffSum = sums
                .Select(p => Math.Abs(p - 1))
                .ToArray();

            var diff = 1 * diffSum.Sum();

            // then
            Assert.NotEqual(0, diff);
        }
    }
}