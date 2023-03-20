using Genetic.Algorithm.Tangram.GA.Solver.Templates;
using GeneticSharp;
using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Elements;
using Tangram.GameParts.Logic.GameParts;

namespace Solver.Tangram.Game.Logic
{
    public class GameConfiguratorBuilder
    {
        private IExecutableAlgorithm? algorithm;
        private GameSet? parts;

        public static GameSetFactory AvalaibleGameSets
            => new GameSetFactory();

        public static GATemplatesFactory AvalaibleGATunedAlgorithms
            => new GATemplatesFactory();

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

        public MultiAlgorithmGameConfiguratorBuilder WithManyAlgorithms()
        {
            return new MultiAlgorithmGameConfiguratorBuilder(this);
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
