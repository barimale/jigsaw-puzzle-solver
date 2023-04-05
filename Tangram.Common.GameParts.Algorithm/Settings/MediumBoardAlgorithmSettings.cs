using GeneticSharp;
using Genetic.Algorithm.Tangram.Solver.Logic.OperatorStrategies;
using Genetic.Algorithm.Tangram.Solver.Logic.Mutations;
using Genetic.Algorithm.Tangram.Solver.Logic.Crossovers;
using Genetic.Algorithm.Tangram.Solver.Logic.Populations.Generators;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses;
using Genetic.Algorithm.Tangram.Solver.Logic.Populations;
using Genetic.Algorithm.Tangram.Solver.Logic;
using Solver.Tangram.AlgorithmDefinitions;
using Tangram.GameParts.Logic.GameParts.Board;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Genetic.Algorithm.Tangram.GA.Solver.Templates.Settings
{
    internal class MediumBoardAlgorithmSettings : IAlgorithmSettings
    {
        public GeneticAlgorithm CreateNew(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles)
        {
            // settings
            var withAllowedLocations = blocks
                .ToList()
                .SelectMany(p => p.AllowedLocations.ToList())
                .ToList()
                .Count > 0;

            // solver
            var generationChromosomesNumber = 300;
            var mutationProbability = 0.2f; // quick for 0.0f and 300 chromosomes / 1 generation
            var crossoverProbability = 1.0f - mutationProbability;
            var fitness = new TangramService(board, blocks);

            // initial population
            IPopulation initialPopulation;
            if (withAllowedLocations)
            {
                var initialPopulationGenerator = new InitialPopulationGenerator();
                var chromosomes = initialPopulationGenerator
                    .Generate(
                        blocks,
                        board,
                        allowedAngles,
                        100d);

                initialPopulation = new PreloadedPopulation(
                    generationChromosomesNumber / 2,
                    generationChromosomesNumber,
                    chromosomes
                        .Shuffle(new FastRandomRandomization())
                        .Take(generationChromosomesNumber)
                        .ToList());
                initialPopulation.GenerationStrategy = new PerformanceGenerationStrategy();
                initialPopulation.CreateInitialGeneration();
            }
            else
            {
                var chromosome = new TangramChromosome(
                    blocks,
                    board,
                    allowedAngles);

                initialPopulation = new Population(
                    generationChromosomesNumber / 2,
                    generationChromosomesNumber,
                    chromosome);
            }

            var selection = new RouletteWheelSelection(); // new EliteSelection(generationChromosomesNumber);//RouletteWheelSelection
            var crossover = new TangramCrossover();
            var mutation = new TangramMutation();
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new VaryRatioOperatorsStrategy(); // DefaultOperatorsStrategy(); //TplOperatorsStrategy
            var termination = new FitnessThresholdTermination(-0.01f); // new FitnessStagnationTermination(100);

            var solver = SolverFactory.GASolverBuilder
                .WithPopulation(initialPopulation)
                .WithReinsertion(reinsertion)
                .WithSelection(selection)
                .WithFitnessFunction(fitness)
                .WithMutation(mutation, mutationProbability)
                .WithCrossover(crossover, crossoverProbability)
                .WithOperatorsStrategy(operatorStrategy)
                .WithTermination(termination)
                //.WithParallelTaskExecutor(1, Math.Max(2, generationChromosomesNumber / 10))
                .Build();

            return solver;
        }
    }
}
