using Genetic.Algorithm.Tangram.AlgorithmSettings;
using Genetic.Algorithm.Tangram.GameParts;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Configurator
{
    public class GamePartConfiguratorBuilder
    {
        private GeneticAlgorithm? algorithm;
        private GamePartsConfigurator? parts;

        public static GamePartsFactory AvalaibleGameSets => new GamePartsFactory(); // without algorithm
        public static AlgorithmSettingsFactory AvalaibleTunedAlgorithms => new AlgorithmSettingsFactory();

        public GamePartConfiguratorBuilder WithAlgorithm(GeneticAlgorithm ga)
        {
            algorithm = ga;

            return this;
        }

        public GamePartConfiguratorBuilder WithGamePartsConfigurator(GamePartsConfigurator gameParts)
        {
            parts = gameParts;

            return this;
        }

        public GamePartsConfigurator Build()
        {
            if (parts == null)
                throw new Exception("The parts cannot be null.");

            if (algorithm == null)
                throw new Exception("The algorithm cannot be null.");

            var gameConfiguration = new GamePartsConfigurator(
                parts.Blocks,
                parts.Board,
                algorithm,
                parts.AllowedAngles);

            var isConfigurationValid = gameConfiguration.Validate();

            if (!isConfigurationValid)
                throw new Exception("The summarized block definitions area cannot be bigger than the board area.");

            return gameConfiguration;
        }
    }
}
