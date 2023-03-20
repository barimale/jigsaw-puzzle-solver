using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Logic.GameParts;

namespace Solver.Tangram.Game.Logic
{
    public class MultiAlgorithmGameConfiguratorBuilder
    {
        private readonly IExecutableAlgorithm? alreadyDefinedAlgorithm;
        private readonly GameSet alreadyDefinedParts;

        private ExecutionMode? algorithmExecutionMode;
        private List<IExecutableAlgorithm> algorithms;

        public MultiAlgorithmGameConfiguratorBuilder(
            GameSet alreadyDefinedParts,
            IExecutableAlgorithm? alreadyDefinedAlgorithm)
        {
            algorithms = alreadyDefinedAlgorithm != null
                ? new List<IExecutableAlgorithm>() { alreadyDefinedAlgorithm }
                : new List<IExecutableAlgorithm>();

            this.alreadyDefinedParts = alreadyDefinedParts;
        }

        public MultiAlgorithmGameConfiguratorBuilder WithExecutionMode(
            ExecutionMode executionMode)
        {
            algorithmExecutionMode = executionMode;

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
            if (algorithmExecutionMode == null)
                throw new Exception("The algorithmExecutionMode cannot be null.");

            if (algorithms.Count == 0)
                throw new Exception("The algorithms count cannot be zero.");


            var multiAlgorithm = new MultiAlgorithm(
                    algorithmExecutionMode.Value,
                    algorithms);

            var game = new Game(
                    alreadyDefinedParts,
                    multiAlgorithm);

            return game;
        }
    }
}