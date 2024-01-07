namespace Solver.Tangram.AlgorithmDefinitions.Generics.Statistics
{
    // WIP
    public class StatisticSettings
    {
        public StatisticSettings(long measurePeriodInCycles)
        {
            MeasurePeriodInCycles = measurePeriodInCycles;
        }

        public long MeasurePeriodInCycles { private set; get; }
    }
}