using Algorithm.Tangram.MCTS.Logic.Domain;
using Genetic.Algorithm.Tangram.Common.Extensions.Extensions;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses.Services;
using GeneticSharp;
using System.Collections.Immutable;
using TreesearchLib;

namespace Algorithm.Tangram.MCTS.Logic
{
    public class FindFittestSolution : IMutableState<FindFittestSolution, IndexedBlockBase, Minimize>
    {
        // settings
        private int size; // amount of blocks
        private Stack<IndexedBlockBase> choicesMade; // solution with vary size depending on the level of the generated tree
        public HashSet<BlockBase> remaining;

        // game parts
        private readonly BoardShapeBase board;
        private readonly IList<BlockBase> blocks;
        private readonly FitnessService fitnessService;

        public FindFittestSolution(
            int size,
            BoardShapeBase board,
            IList<BlockBase> blocks)
        {
            this.size = size;
            this.board = board;
            this.blocks = blocks;
            this.fitnessService = new FitnessService(this.board);
            this.choicesMade = new Stack<IndexedBlockBase>();
            this.remaining = new HashSet<BlockBase>(this.blocks);
        }

        private int CheckFitness(
            bool withPolygonsIntersectionsDiff,
            bool withOutOfBoundsDiff,
            bool withVolumeDiff)
        {
            var evaluatedGeometry = this.choicesMade
                .Select(p => p.TransformedBlock)
                .ToList()
                .Select(pp => pp.Polygon)
                .ToImmutableList();

            var diff = fitnessService.Evaluate(
                evaluatedGeometry.ToArray(),
                this.board,
                withPolygonsIntersectionsDiff,
                withOutOfBoundsDiff,
                withVolumeDiff);

            var diffAsInt = diff > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(diff);

            return diffAsInt;
        }

        public bool IsTerminal => choicesMade.Count == size;

        public Minimize Bound => new Minimize(CheckFitness(true, false, false));

        public Minimize? Quality => IsTerminal ? new Minimize(CheckFitness(true, false, false)) : null;

        public void Apply(IndexedBlockBase choice)
        {
            remaining.Remove(choice.BlockDefinition);
            choicesMade.Push(choice);
        }

        public object Clone()
        {
            var clone = new FindFittestSolution(
                this.size,
                this.board,
                this.blocks);

            clone.choicesMade = new Stack<IndexedBlockBase>(choicesMade);
            clone.remaining = new HashSet<BlockBase>(remaining);

            return clone;
        }

        public IEnumerable<IndexedBlockBase> GetChoices()
        {
            var results = new List<IndexedBlockBase>();

            foreach (var item in remaining)
            {
                var innerResult = new List<IndexedBlockBase>();

                foreach ( var (pp, index) in item.AllowedLocations.WithIndex())
                {
                    innerResult.Add(new IndexedBlockBase(item, index));
                }

                results.AddRange(innerResult);
            }

            return results
                .Shuffle(new FastRandomRandomization())
                .AsEnumerable();
        }

        public void UndoLast()
        {
            var popped = choicesMade.Pop();
            remaining.Add(popped.BlockDefinition);
        }

        public override string ToString()
        {
            return $"FindFittestSolution [{string.Join(", ", choicesMade)}]";
        }
    }
}