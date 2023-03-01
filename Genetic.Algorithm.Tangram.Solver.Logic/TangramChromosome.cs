using Genetic.Algorithm.Tangram.Solver.Logic.Extensions;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using GeneticSharp;
using System.Collections.Immutable;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public class TangramChromosome : ChromosomeBase
    {
        private ImmutableArray<BlockBase> blocks;
        private BoardShapeBase boardShapeDefinition;
        private int[] allowedAngles;

        private int allowedAnglesCount => allowedAngles.Length;

        public TangramChromosome(
            IList<BlockBase> blocks,
            BoardShapeBase boardShapeDefinition,
            int[] allowedAngles)
            : base(blocks.Count)
        {
            this.boardShapeDefinition = boardShapeDefinition;
            this.allowedAngles = allowedAngles;
            this.blocks = blocks.ToImmutableArray();

            foreach (var (block, index) in this.blocks.WithIndex())
            {
                ReplaceGene(index, GenerateGene(index));
            }
        }

        public int GenesCount => this.blocks.Length;

        // These properties represents your phenotype.
        public BlockBase this[int index] => (BlockBase)GetGene(index).Value;

        public override Gene GenerateGene(int geneIndex)
        {
            BlockBase newBlock = this.blocks[geneIndex].Clone();

            // angle random
            var allowedAnglesIndex = RandomizationProvider
                    .Current
                    .GetInt(0, allowedAnglesCount - 1);
            var newAngle = allowedAngles[allowedAnglesIndex];
            newBlock.Rotate(newAngle);

            // true false isflipped
            var trueOrFalse = RandomizationProvider.Current.GetInt(0, 1);
            var hasToBeReflected = trueOrFalse == 1 ? true : false;
            if(hasToBeReflected)
                newBlock.Reflection();

            // width range - position across X line
            var newX = RandomizationProvider
                    .Current
                    .GetInt(0, boardShapeDefinition.Width);

            // height range - position across Y line
            var newY = RandomizationProvider
                   .Current
                   .GetInt(0, boardShapeDefinition.Height);
            newBlock.MoveTo(newX, newY);

            return new Gene(newBlock);
        }

        public override IChromosome CreateNew()
        {
            return new TangramChromosome(
                blocks,
                boardShapeDefinition,
                allowedAngles);
        }

        // TODO:correct it, only first gen is cloned
        // according to tbe base class implementation
        public override IChromosome Clone()
        {
            var clone = base.Clone() as TangramChromosome;

            return clone;
        }
    }
}