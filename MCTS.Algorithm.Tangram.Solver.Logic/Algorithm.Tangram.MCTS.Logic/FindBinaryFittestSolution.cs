using Algorithm.Tangram.Common.Extensions;
using Algorithm.Tangram.TreeSearch.Logic.Domain;
using Algorithm.Tangram.TreeSearch.Logic.Extensions;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses.Services;
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
        private readonly FitnessService fitnessService;

        public FindBinaryFittestSolution(
            BoardShapeBase board,
            IList<BlockBase> blocks)
        {
            this.ID = Guid.NewGuid().ToString();
            this.board = board;
            fitnessService = new FitnessService(this.board);
            this.blocks = new List<BlockBase>(blocks);
            size = this.blocks.Count;

            choicesMade = new Stack<IndexedBinaryBlockBase>();
            remaining = new HashSet<BlockBase>(this.blocks);
        }

        public string ID { private set; get; }
        public ImmutableList<IndexedBinaryBlockBase> Solution => choicesMade.ToImmutableList();
        public BoardShapeBase Board => board;
        public IList<BlockBase> Blocks => blocks.ToImmutableList();

        private int CheckBinarySum()
        {
            return CheckBinarySumAsync().Result;
        }

        private async Task<int> CheckBinarySumAsync()
        {
            Task<int> diffSum = Task
                    .Factory
                    .StartNew(() =>
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
                    });

            var result = await Task.WhenAll(diffSum);

            return result.Sum();
        }

        private int CheckFitness(
            bool withPolygonsIntersectionsDiff,
            bool withOutOfBoundsDiff,
            bool withVolumeDiff)
        {
            var evaluatedGeometry = choicesMade
                .Select(p => p.TransformedBlock)
                .ToList()
                .Select(pp => pp.Polygon)
                .ToArray();

            var diff = fitnessService.Evaluate(
                evaluatedGeometry,
                board,
                withPolygonsIntersectionsDiff,
                withOutOfBoundsDiff,
                withVolumeDiff,
                false);

            var diffAsInt = diff.ConvertToInt32();

            return diffAsInt;
        }

        public bool IsTerminal => choicesMade.Count == size;

        public Minimize Bound => new Minimize(CheckBinarySum());

        public Minimize? Quality => IsTerminal ? new Minimize(CheckFitness(true, true, true)) : null;

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

            nextOne.AllowedLocations.WithIndex().AsParallel().ForAll((p) =>
            {
                innerResult.Add(
                    new IndexedBinaryBlockBase(
                        Board.BoardFieldsDefinition,
                        nextOne,
                        p.index));
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
            return $"FindBinaryFittestSolution [{string.Join(", ", choicesMade)}]";
        }
    }
}