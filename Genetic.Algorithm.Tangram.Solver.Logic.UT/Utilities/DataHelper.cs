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
            var fields = new List<BoardFieldDefinition>()
            {
                new BoardFieldDefinition(0,0, true, scaleFactor),
                new BoardFieldDefinition(0,1, true, scaleFactor),
                new BoardFieldDefinition(1,0, true, scaleFactor),
                new BoardFieldDefinition(1,1, true, scaleFactor)
            };
            BoardShapeBase boardDefinition = new BoardShapeBase(fields, 2, 2, 1);

            // blocks
            var blocks = new List<BlockBase>();
            var polygon = new GeometryFactory()
                .CreatePolygon(new Coordinate[] {
                new Coordinate(0,0),// first the same as last
                new Coordinate(0,2),
                new Coordinate(2,1),
                new Coordinate(1,0),
                new Coordinate(0,0)// last the same as first
                });
            var zielonyBloczek = new BlockBase(polygon, Color.Green);
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
            blocks.Add(czerwonyBloczek);

            // solver
            // TODO: need to be implemented
            // reuse some data from above
            var generationChromosomesNumber = 50;
            var chromosome = new TangramChronosome();
            var population = new Population(
                generationChromosomesNumber,
                generationChromosomesNumber,
                chromosome);// understand the population parameters
            var selection = new EliteSelection(generationChromosomesNumber);//maybe half or 20% of the defined population, understand the parameter, maybe another one, need to be tested
            var crossover = new UniformCrossover(0.1f);// maybe custom needs to be implemented
            var mutation = new ReverseSequenceMutation();// maybe another one, need to be tested
            var fitness = new TangramFitness(boardDefinition);// pass shape via the constructor
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new DefaultOperatorsStrategy();

            var solverBuilder = Factory.Factory.CreateNew();
            var solver = solverBuilder
                .WithPopulation(population)
                .WithReinsertion(reinsertion)
                .WithSelection(selection)
                .WithFitnessFunction(fitness)
                .WithMutation(mutation, 0.1f) // with mutation probability
                .WithCrossover(crossover, 0.75f)
                .WithOperatorsStrategy(operatorStrategy) // copy default strategy class and put brakepoints there to check the correlation between crossover and mutation
                .WithTermination(new FitnessThresholdTermination()) // The default expected fitness is 1.00. but another value may be provided via the constructor's argument
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
