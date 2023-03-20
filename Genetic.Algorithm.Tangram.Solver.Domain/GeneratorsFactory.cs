using Tangram.GameParts.Logic.Generators;

namespace Tangram.GameParts.Logic
{
    public class GeneratorFactory
    {
        public AllowedLocationsGenerator AllowedLocationsGenerator => new AllowedLocationsGenerator();

        public RectangularGameFieldsGenerator RectangularGameFieldsGenerator => new RectangularGameFieldsGenerator();
    }
}
