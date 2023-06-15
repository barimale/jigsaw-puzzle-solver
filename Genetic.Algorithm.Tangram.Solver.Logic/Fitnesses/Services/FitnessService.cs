using Algorithm.Tangram.Common.Extensions;
using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Union;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Genetic.Algorithm.Tangram.Solver.Logic.Fitnesses.Services
{
    public class FitnessService
    {
        private BoardShapeBase? board;

        public FitnessService()
        {
            // intentionally left blank
        }

        public FitnessService(BoardShapeBase board)
            : this()
        {
            this.board = board;
        }

        public async Task<double> EvaluateAsync(
            IEnumerable<Geometry> evaluatedGeometry,
            BoardShapeBase board,
            bool withPolygonsIntersectionsDiff = true,
            bool withOutOfBoundsDiff = true,
            bool withVolumeDiff = true,
            bool resultInverted = true)
        {
            var tasks = new List<Task<double>>();

            if (withPolygonsIntersectionsDiff)
            {
                Task<double> polygonsDiff = Task
                    .Factory
                    .StartNew(() => CalculatePolygonsIntersectDiff(evaluatedGeometry.ToArray()));

                tasks.Add(polygonsDiff);
            }

            if (withOutOfBoundsDiff)
            {
                Task<double> outOfBoundsDiff = Task
                .Factory
                .StartNew(() => CalculateOutOfBoundsDiff(evaluatedGeometry.ToArray(), board));

                tasks.Add(outOfBoundsDiff);
            }

            if (withVolumeDiff)
            {
                Task<double> volumeDiff = Task
                    .Factory
                    .StartNew(() => CalculateVolumeDiff(evaluatedGeometry.ToArray(), board));

                tasks.Add(volumeDiff);
            }
            var diffArray = await Task.WhenAll(tasks.AsEnumerable());

            var summarizedDiff = diffArray.Sum();

            if(resultInverted)
            {
                return -1d * summarizedDiff;
            }

            return summarizedDiff;
        }

        public Task<double> EvaluateAsync(
            IEnumerable<Geometry> evaluatedGeometry,
            bool withPolygonsIntersectionsDiff = true,
            bool withOutOfBoundsDiff = true,
            bool withVolumeDiff = true,
            bool resultInverted = true)
        {
            if (this.board == null)
                throw new ArgumentNullException("board cannot be null");

            return EvaluateAsync(
                evaluatedGeometry,
                this.board,
                withPolygonsIntersectionsDiff,
                withOutOfBoundsDiff,
                withVolumeDiff,
                resultInverted);
        }

        public double Evaluate(
            IEnumerable<Geometry> evaluatedGeometry,
            bool withPolygonsIntersectionsDiff = true,
            bool withOutOfBoundsDiff = true,
            bool withVolumeDiff = true,
            bool resultInverted = true)
        {
            if (this.board == null)
                throw new ArgumentNullException("board cannot be null");

            return EvaluateAsync(
                evaluatedGeometry,
                this.board,
                withPolygonsIntersectionsDiff,
                withOutOfBoundsDiff,
                withVolumeDiff,
                resultInverted).Result;
        }

        public int EvaluateBinary(
            IEnumerable<int[]> binaries,
            int boardFieldAmount)
        {
            return EvaluateBinaryAsync(binaries, boardFieldAmount).Result;
        }

        public async Task<int> EvaluateBinaryAsync(
            IEnumerable<int[]> binaries,
            int boardFieldAmount)
        {
            var tasks = new List<Task<int>>();

            Task<int> evaluateBinaries = Task
               .Factory
               .StartNew(() =>
               {
                   return DoEvaluateBinary(binaries, boardFieldAmount);
               });

            tasks.Add(evaluateBinaries);

            var results = await Task.WhenAll(tasks);

            return results.Sum();
        }

        public int DoEvaluateBinary(IEnumerable<int[]> binaries, int boardFieldAmount)
        {
            try
            {
                var sums =
                from array in binaries
                from valueIndex in array.Select((value, index) => new { Value = value, Index = index })
                group valueIndex by valueIndex.Index into indexGroups
                select indexGroups.Select(indexGroup => indexGroup.Value).Sum();

// double check why values below 0 are filtered here: TOD
                var diffSum = sums
                    //.Select(p => Math.Abs(p - 1))
                    .Select(p => p - 1)
                    .Where(pp => pp > 0)
                    .ToArray();

                var diff = 1 * diffSum.Sum();

                return diff;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // TODO: correct it
            return -1;
        }

        public double Evaluate(
            IEnumerable<Geometry> evaluatedGeometry,
            BoardShapeBase board,
            bool withPolygonsIntersectionsDiff = true,
            bool withOutOfBoundsDiff = true,
            bool withVolumeDiff = true,
            bool resultInverted = true)
        {
            return EvaluateAsync(
                evaluatedGeometry,
                board,
                withPolygonsIntersectionsDiff,
                withOutOfBoundsDiff,
                withVolumeDiff,
                resultInverted).Result;
        }

        private double CalculatePolygonsIntersectDiff(Geometry[] polygons)
        {
            double intersections_area_factor = polygons
                .WithIndex()
                .AsParallel()
                .Sum(i =>
            {
                var points_i = polygons[i.index];

                return polygons.WithIndex().AsParallel().Sum(j =>
                {
                    if (i.index != j.index)
                    {
                        var points_j = polygons[j.index];
                        return PolygonsIntersect(points_i, points_j);
                    }

                    return 0d;
                });
            });

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
            double out_of_bounds_distances = evaluatedGeometry
                .AsParallel()
                .Sum(geometry =>
                {
                    return FilterOutOfBounds(
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
                return diff.Area;
            }
        }
    }
}
