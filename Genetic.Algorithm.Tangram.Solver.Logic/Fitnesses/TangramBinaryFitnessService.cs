using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses.Services;
using GeneticSharp;
using System.Collections.Immutable;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses
{
    public class TangramBinaryFitnessService : IFitness
    {
        private FitnessService fitnessService = new FitnessService();

        private BoardShapeBase boardShapeDefinition;
        private IList<BlockBase> blocks;

        public TangramBinaryFitnessService(
            BoardShapeBase boardShapeDefinition,
            IList<BlockBase> blocks)
        {
            this.boardShapeDefinition = boardShapeDefinition;
            this.blocks = blocks;
        }

        public BoardShapeBase Board => boardShapeDefinition;

        public double Evaluate(IChromosome chromosome)
        {
            var solution = chromosome as TangramChromosome;

            var evaluatedGeometry = solution
                .GetGenes()
                .Select(p => p.Value as BlockBase)
                .Select(pp => pp.ToBinary(boardShapeDefinition.BoardFieldsDefinition))
                .ToImmutableList();

            double diff = fitnessService.EvaluateBinary(
                evaluatedGeometry.AsEnumerable());

            return -1f * diff;
        }

        public async Task<double> EvaluateAsync(IChromosome chromosome)
        {
            var solution = chromosome as TangramChromosome;

            var evaluatedGeometry = solution
                .GetGenes()
                .Select(p => p.Value as BlockBase)
                .Select(pp => pp.ToBinary(boardShapeDefinition.BoardFieldsDefinition))
                .ToImmutableList();

            var diff = await fitnessService.EvaluateBinaryAsync(
                evaluatedGeometry.AsEnumerable());

            return diff;
        }
    }
}
