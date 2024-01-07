namespace Solver.Tangram.AlgorithmDefinitions.Generics.Statistics
{
    // WIP
    public class StatisticDetails
    {
        private long maximalAmountOfIterations;
        private long currentIteration;

        public StatisticDetails(string algorithmId)
        {
            AlgorithmId = algorithmId;
        }

        public StatisticDetails(
            string algorithmId,
            long maximalAmountOfIterations,
            long currentIteration)
            : this(algorithmId)
        {
            this.maximalAmountOfIterations = maximalAmountOfIterations;
            this.currentIteration = currentIteration;
        }

        public string AlgorithmId { private set; get; }

        public long MeanExecutionTimeOfIterationInMiliseconds { get; set; }

        // zero when not obtained
        public long EstimatedExecutionTimeInMiliseconds => (long)(maximalAmountOfIterations - currentIteration) * MeanExecutionTimeOfIterationInMiliseconds;
    }
}
