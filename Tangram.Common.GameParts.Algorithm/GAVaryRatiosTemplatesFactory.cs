using Genetic.Algorithm.Tangram.GA.Solver.Templates.Settings;
using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions.ExecutableVaryRatiosGeneticAlgorithm;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Genetic.Algorithm.Tangram.GA.Solver.Templates
{
    public class GAVaryRatiosTemplatesFactory
    {
        public ExecutableVaryRatiosGeneticAlgorithm CreateBigBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles,
            int maximalGenerationAmount)
        {
            return
                new ExecutableVaryRatiosGeneticAlgorithm(
                    new BigBoardAlgorithmSettings()
                        .CreateNew(
                        board,
                        blocks,
                        allowedAngles),
                    maximalGenerationAmount);
        }

        public ExecutableVaryRatiosGeneticAlgorithm CreateMediumBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles,
            int maximalGenerationAmount)
        {
            return 
                new ExecutableVaryRatiosGeneticAlgorithm(
                    new MediumBoardAlgorithmSettings()
                        .CreateNew(
                        board,
                        blocks,
                        allowedAngles),
                        maximalGenerationAmount
                    );
        }

        public ExecutableVaryRatiosGeneticAlgorithm CreateSimpleBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles,
            int maximalGenerationAmount)
        {
            return
                new ExecutableVaryRatiosGeneticAlgorithm(
                    new SimpleBoardAlgorithmSettings()
                        .CreateNew(
                        board,
                        blocks,
                        allowedAngles),
                        maximalGenerationAmount
                    );
        }
    }
}