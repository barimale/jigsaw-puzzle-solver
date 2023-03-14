using Genetic.Algorithm.Tangram.Solver.Domain.Block;

namespace Algorithm.Tangram.MCTS.Logic.Domain
{
    public class IndexedBlockBase
    {
        public IndexedBlockBase(BlockBase blockDefinition, int index)
        {
            this.BlockDefinition = blockDefinition;
            this.Index = index;

            var clonedBlockDefinition = this.BlockDefinition.Clone();
            clonedBlockDefinition.Apply(clonedBlockDefinition.AllowedLocations[this.Index]);
            this.TransformedBlock = clonedBlockDefinition;
        }

        public BlockBase BlockDefinition { get; private set; }

        public int Index { get; private set; }

        public BlockBase? TransformedBlock { get; set; }
    }
}
