﻿using Algorithm.Tangram.TreeSearch.Logic;
using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using TreesearchLib;

namespace Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions
{
    public class BreadthFirstTreeSearchAlgorithm : Algorithm<FindFittestSolution>, IExecutableAlgorithm
    {
        public BreadthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks)
            : base(new FindFittestSolution(board, blocks))
        {
            // intentionally left blank
        }

        // TODO change to AlgorithmResult
        public event EventHandler QualityCallback;

        public override async Task<AlgorithmResult> ExecuteAsync(CancellationToken ct = default)
        {
            var result = await algorithm.BreadthFirstAsync(
                token: ct,
                callback: (state, control, quality) => HandleQualityCallback(state)
                );

            return new AlgorithmResult()
            {
                Fitness = result.Quality.HasValue ? result.Quality.Value.ToString() : string.Empty,
                Solution = result,
                IsError = !result.Quality.HasValue
            };
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
