using Demo.Utilities;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.GameParts.Board;

namespace Demo.ViewModel
{
    public class DisplayBlockHelper
    {
        public UIElement MapBoardToCanvas(BoardShapeBase board, int canvasHeight)
        {
            // clear canvas
            var canvas = new Canvas();
            canvas.Height = canvasHeight;
            canvas.RenderTransform = new ScaleTransform(30, 30);

            // show board
            var boardComponent = new Polygon();
            boardComponent.Points = new PointCollection();
            boardComponent.Fill = Brushes.LightGray;
            boardComponent.Stroke = Brushes.DarkGray;
            boardComponent.StrokeThickness = 0.08d;

            board
                .Polygon
                .Coordinates
                .ToList()
                .ForEach(p => boardComponent
                    .Points
                    .Add(new Point(p.X, p.Y)));

            canvas.Children.Add(boardComponent);

            return canvas;
        }

        public UIElement MapBlockDefinitionToCanvasWithBoard(BoardShapeBase board, BlockBase block, int canvasHeight)
        {
            // clear canvas
            var canvas = new Canvas();
            canvas.Height = canvasHeight;
            canvas.RenderTransform = new ScaleTransform(30, 30);

            // show board
            var boardComponent = new Polygon();
            boardComponent.Points = new PointCollection();
            boardComponent.Fill = Brushes.LightGray;
            boardComponent.Stroke = Brushes.DarkGray;
            boardComponent.StrokeThickness = 0.08d;

            board
                .Polygon
                .Coordinates
                .ToList()
                .ForEach(p => boardComponent
                    .Points
                    .Add(new Point(p.X, p.Y)));

            canvas.Children.Add(boardComponent);

            // show block definition
            var blockDefinition = new Polygon();
            blockDefinition.Points = new PointCollection(
                block
                    .Polygon
                    .Coordinates
                    .Select(pp => new Point(pp.X, pp.Y))
                    .ToList());

            blockDefinition.Fill = AlgorithmDisplayHelper.ConvertColor(block.Color);
            blockDefinition.Stroke = Brushes.Silver;
            blockDefinition.StrokeThickness = 0.05d;

            canvas.Children.Add(blockDefinition);

            return canvas;
        }

        public UIElement MapAllowedLocationToCanvasWithBoard(NetTopologySuite.Geometries.Geometry location, BoardShapeBase board, BlockBase block, int canvasHeight)
        {
            var cloned = block.Clone(true);
            cloned.Apply(location);

            return MapBlockDefinitionToCanvasWithBoard(board, cloned, canvasHeight);
        }

        public UIElement? MapBlockDefinitionToMeshSideB(BoardShapeBase board, BlockBase blockOriginal, int canvasHeight)
        {
            var block = blockOriginal.Clone();

            // clear canvas
            var canvas = new Canvas();
            canvas.Height = canvasHeight;
            canvas.RenderTransform = new ScaleTransform(30, 30);

            // show board
            var boardComponent = new Polygon();
            boardComponent.Points = new PointCollection();
            boardComponent.Fill = Brushes.LightGray;
            boardComponent.Stroke = Brushes.DarkGray;
            boardComponent.StrokeThickness = 0.08d;

            board
                .Polygon
                .Coordinates
                .ToList()
                .ForEach(p => boardComponent
                    .Points
                    .Add(new Point(p.X, p.Y)));

            canvas.Children.Add(boardComponent);

            // flip block
            block.Reflection();

            // show block definition
            var blockDefinition = new Polygon();
            blockDefinition.Points = new PointCollection(
                block
                    .Polygon
                    .Coordinates
                    .Select(pp => new Point(pp.X, pp.Y))
                    .ToList());

            blockDefinition.Fill = AlgorithmDisplayHelper.ConvertColor(block.Color);
            blockDefinition.Stroke = Brushes.Silver;
            blockDefinition.StrokeThickness = 0.05d;

            canvas.Children.Add(blockDefinition);

            return canvas;
        }

        public UIElement? MapBlockDefinitionToMeshSideA(BoardShapeBase board, BlockBase block, int canvasHeight)
        {
            // clear canvas
            var canvas = new Canvas();
            canvas.Height = canvasHeight;
            canvas.RenderTransform = new ScaleTransform(30, 30);

            // show board
            var boardComponent = new Polygon();
            boardComponent.Points = new PointCollection();
            boardComponent.Fill = Brushes.LightGray;
            boardComponent.Stroke = Brushes.DarkGray;
            boardComponent.StrokeThickness = 0.08d;

            board
                .Polygon
                .Coordinates
                .ToList()
                .ForEach(p => boardComponent
                    .Points
                    .Add(new Point(p.X, p.Y)));

            canvas.Children.Add(boardComponent);

            // show block definition
            var blockDefinition = new Polygon();
            blockDefinition.Points = new PointCollection(
                block
                    .Polygon
                    .Coordinates
                    .Select(pp => new Point(pp.X, pp.Y))
                    .ToList());

            blockDefinition.Fill = AlgorithmDisplayHelper.ConvertColor(block.Color);
            blockDefinition.Stroke = Brushes.Silver;
            blockDefinition.StrokeThickness = 0.05d;

            canvas.Children.Add(blockDefinition);

            return canvas;
        }
    }
}
