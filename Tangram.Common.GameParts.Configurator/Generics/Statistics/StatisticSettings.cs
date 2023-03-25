namespace Solver.Tangram.AlgorithmDefinitions.Generics.Statistics
{
    public class StatisticSettings
    {
        public StatisticSettings(int measurePeriodInCycles)
        {
            MeasurePeriodInCycles = measurePeriodInCycles;
        }

        public int MeasurePeriodInCycles { private set; get; }
    }
}