using Tangram.GameParts.Logic.GameParts.Block;

namespace Algorithm.Tangram.TreeSearch.Logic.Domain
{
    public class IndexedBlockBase
    {
        private readonly BlockBase transformedBlock;

        public IndexedBlockBase(BlockBase blockDefinition, int index)
        {
            BlockDefinition = blockDefinition;
            Index = index;

            var clonedBlockDefinition = BlockDefinition.Clone();
            clonedBlockDefinition.Apply(clonedBlockDefinition.AllowedLocations[Index]);
            transformedBlock = clonedBlockDefinition;
        }

        public BlockBase BlockDefinition { get; private set; }

        public int Index { get; private set; }

        public BlockBase TransformedBlock => transformedBlock;
    }
}
