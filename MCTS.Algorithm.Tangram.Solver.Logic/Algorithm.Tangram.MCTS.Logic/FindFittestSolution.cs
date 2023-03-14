using Algorithm.Tangram.MCTS.Logic.Domain;
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
        }

        private int CheckFitness()
        {
            var evaluatedGeometry = this.choicesMade
                .Select(p => p.TransformedBlock)
                .ToList()
                .Select(pp => pp.Polygon)
                .ToImmutableList();

            var diff = fitnessService.Evaluate(
                evaluatedGeometry.ToArray(),
                this.board);

            var diffAsInt = diff > Int32.MaxValue ? Int32.MaxValue : Convert.ToInt32(diff * 100);

            return 0;
        }

        public bool IsTerminal => choicesMade.Count == size;

        public Minimize Bound => new Minimize(CheckFitness());

        public Minimize? Quality => IsTerminal ? new Minimize(CheckFitness()) : null;

        public void Apply(IndexedBlockBase choice)
        {
            var indexedChoice = new IndexedBlockBase(
                choice.BlockDefinition,
                choice.Index + 1);

            choicesMade.Push(indexedChoice);
        }

        public object Clone()
        {
            var clone = new FindFittestSolution(
                this.size,
                this.board,
                this.blocks);

            clone.choicesMade = new Stack<IndexedBlockBase>(choicesMade.Reverse());

            return clone;
        }

        public IEnumerable<IndexedBlockBase> GetChoices()
        {
            return this.blocks
                .Skip(choicesMade.Count)
                .Select(p => new IndexedBlockBase(p, 0)) // TODO: double check it
                .AsEnumerable();
        }

        public void UndoLast()
        {
            choicesMade.Pop();
        }

        public override string ToString()
        {
            return $"FindFittestSolution [{string.Join(", ", choicesMade.Reverse())}]";
        }
    }
}