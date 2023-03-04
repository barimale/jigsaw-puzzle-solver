using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts;
using GeneticSharp;
using Genetic.Algorithm.Tangram.Solver.Logic.Utilities;
using Genetic.Algorithm.Tangram.Solver.Logic.UT.Data.BigBoard.Blocks;
using NetTopologySuite.Geometries;
using Genetic.Algorithm.Tangram.Solver.Logic.Extensions;

namespace Genetic.Algorithm.Tangram.Solver.Logic.UT.Data.BigBoard
{
    public static class MediumBoardData
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
            var boardColumnsCount = 5;
            var boardRowsCount = 4;
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
                LightBlue.Create(),
                Purple.Create(),
                Blue.Create(),
                //Blue.Create(),// with another blue works ok
            };

            // calculate allowed locations of blocks
            // ( blocks may be use as well, but the chromosome behaviour
            // is going to be more complex)
            var modificator = new AllowedLocationsGenerator();
            var preconfiguredBlocks = modificator.Preconfigure(
                blocks,
                boardDefinition,
                angles);

            var dynamicPopulationSize = blocks
                .Select(p => p.AllowedLocations.Length)
                .ToList();

            var multipliedDynamicPopulationSize = 1;
            foreach(var item in dynamicPopulationSize)
            {
                multipliedDynamicPopulationSize = multipliedDynamicPopulationSize * item;
            }

            // solver
            var generationChromosomesNumber = Math.Max(multipliedDynamicPopulationSize, 30000);
            var mutationProbability = 0.2f;
            var crossoverProbability = 1.0f - mutationProbability;

            // use together with the population class
            var chromosome = new TangramChromosome(
                preconfiguredBlocks,
                boardDefinition,
                angles);

            // all combinations of genes as an initial set of chromosomes
            var chromosomes = new List<IChromosome>();

            var allLocations = preconfiguredBlocks
                .Select(p => p.AllowedLocations.ToArray())
                .ToArray();

            var allPermutations = PopulationHelper
                .Permutate<Geometry>(allLocations);

            var blocksAsArray = blocks.ToArray();
            var preloadFitness = double.MinValue;
            var preloadSolutions = new List<Tuple<double, TangramChromosome>>();
            var tangramFitness = new TangramFitness(boardDefinition, blocks);

            foreach (var permutation in allPermutations)
            {
                var newChromosome = new TangramChromosome(
                    preconfiguredBlocks,
                    boardDefinition,
                    angles);

                foreach(var (gene, index) in permutation.WithIndex())
                {
                    var newBlockAsGene = new BlockBase(
                        gene,
                        blocksAsArray[index].Color,
                        false);

                    newChromosome.ReplaceGene(
                        index,
                        new Gene(newBlockAsGene));
                }

                var newFitness = tangramFitness.Evaluate(newChromosome);
                if(newFitness >= preloadFitness)
                {
                    preloadFitness = newFitness;
                    preloadSolutions.Add(
                        Tuple.Create(
                            newFitness,
                            newChromosome));
                }
                chromosomes.Add(newChromosome);
            }

            var chromosomesAmount = chromosomes.Count;
            var chromosomesWithFitnessBelowFive = preloadSolutions
                .Where(p => p.Item1 > -5f)
                .ToList();

            var onlyCompleteSolutions = preloadSolutions
                .Where(ppp => ppp.Item1 == 0f)
                .ToList();

            var preloadedPopulation = new PreloadedPopulation(
                multipliedDynamicPopulationSize,
                generationChromosomesNumber,
                chromosomes);
            //preloadedPopulation.GenerationStrategy = new TrackingGenerationStrategy();
            //population.CreateInitialGeneration();

            var selection = new EliteSelection(generationChromosomesNumber);//maybe half or 20% of the defined population, understand the parameter
            var crossover = new TangramCrossover(); //  TangramCrossover
            var mutation = new TangramMutation(); // RouletteWheelSelection
            var fitness = new TangramFitness(boardDefinition, blocks);
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new DefaultOperatorsStrategy(); // DefaultOperatorsStrategy(); //TplOperatorsStrategy
            var termination = new FitnessThresholdTermination(-0.01f); // new FitnessStagnationTermination(100);

            var solverBuilder = Factory.Factory.CreateNew();
            var solver = solverBuilder
                .WithPopulation(preloadedPopulation)
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
