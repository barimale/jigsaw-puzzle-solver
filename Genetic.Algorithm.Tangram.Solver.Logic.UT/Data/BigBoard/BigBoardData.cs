using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using GeneticSharp;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Data.BigBoard.Blocks;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Data.BigBoard
{
    public static class BigBoardData
    {
        public static GamePartsConfigurator DemoData()
        {
            // common
            var scaleFactor = 1;

            // allowed angles
            var angles = new int[]
            {
                //-270,
                //-180,
                //-90,
                0,
                90,
                180,
                270
            };

            // board
            var fieldHeight = 1d;
            var fieldWidth = 1d;
            var boardColumnsCount = 10;
            var boardRowsCount = 5;
            var fields = GamePartsHelper.GenerateFields(
                scaleFactor,
                fieldHeight,
                fieldWidth,
                boardColumnsCount,
                boardRowsCount);

            var boardDefinition = new BoardShapeBase(
                fields,
                boardColumnsCount,
                boardRowsCount,
                scaleFactor);

            // blocks
            var blocks = new List<BlockBase>()
            {
                DarkBlue.Create(),
                Red.Create(),
                LightBlue.Create(),
                Purple.Create(),
                Blue.Create(),
                Pink.Create(),
                Green.Create(),
                LightGreen.Create(),
                Orange.Create(),
                Yellow.Create(),
            };

            // calculate allowed locations of blocks
            var modificator = new AllowedLocationsGenerator();
            var preconfiguredBlocks = modificator.Preconfigure(
                blocks.ToList(),
                boardDefinition,
                angles);

            // solver
            var generationChromosomesNumber = 5000;
            var mutationProbability = 0.3f;
            var crossoverProbability = 1.0f - mutationProbability;
            var chromosome = new TangramChromosome(
                preconfiguredBlocks,
                boardDefinition,
                angles);
            var population = new Population(
                generationChromosomesNumber,
                generationChromosomesNumber,
                chromosome);// understand the population parameters
            var selection = new EliteSelection(generationChromosomesNumber);//maybe half or 20% of the defined population, understand the parameter
            var crossover = new TangramCrossover();
            var mutation = new TangramMutation();
            var fitness = new TangramFitness(boardDefinition, blocks);
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new DefaultOperatorsStrategy();
            var termination = new FitnessThresholdTermination(-0.01f); // new FitnessStagnationTermination(100);

            var solverBuilder = Factory.Factory.CreateNew();
            var solver = solverBuilder
                .WithPopulation(population)
                .WithReinsertion(reinsertion)
                .WithSelection(selection)
                .WithFitnessFunction(fitness)
                .WithMutation(mutation, mutationProbability)
                .WithCrossover(crossover, crossoverProbability)
                .WithOperatorsStrategy(operatorStrategy)
                .WithTermination(termination)
                .Build();

            var gameConfiguration = new GamePartsConfigurator(
                blocks,
                boardDefinition,
                solver,
                angles);

            var isConfigurationValid = gameConfiguration.Validate();

            if (!isConfigurationValid)
                throw new Exception("The summarized block definitions area cannot be bigger than the board area.");
            
            return gameConfiguration;
        }

        
    }
}
