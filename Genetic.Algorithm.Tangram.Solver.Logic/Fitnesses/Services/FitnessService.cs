using Genetic.Algorithm.Tangram.Solver.Domain.Board;
using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Union;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses.Services
{
    internal class FitnessService
    {
        public FitnessService()
        {
            // intentionally left blank
        }

        public double Evaluate(IEnumerable<Geometry> evaluatedGeometry, BoardShapeBase board)
        {
            return EvaluateAsync(evaluatedGeometry, board).Result;
        }

        public async Task<double> EvaluateAsync(
            IEnumerable<Geometry> evaluatedGeometry,
            BoardShapeBase board)
        {
            Task<double> polygonsDiff = Task
                .Factory
                .StartNew(() => CalculatePolygonsIntersectDiff(evaluatedGeometry.ToArray()));

            Task<double> outOfBoundsDiff = Task
                .Factory
                .StartNew(() => CalculateOutOfBoundsDiff(evaluatedGeometry.ToArray(), board));

            Task<double> volumeDiff = Task
                .Factory
                .StartNew(() => CalculateVolumeDiff(evaluatedGeometry.ToArray(), board));

            var diffArray = await Task.WhenAll(
                polygonsDiff,
                outOfBoundsDiff,
                volumeDiff);

            var summarizedDiff = diffArray.Sum();
            var invertedSummarizedDiff = -1d * summarizedDiff;

            return invertedSummarizedDiff;
        }

        // TODO: parallel deeper
        private double CalculatePolygonsIntersectDiff(Geometry[] polygons)
        {
            var intersections_area_factor = 0d;

            for (int i = 0; i < polygons.Length; i++)
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

            if (diff.IsEmpty)
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
