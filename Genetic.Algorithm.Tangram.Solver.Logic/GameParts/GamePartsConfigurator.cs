using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts
{
    public class GamePartsConfigurator
    {
        public BoardShapeBase Board { private set; get; }
        public IList<BlockBase> Blocks { private set; get; }
        public GeneticAlgorithm Algorithm { private set; get; }

        public GamePartsConfigurator(
            IList<BlockBase> blocks,
            BoardShapeBase boardShape,
            GeneticAlgorithm algorithm
        )
        {
            Board = boardShape;
            Blocks = blocks;
            Algorithm = algorithm;
        }

        public bool Validate()
        {
            var summarizeBlocksArea = Blocks.ToList().Sum(p => p.Area);
            var boardArea = Board.Area;
            return boardArea >= summarizeBlocksArea;
        }
    }
}
