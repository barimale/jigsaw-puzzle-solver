using Algorithm.Tangram.TreeSearch.Logic.Domain;
using System.Collections.Immutable;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Algorithm.SAT.Logic
{
    public class FindSATFittestSolution
    {
        // settings
        private int size => this.blocks.Count;
        private Stack<IndexedBinaryBlockBase> choicesMade;
        public HashSet<BlockBase> remaining;
        public bool HasError { get; set; } = false;
        public string Fitness { get; set; } = 0.ToString();

        // game parts
        private readonly BoardShapeBase board;
        private readonly IList<BlockBase> blocks;

        public FindSATFittestSolution(
            BoardShapeBase board,
            IList<BlockBase> blocks)
        {
            this.ID = Guid.NewGuid().ToString();

            this.board = board;
            this.blocks = new List<BlockBase>(blocks);

            choicesMade = new Stack<IndexedBinaryBlockBase>();
            remaining = new HashSet<BlockBase>(this.blocks);
        }

        public string ID { private set; get; }
        public ImmutableList<IndexedBinaryBlockBase> Solution => choicesMade.ToImmutableList();
        public BoardShapeBase Board => board;
        public IList<BlockBase> Blocks => blocks.ToImmutableList();

        public bool IsTerminal => choicesMade.Count == size;

        public bool ApplyChoice(IndexedBinaryBlockBase choice)
        {
            if (IsTerminal)
            {
                return false;
            }

            choicesMade.Push(choice);

            return true;
        }

        public override string ToString()
        {
            return $"FindSATFittestSolution [{string.Join(", ", choicesMade)}]";
        }
    }
}