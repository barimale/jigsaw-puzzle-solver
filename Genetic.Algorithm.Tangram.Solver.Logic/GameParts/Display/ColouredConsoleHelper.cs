using System.Text;

namespace Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Display
{
    public static class ColouredConsoleHelper
    {
        public static string ToMatrixString(this int[] vector, int rowsAmount, int columnsAmount)
        {
            var builder = new StringBuilder();

            for (int ii = 0; ii < columnsAmount; ii++)
            {
                var subvector = vector.Skip(ii * rowsAmount).Take(rowsAmount);
                foreach (var item in subvector)
                {
                    builder.Append(item);
                    builder.Append(' ');
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }

        public static List<Tuple<int, int, string, ConsoleColor>> ToSpecialWithColor(this int[] vector, int rowsAmount, int columnsAmount)
        {
            var list = new List<Tuple<int, int, string, ConsoleColor>>();

            for (int ii = 0; ii < columnsAmount; ii++)
            {
                var subvector = vector.Skip(ii * rowsAmount).Take(rowsAmount);
                var counter = 0;
                foreach (var item in subvector)
                {
                    var (special, color) = ColouredConsoleHelper.PrintChar(item);
                    list.Add(Tuple.Create(ii, counter, special, color));
                }
                list.Add(Tuple.Create(-1, -1, string.Empty, ConsoleColor.Black));
            }

            return list;
        }

        public static void PrintLegend(
            int lower, 
            int upper, 
            string? additionalInfo = null, 
            double[]? masses = null
        )
        {
            Console.ResetColor();
            Console.WriteLine(additionalInfo != null ? $"Legend({additionalInfo}):" : "Legend");

            for (int i = lower; i < upper + 1; i++)
            {
                var (special, color) = ColouredConsoleHelper.PrintChar(i);
                var descriptionRow = masses != null ?
                    special + " - " + i + " with mass: " + masses[i - lower].ToString()
                    : special + " - " + i;

                Console.ForegroundColor = color;
                Console.WriteLine(descriptionRow);
            }
        }

        public static void PrintLineBreaks(int count)
        {
            if (count > 3) throw new Exception("Max count supported is equal to 3 inclusive.");

            for (int i = count; i > 0; i--)
            {
                Console.WriteLine(i % 2 == 0 ? "--------------------------------------------------" : "");
            }
        }

        public static Tuple<string, ConsoleColor> PrintChar(int colorNumber)
        {
            char special = 'ø';

            return Tuple.Create(
                special.ToString(),
                Convert(colorNumber));
        }

        // TODO:
        private static ConsoleColor Convert(int colorNumber)
        {
            switch (colorNumber)
            {
                case 0:
                    return ConsoleColor.Green;
                case 1:
                    return ConsoleColor.Blue;
                case 2:
                    return ConsoleColor.Red;
                case 3:
                    return ConsoleColor.Yellow;
                case 4:
                    return ConsoleColor.Magenta;
                case 5:
                    return ConsoleColor.DarkYellow;
                case 6:
                    return ConsoleColor.White;
                default:
                    return ConsoleColor.DarkGray;
            }
        }
    }
}
