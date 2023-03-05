using GeneticSharp;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Logic;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Crossovers;
using Genetic.Algorithm.Tangram.Solver.Logic.Mutations;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitness;
using Genetic.Algorithm.Tangram.AlgorithmSettings.Solver;

namespace Genetic.Algorithm.Tangram.AlgorithmSettings.Settings
{
    internal class BigBoardAlgorithmSettings : IAlgorithmSettings
    {
        public GeneticAlgorithm CreateNew(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles)
        {
            // solver
            var generationChromosomesNumber = 5000;
            var mutationProbability = 0.3f;
            var crossoverProbability = 1.0f - mutationProbability;
            var chromosome = new TangramChromosome(
                blocks,
                board,
                allowedAngles);

            var population = new Population(
                generationChromosomesNumber,
                generationChromosomesNumber,
                chromosome);// understand the population parameters
            var selection = new RouletteWheelSelection(); // EliteSelection(generationChromosomesNumber);//maybe half or 20% of the defined population, understand the parameter
            var crossover = new TangramCrossover();
            var mutation = new TangramMutation();
            var fitness = new TangramFitness(board, blocks);
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new DefaultOperatorsStrategy();
            var termination = new FitnessStagnationTermination(100); // new FitnessThresholdTermination(-0.01f); // new FitnessStagnationTermination(100);

            var solverBuilder = SolverFactory.CreateNew();
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

            return solver;
        }
    }
}
