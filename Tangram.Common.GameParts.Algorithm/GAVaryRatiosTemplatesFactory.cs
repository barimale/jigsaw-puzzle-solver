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
            int maximalGenerationAmount,
            int? maxDegreeOfParallelism = null)
        {
            return
                new ExecutableVaryRatiosGeneticAlgorithm(
                    new BigBoardAlgorithmSettings()
                        .CreateNew(
                        board,
                        blocks,
                        allowedAngles),
                    maximalGenerationAmount,
                    maxDegreeOfParallelism
                );
        }

        public ExecutableVaryRatiosGeneticAlgorithm CreateMediumBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles,
            int maximalGenerationAmount,
            int? maxDegreeOfParallelism = null)
        {
            return 
                new ExecutableVaryRatiosGeneticAlgorithm(
                    new MediumBoardAlgorithmSettings()
                        .CreateNew(
                        board,
                        blocks,
                        allowedAngles),
                        maximalGenerationAmount,
                        maxDegreeOfParallelism
                    );
        }

        public ExecutableVaryRatiosGeneticAlgorithm CreateSimpleBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles,
            int maximalGenerationAmount,
            int? maxDegreeOfParallelism = null)
        {
            return
                new ExecutableVaryRatiosGeneticAlgorithm(
                    new SimpleBoardAlgorithmSettings()
                        .CreateNew(
                        board,
                        blocks,
                        allowedAngles),
                        maximalGenerationAmount,
                        maxDegreeOfParallelism
                    );
        }
    }
}