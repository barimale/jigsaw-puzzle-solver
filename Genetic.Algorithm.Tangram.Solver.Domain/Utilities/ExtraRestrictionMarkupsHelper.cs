using Algorithm.Tangram.Common.Extensions;
using System.Text;

namespace Tangram.GameParts.Logic.Utilities
{
    public static class ExtraRestrictionMarkupsHelper
    {
        public static string MeshSideToString<T>(object[,] mesh, string markupToClean = "&&&")
        {
            var columns = mesh.ColumnsCount();
            var rows = mesh.RowsCount();

            // TODO: maybe use the fieldtransformer here or create new one 
            var builder = new StringBuilder();
            for (var rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                var currentRow = mesh.GetRow(rowIndex);
                var currentRowAsStrings = currentRow.Select(p => ((T)p).ToString()).ToArray();
                var currentRowAsString = string.Join(' ', currentRowAsStrings).Replace(markupToClean, "  ");
                // TODO: replace not field tag by empty
                builder.Append(currentRowAsString);
                if (rowIndex < rows - 1) builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
