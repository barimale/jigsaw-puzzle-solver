using Solver.Tangram.AlgorithmDefinitions.Generics;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Logic.GameParts;

namespace Solver.Tangram.Game.Logic
{
    public class Game : IRunGame
    {
        private readonly GameSet gameSet;

        private Game(GameSet gameSet)
        {
            this.gameSet = gameSet;
        }

        public Game(GameSet gameSet, IExecutableAlgorithm algorithm)
            : this(gameSet)
        {
            Algorithm = algorithm;
        }

        public Game(GameSet gameSet, IExecutableMultiAlgorithm multialgorithm)
            : this(gameSet)
        {
            Multialgorithm = multialgorithm;
        }

        public GameSet GameSet => gameSet;

        public IExecutableAlgorithm? Algorithm { get; private set; }
        public IExecutableMultiAlgorithm? Multialgorithm { get; private set; }

        public async Task<Tout?> RunGameAsync<Tout>(CancellationToken ct = default)
            where Tout : class
        {
            if (Algorithm != null)
                return (await Algorithm.ExecuteAsync(ct)) as Tout;

            if (Multialgorithm != null)
                return (await Multialgorithm.ExecuteManyAsync(ct)) as Tout;

            throw new SystemException();
        }
    }
}