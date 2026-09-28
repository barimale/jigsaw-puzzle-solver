using Algorithm.Tangram.TreeSearch.Logic;
using Algorithm.Tangram.TreeSearch.Logic.Domain;
using Google.OrTools.Sat;
using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Union;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Solver.Tangram.AlgorithmDefinitions.OrToolsHelper
{
    public static class OrToolsHelper
    {
        public readonly record struct Cell(int X, int Y);

        public record class Placement
        {
            public int Id { get; set; }

            public List<Cell> Cells { get; set; } = new List<Cell>();
        }

        public class Piece
        {
            public string Name { get; set; } = "";

            public List<Placement> Placements { get; set; } = new List<Placement>();
        }

        public static List<Cell> GetOccupiedCells(Geometry geometry)
        {
            var result = new List<Cell>();

            var envelope = geometry.EnvelopeInternal;

            var minX = (int)Math.Floor(envelope.MinX);
            var maxX = (int)Math.Ceiling(envelope.MaxX);

            var minY = (int)Math.Floor(envelope.MinY);
            var maxY = (int)Math.Ceiling(envelope.MaxY);

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    var center = new Point(x + 0.5, y + 0.5);

                    if (geometry.Contains(center))
                    {
                        result.Add(new Cell(x, y));
                    }
                }
            }

            return result;
        }

        public static Geometry CreateGeometry(IEnumerable<Cell> cells)
        {
            var geometryFactory = new GeometryFactory();

            var polygons = cells
            .Select(c =>
            geometryFactory.CreatePolygon(new[]
            {
                new Coordinate(c.X, c.Y),
                new Coordinate(c.X + 1, c.Y),
                new Coordinate(c.X + 1, c.Y + 1),
                new Coordinate(c.X, c.Y + 1),
                new Coordinate(c.X, c.Y)
            }))
            .ToArray();

            if (polygons.Length == 0)
            {
                return geometryFactory.CreatePolygon();
            }

            return UnaryUnionOp.Union(polygons);
        }

        public static FindSATFittestSolution Solve(BoardShapeBase board, IList<BlockBase> positions)
        {
            var pieces = positions.Select(x => new Piece
            {
                Name = x.ID.ToString(),

                Placements = x.AllowedLocations
                    .Select((geometry, index) => new Placement
                    {
                        Id = index,
                        Cells = GetOccupiedCells(geometry)
                    })
                    .ToList()
            }).ToList();

            CpModel model = new();

            // x[piece, placement]
            Dictionary<(string Piece, int PlacementId), BoolVar> vars = new();

            foreach (var piece in pieces)
            {
                foreach (var placement in piece.Placements.Distinct())
                {
                    vars[(piece.Name, placement.Id)] =
                    model.NewBoolVar(
                    $"P_{piece.Name}_{placement.Id}");
                }
            }

            // każdy klocek dokładnie raz

            foreach (var piece in pieces)
            {
                model.Add(
                LinearExpr.Sum(
                piece.Placements
                .Select(p =>
                vars[(piece.Name, p.Id)]))
                == 1);
            }

            // mapa pokrycia pól

            Dictionary<Cell, List<BoolVar>> coveredBy = new();

            foreach (var piece in pieces)
            {
                foreach (var placement in piece.Placements)
                {
                    var placementVar =
                    vars[(piece.Name, placement.Id)];

                    foreach (var cell in placement.Cells)
                    {
                        if (!coveredBy.ContainsKey(cell))
                        {
                            coveredBy[cell] = new List<BoolVar>();
                        }

                        coveredBy[cell].Add(placementVar);
                    }
                }
            }

            // każde pole pokryte dokładnie raz

            foreach (var cell in coveredBy.Keys)
            {
                model.Add(
                LinearExpr.Sum(coveredBy[cell])
                == 1);
            }

            CpSolver solver = new();

            var status = solver.Solve(model);

            if (status != CpSolverStatus.Optimal &&
            status != CpSolverStatus.Feasible)
            {
                return null;
            }

            var result = new Dictionary<string, Placement>();

            foreach (var piece in pieces)
            {
                foreach (var placement in piece.Placements)
                {
                    if (solver.Value(
                    vars[(piece.Name, placement.Id)]) == 1)
                    {
                        result[piece.Name] = placement; // CreateGeometry
                        break;
                    }
                }
            }

            var mapped = new FindSATFittestSolution(board, positions)
            {
                Fitness = 0.ToString(),
            };

            foreach(var item in result)
            {
                var block = positions.FirstOrDefault(x => x.ID.ToString() == item.Key);
                if (block != null)
                {
                    var geometry = CreateGeometry(item.Value.Cells);
                    block.Apply(geometry);
                    mapped.ApplyChoice(new IndexedBinaryBlockBase(board.BoardFieldsDefinition, block, item.Value.Id));
                }
            }

            return mapped;
        }
    }
}
