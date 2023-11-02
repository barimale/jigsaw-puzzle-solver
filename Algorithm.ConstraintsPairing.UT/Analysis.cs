using System.Threading.Tasks;
using Xunit;

namespace Algorithm.ConstraintsPairing.UT
{
    public class Analysis
    {
        private readonly Engine _engine;

        public Analysis()
        {
            _engine = new Engine(); 
        }

        [Fact]
        public async Task GoodExample()
        {
            // given
            int[,] costs = {
                { -1, 100, 0, 0 },
                { 0, -1, 0, 0 },
                { 100, 100, -1, 0 },
                { 0, 0, 0, -1 }
            };

            InputData input = new()
            {
                Costs = costs
            };

            // when
            var result = await _engine.CalculateAsync(input);

            // then
            Assert.NotNull(result);
            Assert.False(result.IsError);
            Assert.Null(result.Reason);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data.Pairs);
            Assert.Equal(result.Data.Pairs.Length, result.Data.GifterAmount);
            Assert.Equal(result.Data.Pairs.Length, input.GifterAmount);
        }

        [Fact]
        public async Task BadCornerCase()
        {
            // given
            int[,] costs = {
                { -1, 100, 100, 100 },
                { 0, -1, 0, 0 },
                { 100, 100, -1, 100 },
                { 100, 100, 100, -1 }
            };

            InputData input = new()
            {
                Costs = costs
            };

            // when
            var result = await _engine.CalculateAsync(input);

            // then
            Assert.NotNull(result);
            Assert.False(result.IsError);
            Assert.Null(result.Reason);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data.Pairs);
        }
    }
}