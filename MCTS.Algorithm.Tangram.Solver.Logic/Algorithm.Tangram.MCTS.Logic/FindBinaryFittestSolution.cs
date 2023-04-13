using Algorithm.Tangram.Common.Extensions;
using Algorithm.Tangram.TreeSearch.Logic.Domain;
using Algorithm.Tangram.TreeSearch.Logic.Extensions;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses.Services;
using GeneticSharp;
using System.Collections.Immutable;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;
using TreesearchLib;

namespace Algorithm.Tangram.TreeSearch.Logic
{
    public class FindBinaryFittestSolution : IMutableState<FindBinaryFittestSolution, IndexedBinaryBlockBase, Minimize>
    {
        // settings
        private int size => this.blocks.Count;
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
            this.blocks = new List<BlockBase>(blocks);
            fitnessService = new FitnessService(this.board);

            choicesMade = new Stack<IndexedBinaryBlockBase>();
            remaining = new HashSet<BlockBase>(this.blocks);
        }

        public string ID { private set; get; }
        public ImmutableList<IndexedBinaryBlockBase> Solution => choicesMade.ToImmutableList();
        public BoardShapeBase Board => board;
        public IList<BlockBase> Blocks => blocks.ToImmutableList();

        private int CheckBinarySum()
        {
            var boardFieldAmount = this.board.BoardFieldsDefinition.Count;

            var binaries = choicesMade
                             .Select(p => p.BinaryBlockOnTheBoard)
                             .ToList();

            var diff = fitnessService.EvaluateBinary(binaries, blocks.Count);

            return diff;
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
                blocks)
            {
                choicesMade = new Stack<IndexedBinaryBlockBase>(this.choicesMade),
                remaining = new HashSet<BlockBase>(this.remaining)
            };

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

            results = nextOne.AllowedLocations
                .WithIndex()
                .Select((p) =>
                    {
                        return  new IndexedBinaryBlockBase(
                                    Board.BoardFieldsDefinition,
                                    nextOne,
                                    p.index);
                    })
                .ToList();


            return results
                .Shuffle(new FastRandomRandomization())
                .AsEnumerable();
        }

        public void UndoLast()
        {
            var popped = this.choicesMade.Pop();
            this.remaining.Add(popped.BlockDefinition);
        }

        public override string ToString()
        {
            return $"FindBinaryFittestSolution [{string.Join(", ", choicesMade)}]";
        }
    }
}