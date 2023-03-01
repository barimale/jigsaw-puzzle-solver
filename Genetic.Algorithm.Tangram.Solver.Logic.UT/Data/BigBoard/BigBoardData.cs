using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using System.Drawing;
using GeneticSharp;
using NetTopologySuite.Geometries;
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
                -270,
                -180,
                -90,
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

            // solver
            // and check everything once again
            var generationChromosomesNumber = 500;
            var mutationProbability = 0.1f;
            var crossoverProbability = 1.0f - mutationProbability;
            var chromosome = new TangramChromosome(
                blocks,
                boardDefinition,
                angles);
            var population = new Population(
                generationChromosomesNumber,
                generationChromosomesNumber,
                chromosome);// understand the population parameters, estimate the maximal value by multiply amount of blocks
            var selection = new EliteSelection(generationChromosomesNumber);//maybe half or 20% of the defined population, understand the parameter, maybe another one, need to be tested
            var crossover = new TangramCrossover(crossoverProbability);// 0.8f maybe custom needs to be implemented
            var mutation = new TworsMutation(); // new UniformMutation(false);//TworsMutation or custom and modify cross//custom here maybe another one, need to be tested
            var fitness = new TangramFitness(boardDefinition, blocks);// pass shape via the constructor
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new DefaultOperatorsStrategy();
            var termination = new FitnessThresholdTermination(-0.01f); // new FitnessStagnationTermination(100); // new FitnessThresholdTermination(1.2f)

            var solverBuilder = Factory.Factory.CreateNew();
            var solver = solverBuilder
                .WithPopulation(population)
                .WithReinsertion(reinsertion)
                .WithSelection(selection)
                .WithFitnessFunction(fitness)
                .WithMutation(mutation, mutationProbability) // with mutation probability 0.1f ?
                .WithCrossover(crossover, crossoverProbability)
                //.WithOperatorsStrategy(operatorStrategy) // copy default strategy class and put brakepoints there to check the correlation between crossover and mutation
                .WithTermination(termination) // The default expected fitness is 1.00. but another value may be provided via the constructor's argument
                .Build();

            var gameConfiguration = new GamePartsConfigurator(
                blocks,
                boardDefinition,
                solver,
                angles);

            return gameConfiguration;
        }

        
    }
}
