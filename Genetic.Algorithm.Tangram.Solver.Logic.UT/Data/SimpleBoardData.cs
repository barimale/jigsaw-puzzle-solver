using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using System.Drawing;
using GeneticSharp;
using NetTopologySuite.Geometries;
using Genetic.Algorithm.Tangram.Solver.Logic.Extensions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities
{
    public static class SimpleBoardData
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
                new Coordinate(0,2),
                new Coordinate(1,2),
                new Coordinate(1,0),
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

            // calculate allowed locations of blocks
            var modificator = new AllowedLocationsGenerator();
            var preconfiguredBlocks = modificator.Preconfigure(
                blocks,
                boardDefinition,
                angles);

            // solver
            var generationChromosomesNumber = 500;
            var mutationProbability = 0.1f;
            var crossoverProbability = 1.0f - mutationProbability;
            var chromosome = new TangramChromosome(
                preconfiguredBlocks,
                boardDefinition,
                angles);
            var population = new Population(
                generationChromosomesNumber,
                generationChromosomesNumber,
                chromosome);// understand the population parameters, estimate the maximal value by multiply amount of blocks
            var selection = new EliteSelection(generationChromosomesNumber);//maybe half or 20% of the defined population, understand the parameter, maybe another one, need to be tested
            var crossover = new TangramCrossover();// 0.8f maybe custom needs to be implemented
            var mutation = new TangramMutation();
            var fitness = new TangramFitness(boardDefinition, blocks);// pass shape via the constructor
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new VaryRatioOperatorsStrategy(); // DefaultOperatorsStrategy(); // TplOperatorsStrategy();
            var termination = new FitnessThresholdTermination(-0.01f); // new FitnessStagnationTermination(100); // new FitnessThresholdTermination(1.2f)

            var solverBuilder = Factory.Factory.CreateNew();
            var solver = solverBuilder
                .WithPopulation(population)
                .WithReinsertion(reinsertion)
                .WithSelection(selection)
                .WithFitnessFunction(fitness)
                .WithMutation(mutation, mutationProbability) // with mutation probability 0.1f ?
                .WithCrossover(crossover, crossoverProbability)
                .WithOperatorsStrategy(operatorStrategy)
                .WithTermination(termination)
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
