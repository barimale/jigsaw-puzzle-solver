using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using System.Drawing;
using GeneticSharp;
using NetTopologySuite.Geometries;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities
{
    public static class DataHelper
    {
        public static GamePartsConfigurator SimpleBoardData()
        {
            // common
            var scaleFactor = 1;

            // allowed angles
            var angles = new int[7]
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
            var fields = new List<BoardFieldDefinition>()
            {
                new BoardFieldDefinition(0,0,fieldWidth, fieldHeight, true, scaleFactor),
                new BoardFieldDefinition(0,1,fieldWidth, fieldHeight, true, scaleFactor),
                new BoardFieldDefinition(1,1,fieldWidth, fieldHeight, true, scaleFactor),
                new BoardFieldDefinition(1,0,fieldWidth, fieldHeight, true, scaleFactor)
            };
            BoardShapeBase boardDefinition = new BoardShapeBase(fields, 2, 2, scaleFactor);

            // blocks
            var blocks = new List<BlockBase>();
            var polygon = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                new Coordinate(0,0),// first the same as last
                new Coordinate(0,1),
                new Coordinate(2,1),
                new Coordinate(2,0),
                new Coordinate(0,0)// last the same as first
                });
            var zielonyBloczek = new BlockBase(polygon, Color.Green);
            var toStringFromClone1 = zielonyBloczek.ToString();

            blocks.Add(zielonyBloczek);

            var polygon2 = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                new Coordinate(0,0),// first the same as last
                new Coordinate(0,1),
                new Coordinate(1,1),
                new Coordinate(1,0),
                new Coordinate(0,0)// last the same as first
                });
            var niebieskiBloczek = new BlockBase(polygon2, Color.Blue);
            var toStringFromClone2 = niebieskiBloczek.ToString();

            blocks.Add(niebieskiBloczek);

            var polygon3 = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                    new Coordinate(0,0),// first the same as last
                    new Coordinate(0,1),
                    new Coordinate(1,1),
                    new Coordinate(1,0),
                    new Coordinate(0,0)// last the same as first
                });
            var czerwonyBloczek = new BlockBase(polygon3, Color.Red);
            var toStringFromClone3 = czerwonyBloczek.ToString();

            blocks.Add(czerwonyBloczek);

            // solver
            // TODO: reuse some data from above
            // and check everything once again
            var generationChromosomesNumber = 5000;
            var chromosome = new TangramChromosome(
                blocks,
                boardDefinition,
                angles);
            var population = new Population(
                generationChromosomesNumber,
                generationChromosomesNumber,
                chromosome);// understand the population parameters, estimate the maximal value by multiply amount of blocks
            var selection = new EliteSelection(generationChromosomesNumber);//maybe half or 20% of the defined population, understand the parameter, maybe another one, need to be tested
            var crossover = new TangramCrossover(0.4f);// 0.2f maybe custom needs to be implemented
            var mutation = new UniformMutation(false);// maybe another one, need to be tested
            var fitness = new TangramFitness(boardDefinition, blocks);// pass shape via the constructor
          //  var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new DefaultOperatorsStrategy();

            var solverBuilder = Factory.Factory.CreateNew();
            var solver = solverBuilder
                .WithPopulation(population)
                //.WithReinsertion(reinsertion)
                .WithSelection(selection)
                .WithFitnessFunction(fitness)
                .WithMutation(mutation, 0.35f) // with mutation probability 0.1f ?
                .WithCrossover(crossover)
                //.WithOperatorsStrategy(operatorStrategy) // copy default strategy class and put brakepoints there to check the correlation between crossover and mutation
                .WithTermination(new FitnessThresholdTermination(2.2f)) // The default expected fitness is 1.00. but another value may be provided via the constructor's argument
                .Build();

            // think how to implement the crossover, is it reasonable to invoke the mutation class from there

            var gameConfiguration = new GamePartsConfigurator(
                blocks,
                boardDefinition,
                solver,
                angles);

            return gameConfiguration;
        }
    }
}
