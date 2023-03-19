using Genetic.Algorithm.Tangram.Configurator.Generics;
using Genetic.Algorithm.Tangram.Configurator.Generics.SingleAlgorithm;

namespace Genetic.Algorithm.Tangram.Configurator
{
    // TODO check it step by step
    public class MultiAlgorithmGameConfiguratorBuilder
    {
        private readonly GameConfiguratorBuilder gameConfiguratorBuilder;

        private ExecutionMode? algorithmExecutionMode;
        private List<IExecutableAlgorithm> algorithms;

        public MultiAlgorithmGameConfiguratorBuilder(
            GameConfiguratorBuilder gameConfiguratorBuilder)
        {
            this.algorithms = new List<IExecutableAlgorithm>();
            this.gameConfiguratorBuilder = gameConfiguratorBuilder;
        }

        public MultiAlgorithmGameConfiguratorBuilder WithExecutionMode(
            ExecutionMode executionMode)
        {
            this.algorithmExecutionMode = executionMode;

            return this;
        }

        public MultiAlgorithmGameConfiguratorBuilder WithAlgorithms(IList<IExecutableAlgorithm> algorithms)
        {
            this.algorithms.AddRange(algorithms);

            return this;
        }

        public MultiAlgorithmGameConfiguratorBuilder WithAlgorithms(params IExecutableAlgorithm[] algorithms)
        {
            this.algorithms.AddRange(algorithms.AsEnumerable());

            return this;
        }

        public Game Build()
        {
            if (this.algorithmExecutionMode == null)
                throw new Exception("The algorithmExecutionMode cannot be null.");

            if (this.algorithms.Count == 0)
                throw new Exception("The algorithms count cannot be zero.");

            var singleGame = gameConfiguratorBuilder
                .Build();

            var multiAlgorithm = new MultiAlgorithm<IExecutableAlgorithm>(
                    this.algorithmExecutionMode.Value,
                    this.algorithms);

            var game = new Game(
                    singleGame.GameSet,
                    multiAlgorithm);

            return game;            
        }
    }
}