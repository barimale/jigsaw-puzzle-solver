using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts
{
    public class GamePartsConfigurator
    {
        public BoardShapeBase Board { private set; get; }
        public IList<BlockBase> Blocks { private set; get; }

        public GamePartsConfigurator(
            IList<BlockBase> blocks,
            BoardShapeBase boardShape
        )
        {
            this.Board = boardShape;
            this.Blocks = blocks;
        }
    }
}
