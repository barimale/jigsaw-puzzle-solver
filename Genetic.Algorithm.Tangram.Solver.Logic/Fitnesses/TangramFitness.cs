using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses.Services;
using GeneticSharp;
using System.Collections.Immutable;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Fitness
{
    public class TangramFitness : IFitness
    {
        private FitnessService fitnessService = new FitnessService();

        private BoardShapeBase boardShapeDefinition;
        private IList<BlockBase> blocks;

        public TangramFitness(
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
                .Select(pp => pp.Polygon)
                .ToImmutableList();

            var diff = fitnessService.Evaluate(
                evaluatedGeometry.ToArray(),
                boardShapeDefinition);

            return diff;
        }
    }
}
