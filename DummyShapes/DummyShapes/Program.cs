using ConsoleGraphics.Graphics2D.Bases;
using geometry = NetTopologySuite.Geometries;
using algorithm = NetTopologySuite.Algorithm;
using System.Diagnostics;

Scene2D Scene = new Scene2D(GraphicsType.ColoredPoints);
int sceneScaleFactor = 2;

int fieldWidthAndHeight = 10;

int paddingTop = 5;
int paddingRight = 10;

int widthScaleFactor = 2;
int withNotScaled = 10;
int width = widthScaleFactor * withNotScaled;
int height = 5;
int boardBorderWidth = 2;

var blue5x1 = CreateBlue5x1();
Scene.Add(blue5x1);
var L = CreateLightBlueL3x3();
Scene.Add(L);

var yellow = CreateYellow3x3();
Scene.Add(yellow);

var purple = CreatePurple();
Scene.Add(purple);

var nettopologysuite = Convert();
Scene.Add(nettopologysuite);

Scene2D Scene2 = new Scene2D(GraphicsType.ColoredSymbols);

Point AAA = new Point(paddingRight + 0 - boardBorderWidth, paddingTop + 0 - boardBorderWidth, ConsoleColor.White, "AAA", '-');
Point BBB = new Point(paddingRight + width + boardBorderWidth, paddingTop + 0 - boardBorderWidth, ConsoleColor.White, "BBB", '-');
Point C = new Point(paddingRight + width + boardBorderWidth, paddingTop + height + boardBorderWidth, ConsoleColor.White, "CCC", '-');
Point D = new Point(paddingRight + 0 - boardBorderWidth, paddingTop + height + boardBorderWidth, ConsoleColor.White, "DDD", '-');
LineSegment AAABBBB = AAA + BBB;
LineSegment CD = C + D;
Shape ABCD = AAABBBB + CD;
Scene2.Add(ABCD);

Console.ReadKey();

Shape CreateBlue5x1()
{

    Point p11 = new Point(paddingRight + 0, paddingTop + 0, ConsoleColor.DarkBlue);
    Point p12 = new Point(paddingRight + 0, paddingTop + 5, ConsoleColor.DarkBlue);
    Point p21 = new Point(paddingRight + 1 * widthScaleFactor, paddingTop + 0, ConsoleColor.DarkBlue);
    Point p22 = new Point(paddingRight + 1 * widthScaleFactor, paddingTop + 5, ConsoleColor.DarkBlue);
    LineSegment top = p11 + p12;
    LineSegment bottom = p21 + p22;
    Shape all = top + bottom;

    return all;
}

Shape CreateLightBlueL3x3()
{
    int startX = 2;

    Point p11 = new Point(paddingRight + startX * widthScaleFactor, paddingTop + 0, ConsoleColor.Blue);
    Point p12 = new Point(paddingRight + startX * widthScaleFactor, paddingTop + 3, ConsoleColor.Blue);
    Point p21 = new Point(paddingRight + startX * widthScaleFactor, paddingTop + 0, ConsoleColor.Blue);
    Point p22 = new Point(paddingRight + (startX + 3) * widthScaleFactor, paddingTop + 0, ConsoleColor.Blue);

    var points = new List<Point>();
    points.Add(p11);
    points.Add(p12);
    points.Add(p21);
    points.Add(p22);

    Shape all = new Shape(points, ConsoleColor.Blue);
    
    return all;
}


Shape CreateYellow3x3()
{
    int positionX = 5 * sceneScaleFactor;
    int positionY = 0; // * sceneScaleFactor;
    ConsoleColor color = ConsoleColor.Yellow;
    int height = 3* sceneScaleFactor;
    int width = 3* sceneScaleFactor;

    Point p11 = new Point(0, 0, color);
    Point p12 = new Point(0, height / 2, color);

    Point p21 = new Point(width, height / 2, color);
    Point p212 = new Point(0, height / 2, color);
    Point p22 = new Point(0, height, color);

    var points = new List<Point>();
    points.Add(p11);
    points.Add(p12);
    points.Add(p21);
    points.Add(p212);
    points.Add(p22);

    Shape all = new Shape(points, color);

    all.SetX(positionX + paddingRight);
    all.SetY(positionY + paddingTop);

    return all;
}


Shape CreatePurple()
{
    int positionX = 8 * sceneScaleFactor;
    int positionY = 0; // * sceneScaleFactor;
    ConsoleColor color = ConsoleColor.DarkMagenta;
    int height = 2 * sceneScaleFactor;
    int width = 3 * sceneScaleFactor;

    Point p11 = new Point(0, 0, color);
    Point p12 = new Point(width, 0, color);
    Point p12a = new Point(width, height, color);
    Point p12b = new Point(width, 0, color);
    Point p12c = new Point(0, 0, color);
    Point p22 = new Point(0, height, color);

    var points = new List<Point>();
    points.Add(p11);
    points.Add(p12);
    points.Add(p12a);
    points.Add(p12b);
    points.Add(p12c);
    points.Add(p22);

    Shape all = new Shape(points, color);

    all.SetX(positionX + paddingRight);
    all.SetY(positionY + paddingTop);

    return all;
}

// TODO: continue from here
Shape Convert()
{
    geometry.Coordinate[] coords = new geometry.Coordinate[5];
    coords[0] = new geometry.Coordinate(2, 2);
    coords[1] = new geometry.Coordinate(4, 2);
    coords[2] = new geometry.Coordinate(4, 3);
    coords[3] = new geometry.Coordinate(2, 3);
    coords[4] = new geometry.Coordinate(2, 2);
    geometry.Geometry polygon = new geometry.Polygon(new geometry.LinearRing(coords));

    //var polygon = new geometry
    //    .GeometryFactory()
    //    .CreatePolygon(new geometry.Coordinate[] {
    //        new geometry.Coordinate(0, 0),
    //        new geometry.Coordinate(0, 2),
    //        new geometry.Coordinate(2, 0),
    //        new geometry.Coordinate(2, 2),
    //        new geometry.Coordinate(0, 0)
    //    });

    var isValid = polygon.IsValid;
    var area = polygon.Area;

    var cor = polygon.Centroid;
    var transformer = new geometry.Utilities.GeometryTransformer();
    transformer.Transform(polygon);

    var transform = new geometry.Utilities.AffineTransformation();
    var result = transform.Rotate(algorithm.AngleUtility.ToRadians(90), cor.X, cor.Y);
    polygon.Apply(result);

    //transform.rotate(Math.toRadians(180) /* ?, ? */);


    // TODO: check how to rotate

    int positionX = 10 * sceneScaleFactor;
    int positionY = 10; // * sceneScaleFactor;
    ConsoleColor color = ConsoleColor.Red;

    var points = polygon
        .Coordinates
        .Select(p => new Point(p.X, p.Y, color))
        .ToList();

    Shape all = new Shape(points, color);

    all.SetX(positionX + paddingRight);
    all.SetY(positionY + paddingTop);

    return all;

}