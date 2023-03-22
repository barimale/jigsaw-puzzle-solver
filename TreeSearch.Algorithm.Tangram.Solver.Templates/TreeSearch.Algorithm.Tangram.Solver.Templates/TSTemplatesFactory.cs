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
            IList<BlockBase> blocks)
        {
            return
                new BreadthFirstTreeSearchAlgorithm(
                    board,
                    blocks
                );
        }

        public DepthFirstTreeSearchAlgorithm CreateDepthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks)
        {
            return
                new DepthFirstTreeSearchAlgorithm(
                    board,
                    blocks
                );
        }

        public OneRootParallelDepthFirstTreeSearchAlgorithm CreateOneRootParallelDepthFirstTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks)
        {
            return
                new OneRootParallelDepthFirstTreeSearchAlgorithm(
                    board,
                    blocks
                );
        }

        public PilotTreeSearchAlgorithm CreatePilotTreeSearchAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks)
        {
            return
                new PilotTreeSearchAlgorithm(
                    board,
                    blocks
                );
        }
    }
}
