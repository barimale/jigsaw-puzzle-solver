using GeneticSharp;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Crossovers;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitness;
using Genetic.Algorithm.Tangram.Solver.Logic.Mutations;
using Genetic.Algorithm.Tangram.Solver.Logic.OperatorStrategies;
using Genetic.Algorithm.Tangram.AlgorithmSettings.Solver;

namespace Genetic.Algorithm.Tangram.AlgorithmSettings.Settings
{
    internal class SimpleBoardAlgorithmSettings : IAlgorithmSettings
    {
        public GeneticAlgorithm CreateNew(
            BoardShapeBase board,
            IList<BlockBase> blocks,
            int[] allowedAngles)
        {
            // solver
            var generationChromosomesNumber = 500;
            var mutationProbability = 0.1f;
            var crossoverProbability = 1.0f - mutationProbability;
            var chromosome = new TangramChromosome(
                blocks,
                board,
                allowedAngles);
            var population = new Population(
                generationChromosomesNumber,
                generationChromosomesNumber,
                chromosome);// understand the population parameters, estimate the maximal value by multiply amount of blocks
            var selection = new EliteSelection(generationChromosomesNumber);//maybe half or 20% of the defined population, understand the parameter, maybe another one, need to be tested
            var crossover = new TangramCrossover();// 0.8f maybe custom needs to be implemented
            var mutation = new TangramMutation();
            var fitness = new TangramFitness(board, blocks);// pass shape via the constructor
            var reinsertion = new ElitistReinsertion();
            var operatorStrategy = new VaryRatioOperatorsStrategy(); // DefaultOperatorsStrategy(); // TplOperatorsStrategy();
            var termination = new FitnessThresholdTermination(-0.01f); // new FitnessStagnationTermination(100); // new FitnessThresholdTermination(1.2f)

            var solverBuilder = SolverFactory.CreateNew();
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

            return solver;
        }
    }
}
