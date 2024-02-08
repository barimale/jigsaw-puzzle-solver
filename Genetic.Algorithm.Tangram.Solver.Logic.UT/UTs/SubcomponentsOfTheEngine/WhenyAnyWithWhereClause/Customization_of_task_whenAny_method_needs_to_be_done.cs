using Genetic.Algorithm.Tangram.Solver.Logic.UT.BaseUT;
using Xunit.Abstractions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.UTs.SubcomponentsOfTheEngine.WhenyAnyWithWhereClause
{
    public class Customization_of_task_whenAny_method_needs_to_be_done : PrintToConsoleUTBase
    {
        public Customization_of_task_whenAny_method_needs_to_be_done(ITestOutputHelper output)
            : base(output)
        {
            // intentionally left blank
        }

        [Fact]
        public async Task Check_if_the_fastest_task_ends_the_flow()
        {
            // given
            var expectedValue = 1;

            var tasks = new List<Task<int>>()
            {
                WaitFor(2000, 1),
                WaitFor(4000, 2)
            };

            // when
            var results = tasks
                .ToAsyncEnumerable()
                .WhereAwait(async x => await MeetsCriteria(x, expectedValue));
            //execute them

            // then
            Assert.Equal(results.CountAsync().Result, 1);
        }

        private async Task<int> WaitFor(int inMilliseconds, int resultValue)
        {
            await Task.Delay(inMilliseconds);

            return resultValue;
        }

        private async ValueTask<bool> MeetsCriteria(Task<int> actualValue, int expectedReturnedValue)
        {
            if (actualValue.Result == expectedReturnedValue)
                return true;

            return false;
        }

    }
}