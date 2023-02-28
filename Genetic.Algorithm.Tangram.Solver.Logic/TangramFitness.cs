using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Blocks;
using Genetic.Algorithm.Tangram.Solver.Logic.GameParts.Board;
using GeneticSharp;

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
            // TODO: use width, height etc and check the fitness
            this.boardShapeDefinition = boardShapeDefinition;
            this.blocks = blocks;
        }

        // TODO: implement as is in ga-tangram
        // use the ConvexHull method from the nettopologysuite
        public double Evaluate(IChromosome chromosome)
        {
            var solution = chromosome as TangramChronosome;
            // build a merged polygon based on the solution

            // measure diff factors
            var f1 = CalculatePolygonsIntersectDiff();
            f1 += CalculateOutOfBoundsDiff();
            f1 += CalculateVolumeDiff();

            return f1;
        }

        private double FilterOutOfBounds(object point, object boardPolygon)
        {
            //            #if a point is out of the square,
            //# calculate the distance in x and y and return a sum
            //    distance = 0
            //    if point[0] > 650:
            //        distance += point[0]-650
            //    if point[0] < 0:
            //        distance += (point[0]*-1)-10
            //    if point[1] > 650:
            //        distance += point[1]-650
            //    if point[1] < -10:
            //        distance += (point[0]*-1)-10
            //    return 
        }

        private double PolygonsIntersect(object polygon1, object polygon2)
        {
            //# calculate intersection area between two polygons
            //            p1 = Polygon(points1)
            //    p2 = Polygon(points2)
            //    return p1.intersection(p2).area
        }

        private double CalculatePolygonsIntersectDiff(object mergedPolygon, object boardPolygon)
        {
            //# find and sum all intersections areas between shapes and add it to the fitness
            //            intersections_area = 0
            //    for i in range(len(shapeArray)):
            //        points_i = shapeArray[i].getRotatedPoints(
            //            genome[i][0],
            //            genome[i][1],
            //            genome[i][2]
            //        )
            //        for j in range(len(shapeArray)):
            //            if i != j:
            //                points_j = shapeArray[j].getRotatedPoints(
            //                    genome[j][0],
            //                    genome[j][1],
            //                    genome[j][2]
            //                )
            //                intersections_area += polygonsIntersect(points_i, points_j)
            //    fitness += intersections_area
        }

        private double CalculateVolumeDiff(object mergedPolygon, object boardPolygon)
        {
            //# calculate convex hull volume and add
            //# the diffrence from the square to the fitness value
            //            convex_volume = ConvexHull(np.array(points)).volume
            //    # diff the areas
            //    volume_diff = abs(convex_volume - (640 * *2))
            //    fitness += volume_diff
        }

        private double CalculateOutOfBoundsDiff(object point)
        {
            //# calculate distance of all points that are out of bounds of the square
            //            out_of_bounds_distances = map(filterOutOfBounds, points)
            //    fitness += sum(out_of_bounds_distances)
        }
    }
}
