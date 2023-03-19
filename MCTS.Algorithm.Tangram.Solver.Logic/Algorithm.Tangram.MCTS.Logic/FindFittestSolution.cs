using Algorithm.Tangram.Common.Extensions;
using Algorithm.Tangram.MCTS.Logic.Domain;
using Algorithm.Tangram.MCTS.Logic.Extensions;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses.Services;
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
            BoardShapeBase board,
            IList<BlockBase> blocks)
        {
            this.size = blocks.Count;
            this.board = board;
            this.blocks = blocks;
            this.fitnessService = new FitnessService(this.board);
            this.choicesMade = new Stack<IndexedBlockBase>();
            this.remaining = new HashSet<BlockBase>(this.blocks);
        }

        public ImmutableList<IndexedBlockBase> Solution => choicesMade.ToImmutableList();
        public BoardShapeBase Board => this.board;

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
                this.board,
                this.blocks);

            clone.choicesMade = new Stack<IndexedBlockBase>(choicesMade);
            clone.remaining = new HashSet<BlockBase>(remaining);

            return clone;
        }

        public IEnumerable<IndexedBlockBase> GetChoices()
        {
            var results = new List<IndexedBlockBase>();
            var nextOne = remaining.FirstOrDefault();

            if(nextOne == null)
            {
                return results;
            }

            var innerResult = new List<IndexedBlockBase>();

            foreach ( var (pp, index) in nextOne.AllowedLocations.WithIndex())
            {
                innerResult.Add(new IndexedBlockBase(nextOne, index));
            }

            results.AddRange(innerResult);

            return results
                //.Shuffle(new FastRandomRandomization()) or Reverse first <- some extras
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