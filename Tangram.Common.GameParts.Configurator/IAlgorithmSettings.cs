using GeneticSharp;
using Tangram.GameParts.Logic.Block;
using Tangram.GameParts.Logic.Board;

namespace Solver.Tangram.AlgorithmDefinitions
{
    public interface IAlgorithmSettings
    {
        GeneticAlgorithm CreateNew(BoardShapeBase board, IList<BlockBase> blocks, int[] allowedAngles);
    }
}