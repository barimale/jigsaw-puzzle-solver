﻿using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using TreesearchLib;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions
{
    public class OneRootParallelDepthFirstTreeSearchAlgorithm : Algorithm<FindFittestSolution>, IExecutableAlgorithm
    {
        public OneRootParallelDepthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks)
            : base(new FindFittestSolution(board, blocks))
        {
            // intentionally left blank
        }

        public event EventHandler QualityCallback;

        public override async Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default)
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
                .Select(pp => pp.DepthFirstAsync(
                    token: ct,
                    callback: (state, control, quality) => HandleQualityCallback(state)));

            var results = await Task.WhenAll(rootedAlgorithms);

            var onlyCompletedResults = results
                .Where(pp => pp.Quality.HasValue && pp.Quality.Value.Value == 0);

            var firstSolution = onlyCompletedResults.FirstOrDefault();

            return new AlgorithmResult()
            {
                Fitness = firstSolution != null && firstSolution.Quality.HasValue ? firstSolution.Quality.Value.ToString() : string.Empty,
                Solution = firstSolution!,
                IsError = firstSolution == null || !firstSolution.Quality.HasValue
            };
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

        private void HandleQualityCallback(
            ISearchControl<FindFittestSolution, Minimize> state)
        {
            if (QualityCallback != null)
            {
                QualityCallback.Invoke(state, null);
            }
            else
            {
                // do nothing
            }
        }
    }
}
