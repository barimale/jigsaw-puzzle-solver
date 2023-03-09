using Genetic.Algorithm.Tangram.Common.Extensions.Extensions;
using Genetic.Algorithm.Tangram.Solver.Domain.Block;
using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using GeneticSharp;
using System.Collections.Immutable;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Chromosome
{
    public class TangramChromosome : ChromosomeBase
    {
        private ImmutableArray<BlockBase> blocks;
        private int[] allowedAngles;
        private int allowedAnglesCount => allowedAngles.Length;

        public TangramChromosome(
            IList<BlockBase> blocks,
            BoardShapeBase boardShapeDefinition,
            int[] allowedAngles)
            : base(blocks.Count)
        {
            BoardShapeDefinition = boardShapeDefinition;

            this.allowedAngles = allowedAngles;
            this.blocks = blocks.ToImmutableArray();

            foreach (var (block, index) in this.blocks.WithIndex())
            {
                ReplaceGene(index, GenerateGene(index));
            }
        }
        public BoardShapeBase BoardShapeDefinition { private set; get; }

        public int GenesCount => blocks.Length;

        // These properties represents your phenotype.
        public BlockBase this[int index] => (BlockBase)GetGene(index).Value;

        public override Gene GenerateGene(int geneIndex)
        {
            BlockBase newBlock = blocks[geneIndex].Clone();
            var toStringFromClone = newBlock.ToString();

            // do one random only if allowed locations of the block specified
            if (newBlock.IsAllowedLocationsEnabled)
            {
                var randomIndex = new FastRandomRandomization()
                    .GetInt(
                        0,
                        newBlock.AllowedLocations.Length);

                var randomMutatedBlock = newBlock.AllowedLocations[randomIndex];
                newBlock.Apply(randomMutatedBlock);

                return new Gene(newBlock);
            }

            // angle random
            var allowedAnglesIndex = new FastRandomRandomization()
                    .GetInt(0, allowedAnglesCount);
            var newAngle = allowedAngles[allowedAnglesIndex];
            newBlock.Rotate(newAngle);

            // true false isflipped
            var trueOrFalse = new FastRandomRandomization().GetInt(0, 1);
            var hasToBeReflected = trueOrFalse == 1 ? true : false;
            if (hasToBeReflected)
                newBlock.Reflection();

            // width range - position across X line
            var newX = new FastRandomRandomization()
                .GetInt(
                        0,
                        Convert.ToInt32(
                            Math.Round(
                                1 * BoardShapeDefinition.ScaleFactor + BoardShapeDefinition.Polygon.Boundary.EnvelopeInternal.Width -
                                    newBlock.Polygon.Boundary.EnvelopeInternal.Width,
                                MidpointRounding.AwayFromZero
                            )// as the minimal
                        )
                    );

            // height range - position across Y line
            var newY = new FastRandomRandomization()
                .GetInt(
                        0,
                            Convert.ToInt32(
                            Math.Round(
                                1 * BoardShapeDefinition.ScaleFactor + BoardShapeDefinition.Polygon.Boundary.EnvelopeInternal.Height -
                                    newBlock.Polygon.Boundary.EnvelopeInternal.Height,
                                MidpointRounding.AwayFromZero
                            )
                        )
                    );
            newBlock.MoveTo(newX, newY);

            return new Gene(newBlock);
        }

        public override IChromosome CreateNew()
        {
            return new TangramChromosome(
                blocks,
                BoardShapeDefinition,
                allowedAngles);
        }

        public override IChromosome Clone()
        {
            var clone = base.Clone() as TangramChromosome;

            return clone;
        }
    }
}