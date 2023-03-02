using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts
{
    public class GamePartsConfigurator
    {
        // TODO: provide the allowedLocations generator for each block 
        // random only specific geometry, so just one int
        // implement thread safe randomizer
        // and use tplOperator together with multithreading where is possible
        // refactor first 
        // think how many times execute the algorithm,
        // maybe the reinformcent then time termination
        public BoardShapeBase Board { private set; get; }
        public IList<BlockBase> Blocks { private set; get; }
        public int[] AllowedAngles { get; private set; }

        public GeneticAlgorithm Algorithm { private set; get; }

        public GamePartsConfigurator(
            IList<BlockBase> blocks,
            BoardShapeBase boardShape,
            GeneticAlgorithm algorithm,
            int[] allowedAngles
        )
        {
            Board = boardShape;
            Blocks = blocks;
            Algorithm = algorithm;
            AllowedAngles = allowedAngles;
        }

        public bool Validate()
        {
            var summarizeBlocksArea = Blocks.ToList().Sum(p => p.Area);
            var boardArea = Board.Area;
            return boardArea >= summarizeBlocksArea;
        }
    }
}
