using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using GeneticSharp;
using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Union;
using System.Collections.Immutable;

namespace Genetic.Algorithm.Tangram.Solver.Logic
{
    public class TangramFitness : IFitness
    {
        private BoardShapeBase boardShapeDefinition;
        private IList<BlockBase> blocks;

        public TangramFitness(
            BoardShapeBase boardShapeDefinition,
            IList<BlockBase> blocks)
        {
            this.boardShapeDefinition = boardShapeDefinition;
            this.blocks = blocks;
        }

        public double Evaluate(IChromosome chromosome)
        {
            // TODO: debug it why fitness 1 is minimum and means the solution is found
            // custom mutation class
            // check the diff between mutation and cross
            var solution = chromosome as TangramChromosome;
            var evaluatedGeometry = solution
                .GetGenes()
                .Select(p => p.Value as BlockBase)
                .Select(pp => pp.Polygon)
                .ToImmutableList();

            // measure diff factors
            var f1 = CalculatePolygonsIntersectDiff(evaluatedGeometry.ToArray());
            f1 += CalculateOutOfBoundsDiff(
                evaluatedGeometry.ToArray(),
                boardShapeDefinition);
            f1 += CalculateVolumeDiff(
                evaluatedGeometry.ToArray(),
                boardShapeDefinition);

            return f1;
        }

        private double CalculatePolygonsIntersectDiff(Geometry[] polygons)
        {
            var intersections_area_factor = 0d;

            for(int i = 0; i< polygons.Length; i++)
            {
                var points_i = polygons[i];
                for (int j = 0; j < polygons.Length; j++)
                {
                    if (i == j) continue;

                    var points_j = polygons[j];
                    intersections_area_factor += PolygonsIntersect(points_i, points_j);
                }
            }

            return intersections_area_factor;
        }

        private double CalculateVolumeDiff(Geometry[] evaluatedGeometry, BoardShapeBase boardShapeDefinition)
        {
            var volume_diff = 0d;

            var boardConvexedHullVolume = boardShapeDefinition
                .Polygon
                .ConvexHull()
                .Area;

            var mergedPolygon = new CascadedPolygonUnion(evaluatedGeometry)
                .Union();
            var mergedPolygonConvexedHullVolume = mergedPolygon
                .ConvexHull()
                .Area;

            volume_diff += Math.Abs(
                mergedPolygonConvexedHullVolume -
                boardConvexedHullVolume);

            return volume_diff;
        }

        private double CalculateOutOfBoundsDiff(
            Geometry[] evaluatedGeometry,
            BoardShapeBase boardShapeDefinition)
        {
            var out_of_bounds_distances = 0d;

            evaluatedGeometry.ToList()
                .ForEach(geometry =>
                {
                    out_of_bounds_distances += FilterOutOfBounds(
                        geometry,
                        boardShapeDefinition);
                });

            return out_of_bounds_distances;
        }

        private double PolygonsIntersect(Geometry polygon1, Geometry polygon2)
        {
            var isIntersected = polygon1.Intersects(polygon2);

            if (isIntersected)
            {
                var result = polygon1.Intersection(polygon2);
                return result.Area;
            }
            else
            {
                return 0d;
            }
        }

        private double FilterOutOfBounds(
            Geometry polygon,
            BoardShapeBase boardShapeDefinition)
        {
            var diff = polygon.Difference(boardShapeDefinition.Polygon);

            if(diff.IsEmpty)
            {
                return 0d;
            }
            else
            {
                // TODO: correct it as described in ga-tangram
                //var distance = polygon.Distance(boardShapeDefinition.Polygon);

                return diff.Area;
            }
        }
    }
}
