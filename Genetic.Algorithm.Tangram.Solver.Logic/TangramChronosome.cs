using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public class TangramChronosome : ChromosomeBase
    {
        private BoardShapeBase boardShapeDefinition;
        private int[] allowedAngles;

        private int allowedAnglesCount => allowedAngles.Length;

        public TangramChronosome(
            BoardShapeBase boardShapeDefinition,
            int[] allowedAngles)
            : base(4)
        {
            this.boardShapeDefinition = boardShapeDefinition;
            this.allowedAngles = allowedAngles;

            ReplaceGene(0, GenerateGene(0));
            ReplaceGene(1, GenerateGene(1));
            ReplaceGene(2, GenerateGene(2));
            ReplaceGene(3, GenerateGene(3));
        }

        // These properties represents your phenotype.
        public int X
        {
            get
            {
                return (int)GetGene(0).Value;
            }
        }

        public int Y
        {
            get
            {
                return (int)GetGene(1).Value;
            }
        }

        public int Angle
        {
            get
            {
                return (int)GetGene(2).Value;
            }
        }

        public bool IsFlipped
        {
            get
            {
                return (bool)GetGene(3).Value;
            }
        }

        public override Gene GenerateGene(int geneIndex)
        {
            object value;

            if (geneIndex == 0)
            {
                value = RandomizationProvider
                    .Current
                    .GetInt(0, boardShapeDefinition.Width);// width range
            }
            else if(geneIndex == 1)
            {
                value = RandomizationProvider
                    .Current
                    .GetInt(0, boardShapeDefinition.Height);// height range
            }
            else if (geneIndex == 2)
            {
                var index = RandomizationProvider
                    .Current
                    .GetInt(0, allowedAnglesCount -1);
                value = allowedAngles[index];// angle random
            }
            else
            {
                var trueOrFalse = RandomizationProvider.Current.GetInt(0, 1);
                value = trueOrFalse == 1 ? true : false; // true false isflipped
            }

            return new Gene(value);
        }

        public override IChromosome CreateNew()
        {
            return new TangramChronosome(
                boardShapeDefinition,
                allowedAngles);
        }

        // TODO:correct it, only first gen is cloned
        // according to tbe base class implementation
        public override IChromosome Clone()
        {
            var clone = base.Clone() as TangramChronosome;

            return clone;
        }
    }
}