using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.AlgorithmSettings
{
    public interface IAlgorithmSettings
    {
        GeneticAlgorithm CreateNew(BoardShapeBase board, IList<BlockBase> blocks, int[] allowedAngles);
    }
}