namespace Genetic.Algorithm.Tangram.Common.Extensions.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static IEnumerable<IEnumerable<T>> Permutate<T>(this T[][] sequences)
        {
            var result = sequences.CartesianProduct();

            return result;
        }

        public static IEnumerable<IEnumerable<T>> PermutatePartially<T>(this T[][] sequences, double percentOfAllPermutationsInPercents)
        {
            var result = sequences.CartesianProduct();

            return result;
        }

        public static IEnumerable<IEnumerable<T>> Permutate<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            var result = sequences.CartesianProduct();

            return result;
        }

        private static IEnumerable<IEnumerable<T>> CartesianProduct<T>
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
