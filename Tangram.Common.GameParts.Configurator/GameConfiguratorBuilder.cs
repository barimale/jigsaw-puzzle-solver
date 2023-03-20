using Generic.Algorithm.Tangram.GameParts;
using Generic.Algorithm.Tangram.GameParts.Elements;
using Genetic.Algorithm.Tangram.AlgorithmSettings.TemplatesFactory;
using GeneticSharp;
using Solver.Tangram.Configurator.Algorithms;
using Solver.Tangram.Configurator.Generics.SingleAlgorithm;

namespace Solver.Tangram.Configurator
{
    public class GameConfiguratorBuilder
    {
        private IExecutableAlgorithm? algorithm;
        private GameSet? parts;

        public static GameSetFactory AvalaibleGameSets
            => new GameSetFactory();

        public static AlgorithmSettingsFactory AvalaibleGATunedAlgorithms
            => new AlgorithmSettingsFactory();

        public GameConfiguratorBuilder WithAlgorithm(GeneticAlgorithm ga)
        {
            algorithm = new ExecutableGeneticAlgorithm(ga);

            return this;
        }

        public GameConfiguratorBuilder WithAlgorithm(IExecutableAlgorithm algorithm)
        {
            this.algorithm = algorithm;

            return this;
        }

        public GameConfiguratorBuilder WithGamePartsConfigurator(GameSet gameParts)
        {
            parts = gameParts;

            return this;
        }

        public Game Build()
        {
            if (parts == null)
                throw new Exception("The parts cannot be null.");

            if (algorithm == null)
                throw new Exception("The algorithm cannot be null.");

            var gameConfiguration = new GameSet(
                parts.Blocks,
                parts.Board,
                parts.AllowedAngles);

            var isConfigurationValid = gameConfiguration.Validate();

            if (!isConfigurationValid)
                throw new Exception("The summarized block definitions area cannot be bigger than the board area.");

            var game = new Game(
                gameConfiguration,
                algorithm);

            return game;
        }
    }
}
