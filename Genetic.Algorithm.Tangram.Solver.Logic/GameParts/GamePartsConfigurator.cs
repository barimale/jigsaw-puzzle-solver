using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using GeneticSharp;
using NetTopologySuite.Geometries;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts
{
    public class GamePartsConfigurator
    {
        // TODO:
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

        public static string[] LocationAsStringArray(Geometry location)
        {
            // TODO
            return new string[0] { };
        }

        public string[] BlocksAsStringArray()
        {
            // TODO: implement here
            return new string[0] { } ;
        }

        public string[] BoardAsStringArray()
        {
            return Board.BoardPolygonAsStringArray();
        }

        public bool Validate()
        {
            var digits = 3;

            var summarizeBlocksArea = Math.Round(
                    Blocks.ToList().Sum(p => p.Area),
                    digits,
                    MidpointRounding.ToEven);

            var boardArea = Math.Round(
                    Board.Area,
                    digits,
                    MidpointRounding.ToEven);

            return boardArea >= summarizeBlocksArea;
        }
    }
}
