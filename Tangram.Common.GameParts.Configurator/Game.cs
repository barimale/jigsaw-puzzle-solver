using Genetic.Algorithm.Tangram.Configurator.Generics;
using Genetic.Algorithm.Tangram.Configurator.Generics.SingleAlgorithm;
using Genetic.Algorithm.Tangram.GameParts.Elements;

namespace Genetic.Algorithm.Tangram.Configurator
{
    public class Game: IRunGame
    {
        private readonly GameSet gameSet;

        private Game(GameSet gameSet)
        {
            this.gameSet = gameSet;
        }

        public Game(GameSet gameSet, IExecutableAlgorithm algorithm)
            : this(gameSet)
        {
            this.Algorithm = algorithm;
        }

        public Game(GameSet gameSet, IExecutableMultiAlgorithm multialgorithm)
            : this(gameSet)
        {
            this.Multialgorithm = multialgorithm;
        }

        public GameSet GameSet => gameSet;

        public IExecutableAlgorithm? Algorithm { get; private set; }
        public IExecutableMultiAlgorithm? Multialgorithm { get; private set; }

        public async Task<object> RunGameAsync(CancellationToken ct = default(CancellationToken))
        {
            if (Algorithm != null)
                return await Algorithm.ExecuteAsync(ct);

            if (Multialgorithm != null)
                return await Multialgorithm.ExecuteManyAsync(ct);

            throw new SystemException();
        }
    }
}