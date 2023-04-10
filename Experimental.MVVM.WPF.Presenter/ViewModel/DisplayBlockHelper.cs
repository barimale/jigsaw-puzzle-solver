using Algorithm.Tangram.Common.Extensions;
using Demo.Utilities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
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

        public UIElement MapBlockDefinitionToBinaryText(
            BoardShapeBase board,
            BlockBase block,
            NetTopologySuite.Geometries.Geometry locationToBeapplied,
            int containerHeight)
        {
            var stackPanel = new StackPanel();

            var boardWidth = board.Width;
            var boardHeight = board.Height;

            var clonedBlockDefinition = block.Clone();
            clonedBlockDefinition.Apply(locationToBeapplied);
            var binaryText = clonedBlockDefinition.ToBinary(board.BoardFieldsDefinition);
            var binaryTextBuilder = new StringBuilder();
                
            for(int i = 0;i < boardHeight; i++)
            {
                var oneRow = binaryText
                    .Skip(i * boardWidth)
                    .Take(boardWidth)
                    .Select(p => p.ToString())
                    .ToList();

                var oneRowAsString = string.Join(' ', oneRow);
                binaryTextBuilder.Append(oneRowAsString);
                if(i < boardHeight -1)
                {
                    binaryTextBuilder.AppendLine();
                }
            }

            var textComponent = new TextBlock();
            textComponent.Height = containerHeight;
            textComponent.Text = binaryTextBuilder.ToString();
            stackPanel.Children.Add(textComponent);

            return stackPanel;
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
