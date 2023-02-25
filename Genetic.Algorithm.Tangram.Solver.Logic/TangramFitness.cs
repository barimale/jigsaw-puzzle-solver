using GeneticSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public class TangramFitness : IFitness
    {
        // TODO
        public double Evaluate(IChromosome chromosome)
        {
            double n = 9;
            var x = (int)chromosome.GetGene(0).Value;
            var y = (int)chromosome.GetGene(1).Value;
            double f1 = System.Math.Pow(15 * x * y * (1 - x) * (1 - y) * System.Math.Sin(n * System.Math.PI * x) * System.Math.Sin(n * System.Math.PI * y), 2);

            return f1;
        }
    }
}
