using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Algorithm.Tangram.TreeSearch.Logic.Domain
{
    public class IndexedBinaryBlockBase: IndexedBlockBase
    {
        private readonly IList<BoardFieldDefinition> boardFieldsDefinition;
        private readonly int[] binaryBlockOnTheBoard;

        public IndexedBinaryBlockBase(
            IList<BoardFieldDefinition> boardFieldsDefinition,
            BlockBase blockDefinition,
            int index)
            : base(blockDefinition, index)
        {
            this.boardFieldsDefinition = boardFieldsDefinition;
            if(TransformedBlock == null)
            {
                var i = 0;
            }
            binaryBlockOnTheBoard = TransformedBlock.ToBinary(this.boardFieldsDefinition);
        }

        public int[] BinaryBlockOnTheBoard => binaryBlockOnTheBoard;
    }
}
