using GeneticSharp;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Common.Extensions.Extensions;
using Genetic.Algorithm.Tangram.Solver.Logic.Chromosome;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitness;
using Genetic.Algorithm.Tangram.Solver.Logic.Population;
using Genetic.Algorithm.Tangram.Solver.Logic.OperatorStrategies;
using Genetic.Algorithm.Tangram.Solver.Logic.Mutations;
using Genetic.Algorithm.Tangram.Solver.Logic.Crossovers;
using Genetic.Algorithm.Tangram.AlgorithmSettings.Solver;

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
            var generationChromosomesNumber = 30000;
            var mutationProbability = 0.2f;
            var crossoverProbability = 1.0f - mutationProbability;

            // use together with the population class
            var chromosome = new TangramChromosome(
                blocks,
                board,
                allowedAngles);

            // all combinations of genes as an initial set of chromosomes
            var chromosomes = new List<IChromosome>();

            var allLocations = blocks
                .Select(p => p.AllowedLocations.ToArray())
                .ToArray();

            var allPermutations = allLocations
                .Permutate();

            var blocksAsArray = blocks.ToArray();
            var preloadFitness = double.MinValue;
            var preloadSolutions = new List<Tuple<double, TangramChromosome>>();
            var tangramFitness = new TangramFitness(board, blocks);

            foreach (var permutation in allPermutations)
            {
                var newChromosome = new TangramChromosome(
                    blocks,
                    board,
                    allowedAngles);

                foreach (var (gene, index) in permutation.WithIndex())
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
                if (newFitness >= preloadFitness)
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
                30000,
                generationChromosomesNumber,
                chromosomes);
            //preloadedPopulation.GenerationStrategy = new TrackingGenerationStrategy();
            //population.CreateInitialGeneration();

            var selection = new RouletteWheelSelection(); // new EliteSelection(generationChromosomesNumber);//RouletteWheelSelection
            var crossover = new TangramCrossover();
            var mutation = new TangramMutation();
            var fitness = new TangramFitness(board, blocks);
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
                .Build();

            return solver;
        }
    }
}
