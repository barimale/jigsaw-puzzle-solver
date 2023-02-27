using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using GeneticSharp;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public class TangramFitness : IFitness
    {
        public TangramFitness(BoardShapeBase boardShapeDefinition)
        {
            BoardShapeDefinition = boardShapeDefinition;
        }

        public BoardShapeBase BoardShapeDefinition { private set; get; }

        // TODO: implement as is in ga-tangram 
        public double Evaluate(IChromosome chromosome)
        {
            var aa = chromosome as TangramChronosome;
            double n = 9;
            var x = (int)chromosome.GetGene(0).Value;
            var y = (int)chromosome.GetGene(1).Value;
            double f1 = System.Math.Pow(15 * x * y * (1 - x) * (1 - y) * System.Math.Sin(n * System.Math.PI * x) * System.Math.Sin(n * System.Math.PI * y), 2);

            return f1;
        }
    }
}
