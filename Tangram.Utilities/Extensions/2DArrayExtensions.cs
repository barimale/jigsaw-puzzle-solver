using NetTopologySuite.Algorithm;

namespace Algorithm.Tangram.Common.Extensions
{
    // it is dedicated for classic arrays, for which lengths of columns
    // are the same and lengths of rows are the same
    public static class _2DArrayExtensions
    {
        public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }

        public static int ColumnsCount<T>(this T[,] matrix)
        {
            var firstRow = matrix.GetRow(0);

            if (firstRow == null)
                throw new Exception("Array incorrectly defined");

            return firstRow.Length;
        }

        public static int RowsCount<T>(this T[,] matrix)
        {
            var firstColumn = matrix.GetColumn(0);

            if (firstColumn == null)
                throw new Exception("Array incorrectly defined");

            return firstColumn.Length;
        }
    }
}
