// See https://aka.ms/new-console-template for more information
using ConsoleGraphics.Graphics2D.Bases;

Scene2D Scene = new Scene2D(GraphicsType.ColoredPoints);

int fieldWidthAndHeight = 10;

int paddingTop = 5;
int paddingRight = 10;

Point p11 = new Point(paddingRight + 0, paddingTop + 0, ConsoleColor.Blue);
Point p12 = new Point(paddingRight + 0, paddingTop + 1, ConsoleColor.Blue);
Point p21 = new Point(paddingRight + 1, paddingTop + 0, ConsoleColor.Blue);
Point p22 = new Point(paddingRight + 1, paddingTop + 1, ConsoleColor.Blue);
LineSegment top = p11 + p12;
LineSegment bottom = p21 + p22;
Shape all = top + bottom;
Scene.Add(all);

Scene2D Scene2 = new Scene2D(GraphicsType.ColoredSymbols);

int width = 20;
int height = 10;
int boardBorderWidth = 2;

Point AAA = new Point(paddingRight + 0 - boardBorderWidth, paddingTop + 0 - boardBorderWidth, ConsoleColor.White, "AAA", '-');
Point BBB = new Point(paddingRight + width + boardBorderWidth, paddingTop + 0 - boardBorderWidth, ConsoleColor.White, "BBB", '-');
Point C = new Point(paddingRight + width + boardBorderWidth, paddingTop + height + boardBorderWidth, ConsoleColor.White, "CCC", '-');
Point D = new Point(paddingRight + 0 - boardBorderWidth, paddingTop + height + boardBorderWidth, ConsoleColor.White, "DDD", '-');
LineSegment AAABBBB = AAA + BBB;
LineSegment CD = C + D;
Shape ABCD = AAABBBB + CD;
Scene2.Add(ABCD);

Console.ReadKey();