using GeneticSharp;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitness;
using Genetic.Algorithm.Tangram.Solver.Logic.Population;
using Genetic.Algorithm.Tangram.Solver.Logic.OperatorStrategies;
using Genetic.Algorithm.Tangram.Solver.Logic.Mutations;
using Genetic.Algorithm.Tangram.Solver.Logic.Crossovers;
using Genetic.Algorithm.Tangram.AlgorithmSettings.Solver;
using Genetic.Algorithm.Tangram.Solver.Logic.Populations.Generators;

namespace Genetic.Algorithm.Tangram.AlgorithmSettings.Settings
{
    internal class MediumBoardAlgorithmSettings : IAlgorithmSettings
    {
        public GeneticAlgorithm CreateNew(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles)
        {
            // solver
            var generationChromosomesNumber = 300;
            var mutationProbability = 0.2f;
            var crossoverProbability = 1.0f - mutationProbability;

            // initial population
            var fitness = new TangramFitness(board, blocks);

            var initialPopulationGenerator = new InitialPopulationGenerator();
            var chromosomes = initialPopulationGenerator
                .Generate(
                    blocks,
                    board,
                    allowedAngles,
                    20d);

            var preloadedPopulation = new PreloadedPopulation(
                generationChromosomesNumber / 2,
                generationChromosomesNumber,
                chromosomes
                    .Shuffle(new FastRandomRandomization())
                    .Take(generationChromosomesNumber)
                    .ToList());
            //preloadedPopulation.GenerationStrategy = new TrackingGenerationStrategy();
            preloadedPopulation.GenerationStrategy = new PerformanceGenerationStrategy();

            var selection = new RouletteWheelSelection(); // new EliteSelection(generationChromosomesNumber);//RouletteWheelSelection
            var crossover = new TangramCrossover();
            var mutation = new TangramMutation();
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new VaryRatioOperatorsStrategy(); // DefaultOperatorsStrategy(); //TplOperatorsStrategy
            var termination = new FitnessThresholdTermination(-0.01f); // new FitnessStagnationTermination(100);

            var solverBuilder = SolverFactory.CreateNew();
            var solver = solverBuilder
                .WithPopulation(preloadedPopulation)
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
