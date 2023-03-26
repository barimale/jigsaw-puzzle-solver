using Algorithm.Tangram.Common.Extensions;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Tangram.GameParts.Logic.Extensions
{
    public static class BlockBaseExtensions
    {
        // WIP all combinations with single allowed locations
        // as a set of algorithms to be executed
        public static IList<BlockBase[]> GetNxNWithSingleAllowedLocations (this IEnumerable<BlockBase> blocksWithAllowedLocations)
        {
            var result = blocksWithAllowedLocations
                .WithIndex()
                .AsParallel()
                .SelectMany(block =>
            {
                // transformed to n roots with 1 allowed location
                var rootBlocks = block.item.AllowedLocations.WithIndex().Select(p =>
                {
                    var cloned = block.item.Clone();
                    cloned.SetAllowedLocations(new NetTopologySuite.Geometries.Geometry[1] { p.item });

                    return cloned;
                }).ToList();

                return rootBlocks.Select(p =>
                {
                    var copiedCollection = new List<BlockBase>(blocksWithAllowedLocations.ToList()).ToArray();

                    copiedCollection[block.index] = p;

                    return copiedCollection;
                }).ToList();
            }).ToList();

            return result;
        }
    }
}
