using GeneticSharp;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Crossovers;
using Genetic.Algorithm.Tangram.Solver.Logic.Mutations;
using Genetic.Algorithm.Tangram.Solver.Logic.Populations.Generators;
using Genetic.Algorithm.Tangram.Solver.Logic.Populations;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses;
using Genetic.Algorithm.Tangram.Solver.Logic;
using Solver.Tangram.AlgorithmDefinitions;
using Tangram.GameParts.Logic.GameParts.Board;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Genetic.Algorithm.Tangram.GA.Solver.Templates.Settings
{
    internal class SimpleBoardAlgorithmSettings : IAlgorithmSettings
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
            var generationChromosomesNumber = 500;
            var mutationProbability = 0.1f;
            var crossoverProbability = 1.0f - mutationProbability;

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
                        80d);

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

            var selection = new EliteSelection(generationChromosomesNumber);
            var crossover = new TangramCrossover();
            var mutation = new TangramMutation();
            var fitness = new TangramGeometricalFitnessService(board, blocks);
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new DefaultOperatorsStrategy(); // DefaultOperatorsStrategy()
            var termination = new FitnessThresholdTermination(-0.01f); // new FitnessStagnationTermination(100); // new FitnessThresholdTermination(1.2f)

            var solver = SolverFactory.GASolverBuilder
                .WithPopulation(initialPopulation)
                .WithReinsertion(reinsertion)
                .WithSelection(selection)
                .WithFitnessFunction(fitness)
                .WithMutation(mutation, mutationProbability)
                .WithCrossover(crossover, crossoverProbability)
                .WithOperatorsStrategy(operatorStrategy)
                .WithTermination(termination)
                .Build();

            return solver;
        }
    }
}
