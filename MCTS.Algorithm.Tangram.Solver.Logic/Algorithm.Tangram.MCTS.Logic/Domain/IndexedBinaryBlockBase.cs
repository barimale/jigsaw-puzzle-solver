using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Algorithm.Tangram.TreeSearch.Logic.Domain
{
    public class IndexedBinaryBlockBase
    {
        public IndexedBinaryBlockBase(
            IList<BoardFieldDefinition> boardFieldsDefinition,
            BlockBase blockDefinition,
            int index)
        {
            BoardFieldsDefinition = boardFieldsDefinition;
            BlockDefinition = blockDefinition;
            Index = index;

            var clonedBlockDefinition = BlockDefinition.Clone();
            clonedBlockDefinition.Apply(clonedBlockDefinition.AllowedLocations[Index]);
            TransformedBlock = clonedBlockDefinition;
            BinaryBlockOnTheBoard = TransformedBlock.ToBinary(BoardFieldsDefinition);
        }

    public IList<BoardFieldDefinition> BoardFieldsDefinition { private set; get; }


    public BlockBase BlockDefinition { get; private set; }

        public int Index { get; private set; }

        public BlockBase? TransformedBlock { get; set; }
        public int[] BinaryBlockOnTheBoard { get; set; }
    }
}
