using Algorithm.Tangram.Common.Extensions;
using Algorithm.Tangram.TreeSearch.Logic.Domain;
using GeneticSharp;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using TreesearchLib;

namespace Algorithm.Tangram.TreeSearch.Logic
{
    public class FindBinaryFittestSolution : IMutableState<FindBinaryFittestSolution, IndexedBinaryBlockBase, Minimize>
    {
        // settings
        private int size;
        private Stack<IndexedBinaryBlockBase> choicesMade;
        public HashSet<BlockBase> remaining;

        // game parts
        private readonly BoardShapeBase board;
        private readonly IList<BlockBase> blocks;

        public FindBinaryFittestSolution(
            BoardShapeBase board,
            IList<BlockBase> blocks)
        {
            this.board = board;
            this.blocks = new List<BlockBase>(blocks);
            size = this.blocks.Count;

            choicesMade = new Stack<IndexedBinaryBlockBase>();
            remaining = new HashSet<BlockBase>(this.blocks);
        }

        public ImmutableList<IndexedBinaryBlockBase> Solution => choicesMade.ToImmutableList();
        public BoardShapeBase Board => board;
        public IList<BlockBase> Blocks => blocks.ToImmutableList();

        private int CheckBinarySum()
        {
            var binaries = choicesMade
                .Select(p => p.BinaryBlockOnTheBoard)
                .ToList();

            var sums =
                from array in binaries
                from valueIndex in array.Select((value, index) => new { Value = value, Index = index })
                group valueIndex by valueIndex.Index into indexGroups
                select indexGroups.Select(indexGroup => indexGroup.Value).Sum();

            var diffSum = sums.Select(p => Math.Abs(p - 1)).ToArray();
            var diff = 1 * Math.Abs(diffSum.Sum());

            return diff;
        }

        public bool IsTerminal => choicesMade.Count == size;

        public Minimize Bound => new Minimize(CheckBinarySum());

        public Minimize? Quality => IsTerminal ? new Minimize(CheckBinarySum()) : null;

        public void Apply(IndexedBinaryBlockBase choice)
        {
            remaining.Remove(choice.BlockDefinition);
            choicesMade.Push(choice);
        }

        public object Clone()
        {
            var clone = new FindBinaryFittestSolution(
                board,
                blocks);

            clone.choicesMade = new Stack<IndexedBinaryBlockBase>(choicesMade);
            clone.remaining = new HashSet<BlockBase>(remaining);

            return clone;
        }

        public IEnumerable<IndexedBinaryBlockBase> GetChoices()
        {
            var results = new List<IndexedBinaryBlockBase>();
            var nextOne = remaining.FirstOrDefault();

            if (nextOne == null)
            {
                return results;
            }

            var innerResult = new ConcurrentBag<IndexedBinaryBlockBase>();

            nextOne.AllowedLocations
                .WithIndex()
                .AsParallel()
                .ForAll((p) => {
                    innerResult.Add(new IndexedBinaryBlockBase(Board.BoardFieldsDefinition, nextOne, p.index));
                });

            results.AddRange(innerResult.AsEnumerable());

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