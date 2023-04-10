using Tangram.GameParts.Logic.GameParts.Block;

namespace Algorithm.Tangram.TreeSearch.Logic.Domain
{
    public class IndexedBinaryBlockBase
    {
        public IndexedBinaryBlockBase(BlockBase blockDefinition, int index)
        {
            BlockDefinition = blockDefinition;
            Index = index;

            var clonedBlockDefinition = BlockDefinition.Clone();
            clonedBlockDefinition.Apply(clonedBlockDefinition.AllowedLocations[Index]);
            TransformedBlock = clonedBlockDefinition;
            BinaryBlockOnTheBoard = TransformedBlock.ToBinary();
        }

        public BlockBase BlockDefinition { get; private set; }

        public int Index { get; private set; }

        public BlockBase? TransformedBlock { get; set; }
        public int[] BinaryBlockOnTheBoard { get; set; }
    }
}
