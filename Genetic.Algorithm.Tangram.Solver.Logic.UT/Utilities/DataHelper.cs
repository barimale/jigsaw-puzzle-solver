using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using SixLabors.Shapes;
using System.Drawing;
using GeneticSharp.Extensions;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Utilities
{
    public static class DataHelper
    {
        public static GamePartsConfigurator SimpleBoardData()
        {
            // common
            var scaleFactor = 1;

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
            var polygon = new RectangularPolygon(0, 0, 1, 2);
            var zielonyBloczek = new BlockBase(polygon, Color.Green);
            blocks.Add(zielonyBloczek);

            var polygon2 = new RectangularPolygon(0, 0, 1, 1);
            var niebieskiBloczek = new BlockBase(polygon2, Color.Blue);
            blocks.Add(niebieskiBloczek);

            var polygon3 = new RectangularPolygon(0, 0, 1, 1);
            var czerwonyBloczek = new BlockBase(polygon3, Color.Red);
            blocks.Add(czerwonyBloczek);

            // solver
            // TODO: need to be implemented
            // reuse some data from above
            var chromosome = new TspChromosome(100);
            var population = new Population(50, 50, chromosome);
            var selection = new EliteSelection();
            var crossover = new OrderedCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new TspFitness(100, 0, 1000, 0, 1000);

            var solverBuilder = Factory.Factory.CreateNew();
            var solver = solverBuilder
                .WithPopulation(population)
                .WithSelection(selection)
                .WithFitnessFunction(fitness)
                .WithMutation(mutation)
                .WithCrossover(crossover)
                .WithTermination(new GenerationNumberTermination(100))
                .Build();

            var gameConfiguration = new GamePartsConfigurator(
                blocks,
                boardDefinition,
                solver);

            return gameConfiguration;
        }
    }
}
