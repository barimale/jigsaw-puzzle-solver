using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace OrToolsSAT.Algorithm.Tangra.Solver.Templates
{
    public class OrToolsTemplatesFactory
    {
        public OrToolsSatAlgorithm CreateOrToolsSatAlgorithm(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int? maxDegreeOfParallelism = null)
        {
            return
                new OrToolsSatAlgorithm(
                    board,
                    blocks,
                    maxDegreeOfParallelism
                );
        }
    }
}
