using Algorithm.Tangram.Common.Extensions;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Tangram.GameParts.Logic.Generators
{
    public class RectangularGameFieldsGenerator
    {
        public IList<BoardFieldDefinition> GenerateFields(
            int scaleFactor,
            double fieldHeight,
            double fieldWidth,
            int boardColumnsCount,
            int boardRowsCount,
            object[,]? withExtraRistrictedMarkups = null)
        {
            var isExtraRistricted = withExtraRistrictedMarkups != null
                && withExtraRistrictedMarkups.RowsCount() == boardRowsCount
                && withExtraRistrictedMarkups.ColumnsCount() == boardColumnsCount;

            var fields = new List<BoardFieldDefinition>();

            for (int c = 0; c < boardColumnsCount; c++)
            {
                for (int r = 0; r < boardRowsCount; r++)
                {
                    if (isExtraRistricted)
                    {
                        fields.Add(
                           new BoardFieldDefinition(
                               c,
                               r,
                               fieldWidth,
                               fieldHeight,
                               withExtraRistrictedMarkups[r, c],
                               true,
                               scaleFactor)
                        );
                    }
                    else
                    {
                        fields.Add(
                           new BoardFieldDefinition(
                               c,
                               r,
                               fieldWidth,
                               fieldHeight,
                               true,
                               scaleFactor)
                        );
                    }
                }
            }

            return fields;
        }
    }
}
