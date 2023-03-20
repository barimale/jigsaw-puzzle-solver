using Genetic.Algorithm.Tangram.AlgorithmSettings.Settings;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.AlgorithmSettings.TemplatesFactory
{
    public class AlgorithmSettingsFactory
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
