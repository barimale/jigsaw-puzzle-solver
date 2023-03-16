namespace Algorithm.Tangram.MCTS.Logic.Extensions
{
    public static class DoubleExtensions
    {
        public static Int32 ConvertToInt32(this Double d)
        {
            if (d <= (double)Int32.MinValue)
                return Int32.MinValue;
            else if (d >= (double)Int32.MaxValue)
                return Int32.MaxValue;
            else
                return (Int32)d;
        }
    }
}
