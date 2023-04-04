using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using TreesearchLib;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions
{
    public class OneRootParallelDepthFirstTreeSearchAlgorithm : Algorithm<FindFittestSolution>, IExecutableAlgorithm
    {
        private const string NAME = "OneRootParallelDepthFirstTreeSearchAlgorithm";
        private const int ROOT_HAS_TO_BE_TREATED_AS_SINGLE_SO_SKIP_IT = 1;

        public OneRootParallelDepthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks)
            : base(new FindFittestSolution(board, blocks))
        {
           // intentionally left blank
        }

        public override string Name => NAME;

        public override async Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            try
            {
                // settings
                var board = algorithm.Board;
                var allBlocks = algorithm.Blocks.ToArray();

                // roots - first block with n allowed locations
                var rootBlock = allBlocks[0];

                // transformed to n roots with 1 allowed location
                var rootBlocks = rootBlock.AllowedLocations.Select(p =>
                {
                    var cloned = rootBlock.Clone();
                    cloned.SetAllowedLocations(new NetTopologySuite.Geometries.Geometry[1] { p });

                    return cloned;
                }).ToArray();

                // based on above n algorithms are started in parallel 
                // having just 1 root element
                var rootedAlgorithms = rootBlocks.Select(
                    p => new FindFittestSolution(
                        board,
                        ModifyRootElementOfArray(p, allBlocks)))
                    .Select(async pp => await ExecuteAlgorithmAsync(pp, ct));

                var results = rootedAlgorithms
                    .ToAsyncEnumerable()
                    .WhereAwaitWithCancellation(async (x, ct) => await MeetsCriteria(x, 0));

                var firstSolvedAlgorithm = results.FirstOrDefaultAsync();
                var solution = firstSolvedAlgorithm.Result.Result;

                return new AlgorithmResult()
                {
                    Fitness = solution != null && solution.Quality.HasValue ? solution.Quality.Value.ToString() : string.Empty,
                    Solution = solution!,
                    IsError = solution == null || !solution.Quality.HasValue
                };

            }
            catch (OperationCanceledException oce)
            {
                return new AlgorithmResult()
                {
                    Fitness = string.Empty,
                    Solution = null,
                    IsError = true,
                    ErrorMessage = oce.Message
                };
            }
            catch (Exception ex)
            {
                return new AlgorithmResult()
                {
                    Fitness = string.Empty,
                    Solution = null,
                    IsError = true,
                    ErrorMessage = ex.Message
                };
            }
        }

        private async Task<FindFittestSolution> ExecuteAlgorithmAsync(FindFittestSolution pp, CancellationToken ct)
        {
            FindFittestSolution? result;
            switch (algorithm.Blocks.Count)
            {
                case int n when n < 3:
                    result = algorithm.DepthFirst(
                        token: ct,
                         callback: (state, control, quality) =>
                         {
                             base.HandleQualityCallback(state);
                             base.CurrentIteration = state.VisitedNodes;
                             base.HandleExecutionEstimationCallback(
                                 state,
                                 pp.Blocks.Skip(ROOT_HAS_TO_BE_TREATED_AS_SINGLE_SO_SKIP_IT).Select(p => p.AllowedLocations.Length)
                                     .Aggregate(1, (x, y) => x * y));
                         });
                    break;
                case int n when n < 5:
                    result = await algorithm.DepthFirstAsync(
                        token: ct,
                         callback: (state, control, quality) =>
                         {
                             base.HandleQualityCallback(state);
                             base.CurrentIteration = state.VisitedNodes;
                             base.HandleExecutionEstimationCallback(
                                 state,
                                 pp.Blocks.Skip(ROOT_HAS_TO_BE_TREATED_AS_SINGLE_SO_SKIP_IT).Select(p => p.AllowedLocations.Length)
                                     .Aggregate(1, (x, y) => x * y));
                         });
                    break;
                default:
                    result = await algorithm.ParallelDepthFirstAsync(
                        token: ct,
                        callback: (state, control, quality) =>
                        {
                            base.HandleQualityCallback(state);
                            base.CurrentIteration = state.VisitedNodes;
                            base.HandleExecutionEstimationCallback(
                                state,
                                pp.Blocks.Skip(ROOT_HAS_TO_BE_TREATED_AS_SINGLE_SO_SKIP_IT).Select(p => p.AllowedLocations.Length)
                                    .Aggregate(1, (x, y) => x * y));
                        });
                    break;
            }

            return result;
        }

        private BlockBase[] ModifyRootElementOfArray(BlockBase replaceBy, BlockBase[] array)
        {
            if (array == null)
                throw new ArgumentException("array cannot be null");

            if (array.Length < 1)
                throw new ArgumentException("array cannot be empty");

            if (replaceBy == null)
                throw new ArgumentException("replaceBy cannot be null");

            array[0] = replaceBy;

            return array;
        }

        private async ValueTask<bool> MeetsCriteria(
            Task<FindFittestSolution> actualValue,
            int expectedReturnedValue)
        {
            if (actualValue.Result.Quality.HasValue 
                && actualValue.Result.Quality.Value.Value == expectedReturnedValue)
                return true;

            return false;
        }
    }
}
