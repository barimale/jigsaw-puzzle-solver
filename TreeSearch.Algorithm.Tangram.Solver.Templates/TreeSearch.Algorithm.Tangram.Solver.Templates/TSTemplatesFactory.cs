using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace TreeSearch.Algorithm.Tangram.Solver.Templates
{
    public class TSTemplatesFactory
    {
        // TODO: allowed angels need to be use also in case allowedLocation are calculated
        public BreadthFirstTreeSearchAlgorithm CreateBreadthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int? maxDegreeOfParallelism = null)
        {
            return
                new BreadthFirstTreeSearchAlgorithm(
                    board,
                    blocks,
                    maxDegreeOfParallelism
                );
        }

        public DepthFirstTreeSearchAlgorithm CreateDepthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int? maxDegreeOfParallelism = null)
        {
            return
                new DepthFirstTreeSearchAlgorithm(
                    board,
                    blocks,
                    maxDegreeOfParallelism
                );
        }

        public BinaryDepthFirstTreeSearchAlgorithm CreateBinaryDepthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int? maxDegreeOfParallelism = null)
        {
            return
                new BinaryDepthFirstTreeSearchAlgorithm(
                    board,
                    blocks,
                    maxDegreeOfParallelism
                );
        }

        public OneRootParallelDepthFirstTreeSearchAlgorithm CreateOneRootParallelDepthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int? maxDegreeOfParallelism = null)
        {
            return
                new OneRootParallelDepthFirstTreeSearchAlgorithm(
                    board,
                    blocks,
                    maxDegreeOfParallelism
                );
        }

        public PilotTreeSearchAlgorithm CreatePilotTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int? maxDegreeOfParallelism = null)
        {
            return
                new PilotTreeSearchAlgorithm(
                    board,
                    blocks,
                    maxDegreeOfParallelism
                );
        }
    }
}
