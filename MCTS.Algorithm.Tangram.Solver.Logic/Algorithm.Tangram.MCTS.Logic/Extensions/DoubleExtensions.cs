namespace Algorithm.Tangram.TreeSearch.Logic.Extensions
{
    public static class DoubleExtensions
    {
        public static int ConvertToInt32(this double d)
        {
            if (d <= int.MinValue)
                return int.MinValue;
            else if (d >= int.MaxValue)
                return int.MaxValue;
            else
                return (int)d;
        }
    }
}
