using Genetic.Algorithm.Tangram.GA.Solver.Templates;
using GeneticSharp;
using Solver.Tangram.AlgorithmDefinitions.AlgorithmsDefinitions;
using Solver.Tangram.AlgorithmDefinitions.Generics.SingleAlgorithm;
using Tangram.GameParts.Elements;
using Tangram.GameParts.Logic.GameParts;
using TreeSearch.Algorithm.Tangram.Solver.Templates;

namespace Solver.Tangram.Game.Logic
{
    public class GameBuilder
    {
        private IExecutableAlgorithm? algorithm;
        private GameSet? parts;

        public static GameSetFactory AvalaibleGameSets
            => new GameSetFactory();

        public static GATemplatesFactory AvalaibleGATunedAlgorithms
            => new GATemplatesFactory();

        public static GAVaryRatiosTemplatesFactory AvalaibleGAVaryRatiosTunedAlgorithms
            => new GAVaryRatiosTemplatesFactory();

        public static TSTemplatesFactory AvalaibleTSTemplatesAlgorithms
            => new TSTemplatesFactory();

        public GameBuilder WithAlgorithm(IExecutableAlgorithm algorithm)
        {
            this.algorithm = algorithm;

            return this;
        }

        public GameBuilder WithGamePartsConfigurator(GameSet gameParts)
        {
            parts = gameParts;

            return this;
        }

        public MultiAlgorithmGameConfiguratorBuilder WithManyAlgorithms()
        {
            if (parts == null)
                throw new Exception("The parts cannot be null.");

            return new MultiAlgorithmGameConfiguratorBuilder(
                this.parts,
                this.algorithm);
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
