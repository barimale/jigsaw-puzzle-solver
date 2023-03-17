using Genetic.Algorithm.Tangram.Solver.Domain.Generators;

namespace Genetic.Algorithm.Tangram.Solver.Domain
{
    public class GeneratorFactory
    {
        public AllowedLocationsGenerator AllowedLocationsGenerator => new AllowedLocationsGenerator();

        public RectangularBoardFieldsGenerator RectangularBoardFieldsGenerator => new RectangularBoardFieldsGenerator();
    }
}
