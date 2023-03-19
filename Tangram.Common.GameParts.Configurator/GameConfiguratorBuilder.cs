using Genetic.Algorithm.Tangram.AlgorithmSettings;
using Genetic.Algorithm.Tangram.Configurator.Algorithms;
using Genetic.Algorithm.Tangram.Configurator.Generics.SingleAlgorithm;
using Genetic.Algorithm.Tangram.GameParts;
using Genetic.Algorithm.Tangram.GameParts.Elements;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Configurator
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
