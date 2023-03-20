using Genetic.Algorithm.Tangram.GA.Solver.Templates.Settings;
using GeneticSharp;
using Tangram.GameParts.Logic.Block;
using Tangram.GameParts.Logic.Board;

namespace Genetic.Algorithm.Tangram.GA.Solver.Templates
{
    public class GATemplatesFactory
    {
        public GeneticAlgorithm CreateBigBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles)
        {
            return new BigBoardAlgorithmSettings()
                .CreateNew(
                    board,
                    blocks,
                    allowedAngles);
        }

        public GeneticAlgorithm CreateMediumBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles)
        {
            return new MediumBoardAlgorithmSettings()
                .CreateNew(
                    board,
                    blocks,
                    allowedAngles);
        }

        public GeneticAlgorithm CreateSimpleBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles)
        {
            return new SimpleBoardAlgorithmSettings()
                .CreateNew(
                    board,
                    blocks,
                    allowedAngles);
        }
    }
}
