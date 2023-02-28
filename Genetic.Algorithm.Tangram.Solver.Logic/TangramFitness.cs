using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public class TangramFitness : IFitness
    {
        private BoardShapeBase boardShapeDefinition;
        private IList<BlockBase> blocks;

        public TangramFitness(
            BoardShapeBase boardShapeDefinition,
            IList<BlockBase> blocks)
        {
            // TODO: use width, height etc and check the fitness
            this.boardShapeDefinition = boardShapeDefinition;
            this.blocks = blocks;
        }

        // TODO: implement as is in ga-tangram
        // use the ConvexHull method from the nettopologysuite
        public double Evaluate(IChromosome chromosome)
        {
            // put to the chromosome collection of blocks 
            // use two or three genes only but it needs to be checked
            var aa = chromosome as TangramChronosome;
            // get data from the chromosome
            // based on it clone the specific block 
            // from the collection
            double n = 9;
            var x = (int)chromosome.GetGene(0).Value;
            var y = (int)chromosome.GetGene(1).Value;
            double f1 = System.Math.Pow(15 * x * y * (1 - x) * (1 - y) * System.Math.Sin(n * System.Math.PI * x) * System.Math.Sin(n * System.Math.PI * y), 2);

            return f1;
        }
    }
}
