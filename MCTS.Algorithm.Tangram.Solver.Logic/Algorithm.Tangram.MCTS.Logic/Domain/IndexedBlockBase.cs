using Tangram.GameParts.Logic.Block;

namespace Algorithm.Tangram.TreeSearch.Logic.Domain
{
    public class IndexedBlockBase
    {
        public IndexedBlockBase(BlockBase blockDefinition, int index)
        {
            BlockDefinition = blockDefinition;
            Index = index;

            var clonedBlockDefinition = BlockDefinition.Clone();
            clonedBlockDefinition.Apply(clonedBlockDefinition.AllowedLocations[Index]);
            TransformedBlock = clonedBlockDefinition;
        }

        public BlockBase BlockDefinition { get; private set; }

        public int Index { get; private set; }

        public BlockBase? TransformedBlock { get; set; }
    }
}
