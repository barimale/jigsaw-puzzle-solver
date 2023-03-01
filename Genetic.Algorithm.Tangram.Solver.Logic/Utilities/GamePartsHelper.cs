using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Utilities
{
    public static class GamePartsHelper
    {
        public static List<BoardFieldDefinition> GenerateFields(
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
