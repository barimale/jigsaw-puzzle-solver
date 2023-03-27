using NetTopologySuite.Geometries.Utilities;
using NetTopologySuite.Geometries;
using System.Collections.Immutable;
using Tangram.GameParts.Logic.Utilities;

namespace Tangram.GameParts.Logic.GameParts.Block
{
    // TODO reuse later on when AllowedLocation modified and IOC
    public class AllowedLocation
    {
        public static TransformationHelper TransformationHelper => new TransformationHelper();

        private ImmutableList<AffineTransformation> orderedTransformationsHistory;

        public AllowedLocation(Geometry polygon, params AffineTransformation[] transformations)
        {
            Polygon = polygon;
            orderedTransformationsHistory = transformations.ToImmutableList();
        }

        public Geometry Polygon { private set; get; }

        IImmutableList<AffineTransformation> TransformationsHistory => orderedTransformationsHistory
            .ToImmutableList();
    }
}
