using Genetic.Algorithm.Tangram.Solver.Domain.Board;

namespace Genetic.Algorithm.Tangram.Solver.Domain.Generators
{
    public class FieldsGenerator
    {
        public List<BoardFieldDefinition> GenerateFields(
            int scaleFactor,
            double fieldHeight,
            double fieldWidth,
            int boardColumnsCount,
            int boardRowsCount)
        {
            var fields = new List<BoardFieldDefinition>();

            for (int c = 0; c < boardColumnsCount; c++)
            {
                for (int r = 0; r < boardRowsCount; r++)
                {
                    fields.Add(
                        new BoardFieldDefinition(c, r, fieldWidth, fieldHeight, true, scaleFactor)
                    );
                }
            }

            return fields;
        }
    }
}
