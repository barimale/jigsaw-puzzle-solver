using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Display
{
    public static class AgnosticConsoleColouredDisplay
    {
        // no console is defined
        // assumtion: string or strings array are allowed to be returned

        // TODO:
        public static string[] DisplaySolution(
            IList<BlockBase> solution,
            BoardShapeBase board)
        {
            return new string[0] { };
        }
    }
}
