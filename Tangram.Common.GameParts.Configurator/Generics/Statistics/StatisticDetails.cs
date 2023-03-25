namespace Solver.Tangram.AlgorithmDefinitions.Generics.Statistics
{
    public class StatisticDetails
    {
        private int maximalAmountOfIterations;
        private int currentIteration;

        public StatisticDetails(string algorithmId)
        {
            AlgorithmId = algorithmId;
        }

        public StatisticDetails(
            string algorithmId,
            int maximalAmountOfIterations,
            int currentIteration)
            : this(algorithmId)
        {
            this.maximalAmountOfIterations = maximalAmountOfIterations;
            this.currentIteration = currentIteration;
        }

        public string AlgorithmId { private set; get; }

        public int MeanExecutionTimeOfIterationInMiliseconds { get; set; }

        // zero when not obtained
        public int EstimatedExecutionTimeInMiliseconds => (maximalAmountOfIterations - currentIteration) * MeanExecutionTimeOfIterationInMiliseconds;
    }
}
