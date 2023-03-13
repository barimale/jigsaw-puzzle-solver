using Genetic.Algorithm.Tangram.Solver.Domain.Generators;

namespace Genetic.Algorithm.Tangram.Solver.Domain
{
    public class GeneratorsFactory
    {
        public AllowedLocationsGenerator AllowedLocationsGenerator => new AllowedLocationsGenerator();

        public FieldsGenerator FieldsGenerator => new FieldsGenerator();
    }
}
