using Genetic.Algorithm.Tangram.GA.Solver.Templates.Settings;
using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Genetic.Algorithm.Tangram.GA.Solver.Templates
{
    public class GATemplatesFactory
    {
        public ExecutableGeneticAlgorithm CreateBigBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles,
            int? maxDegreeOfParallelism = null)
        {
            return
                new ExecutableGeneticAlgorithm(
                    new BigBoardAlgorithmSettings()
                        .CreateNew(
                        board,
                        blocks,
                        allowedAngles),
                    maxDegreeOfParallelism
                    );
        }

        public ExecutableGeneticAlgorithm CreateMediumBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles,
            int? maxDegreeOfParallelism = null)
        {
            return 
                new ExecutableGeneticAlgorithm(
                    new MediumBoardAlgorithmSettings()
                        .CreateNew(
                        board,
                        blocks,
                        allowedAngles),
                    maxDegreeOfParallelism
                    );
        }

        public ExecutableGeneticAlgorithm CreateSimpleBoardSettings(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles,
            int? maxDegreeOfParallelism = null)
        {
            return
                new ExecutableGeneticAlgorithm(
                    new SimpleBoardAlgorithmSettings()
                        .CreateNew(
                        board,
                        blocks,
                        allowedAngles),
                    maxDegreeOfParallelism
                    );
        }
    }
}
