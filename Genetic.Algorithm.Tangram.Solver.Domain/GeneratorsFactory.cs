using Genetic.Algorithm.Tangram.Solver.Domain.Generators;

namespace Genetic.Algorithm.Tangram.Solver.Domain
{
    public class GeneratorFactory
    {
        public AllowedLocationsGenerator AllowedLocationsGenerator => new AllowedLocationsGenerator();

        public RectangularGameFieldsGenerator RectangularGameFieldsGenerator => new RectangularGameFieldsGenerator();
    }
}
