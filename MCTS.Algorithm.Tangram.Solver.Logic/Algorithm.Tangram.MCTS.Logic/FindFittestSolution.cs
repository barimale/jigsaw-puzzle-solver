using Algorithm.Tangram.Common.Extensions;
using Algorithm.Tangram.TreeSearch.Logic.Domain;
using Algorithm.Tangram.TreeSearch.Logic.Extensions;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses.Services;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using TreesearchLib;

namespace Algorithm.Tangram.TreeSearch.Logic
{
    public class FindFittestSolution : IMutableState<FindFittestSolution, IndexedBlockBase, Minimize>
    {
        // settings
        private int size;
        private Stack<IndexedBlockBase> choicesMade;
        public HashSet<BlockBase> remaining;

        // game parts
        private readonly BoardShapeBase board;
        private readonly IList<BlockBase> blocks;
        private readonly FitnessService fitnessService;

        public FindFittestSolution(
            BoardShapeBase board,
            IList<BlockBase> blocks)
        {
            this.board = board;
            this.blocks = new List<BlockBase>(blocks);
            size = this.blocks.Count;
            fitnessService = new FitnessService(this.board);
            choicesMade = new Stack<IndexedBlockBase>();
            remaining = new HashSet<BlockBase>(this.blocks);
        }

        public ImmutableList<IndexedBlockBase> Solution => choicesMade.ToImmutableList();
        public BoardShapeBase Board => board;
        public IList<BlockBase> Blocks => blocks.ToImmutableList();

        private int CheckFitness(
            bool withPolygonsIntersectionsDiff,
            bool withOutOfBoundsDiff,
            bool withVolumeDiff)
        {
            var evaluatedGeometry = choicesMade
                .Select(p => p.TransformedBlock)
                .ToList()
                .Select(pp => pp.Polygon)
                .ToImmutableList();

            var diff = fitnessService.Evaluate(
                evaluatedGeometry.ToArray(),
                board,
                withPolygonsIntersectionsDiff,
                withOutOfBoundsDiff,
                withVolumeDiff,
                false);

            var diffAsInt = diff.ConvertToInt32();

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
                board,
                blocks);

            clone.choicesMade = new Stack<IndexedBlockBase>(choicesMade);
            clone.remaining = new HashSet<BlockBase>(remaining);

            return clone;
        }

        public IEnumerable<IndexedBlockBase> GetChoices()
        {
            var results = new List<IndexedBlockBase>();
            var nextOne = remaining.FirstOrDefault();

            if (nextOne == null)
            {
                return results;
            }

            var innerResult = new ConcurrentBag<IndexedBlockBase>();

            nextOne.AllowedLocations.WithIndex().AsParallel().ForAll((p) =>
            {
                innerResult.Add(new IndexedBlockBase(nextOne, p.index));
            });

            results.AddRange(innerResult.AsEnumerable());

            return results
                //.Shuffle(new FastRandomRandomization()) // TODO WIP or Reverse first < -some extras
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