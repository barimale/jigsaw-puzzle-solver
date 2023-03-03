using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public static class PopulationHelper
    {
        public static IEnumerable<IEnumerable<T>> Permutate<T>(T[][] locationsOfGenes)
        {
            var result = locationsOfGenes.CartesianProduct();

            //var cart = result.Select(tuple => $"({string.Join(", ", tuple)})");
            //Console.WriteLine($"{{{string.Join(", ", cart)}}}");

            return result;
        }

        static IEnumerable<IEnumerable<T>> CartesianProduct<T>
            (this IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct =
              new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
              emptyProduct,
              (accumulator, sequence) =>
                from accseq in accumulator
                from item in sequence
                select accseq.Concat(new[] { item }));
        }
    }
}
