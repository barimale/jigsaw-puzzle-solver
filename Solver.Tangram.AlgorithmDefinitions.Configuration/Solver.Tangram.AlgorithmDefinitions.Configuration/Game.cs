using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Logic.GameParts;

namespace Solver.Tangram.Game.Logic
{
    public class Game : IRunGame
    {
        private readonly GameSet gameSet;

        public event EventHandler OnExecutionEstimationReady;

        private Game(GameSet gameSet)
        {
            this.gameSet = gameSet;
        }

        public Game(GameSet gameSet, IExecutableAlgorithm algorithm)
            : this(gameSet)
        {
            Algorithm = algorithm;
            Algorithm.OnExecutionEstimationReady += Algorithm_OnExecutionEstimationReady; ;
        }

        public Game(GameSet gameSet, IExecutableMultiAlgorithm multialgorithm)
            : this(gameSet)
        {
            Multialgorithm = multialgorithm;
            Multialgorithm.OnExecutionEstimationReady += Algorithm_OnExecutionEstimationReady;
        }

        public GameSet GameSet => gameSet;
        public bool HasManyAlgorithms => Multialgorithm != null && Algorithm == null;

        public IExecutableAlgorithm? Algorithm { get; private set; }
        public IExecutableMultiAlgorithm? Multialgorithm { get; private set; }

        public async Task<Tout> RunGameAsync<Tout>(CancellationToken ct = default)
            where Tout : class
        {
            ct.ThrowIfCancellationRequested();

            if (Algorithm != null)
                return (await Algorithm.ExecuteAsync(ct)) as Tout;

            if (Multialgorithm != null)
                return (await Multialgorithm.ExecuteManyAsync(ct)) as Tout;

            throw new SystemException();
        }

        public string GetAlgorithmNameBy(string id)
        {
            if (this.HasManyAlgorithms)
            {
                this.Multialgorithm.Algorithms.TryGetValue(id, out var algorithm);
                return algorithm != null ? algorithm.Name : string.Empty;
            }
            else
            {
                return this.Algorithm.Name;
            }
        }

        private void Algorithm_OnExecutionEstimationReady(object? sender, EventArgs e)
        {
            if (OnExecutionEstimationReady != null)
            {
                OnExecutionEstimationReady.Invoke(sender, e);
            }
        }
    }
}