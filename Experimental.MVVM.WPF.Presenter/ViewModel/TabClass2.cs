using Algorithm.Tangram.Common.Extensions;
using Solver.Tangram.Game.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tangram.GameParts.Logic.GameParts.Block;

namespace Demo.ViewModel
{
    // game parts: board and blocks
   public class TabClass2 : TabBase
    {
        private Game _gameInstance;

        public TabClass2(ref Game gameInstance)
        {
            this._gameInstance = gameInstance;
            MyCanvasContent = MapBlocksToTabItems();
        }

        public string MyStringContent { get; set; }
        public int[] MyNumberCollection { get; set; }
        public int MySelectedNumber { get; set; }
        public UIElement MyCanvasContent { get; set; }

        private UIElement MapBlocksToTabItems()
        {
            var board = this._gameInstance.GameSet.Board;

            var tabItems =  this._gameInstance
                .GameSet
                .Blocks
                .WithIndex()
                .Select(block =>
                    {
                        return new TabItem()
                        {
                            Content = MapBlockToDetails(block.item),
                            Header = $"Block #{block.index}",
                        };
                    })
                .ToList();

            var container = new TabControl()
            {
                Background = new SolidColorBrush()
                {
                    Color = Colors.DarkGreen,
                },
                ItemsSource = tabItems
            };

            return container;
        }

        private UIElement MapBlockToDetails(BlockBase block)
        {
            var grid = GetGridDefinition();

            // block definition label
            var blockDefinitionLabel = new TextBlock();
            blockDefinitionLabel.Text = "Block definition:";

            Grid.SetRow(blockDefinitionLabel, 0);
            Grid.SetColumn(blockDefinitionLabel, 0);
            grid.Children.Add(blockDefinitionLabel);

            // block definition image
            UIElement blockDefinition = new DisplayBlockHelper()
                .MapBlockDefinitionToCanvasWithBoard(
                    _gameInstance.GameSet.Board,
                    block,
                    160);

            Grid.SetRow(blockDefinition, 1);
            Grid.SetColumn(blockDefinition, 0);
            Grid.SetColumnSpan(blockDefinition, 2);

            // allowed locations amount or not supported
            grid.Children.Add(blockDefinition);

            var allowedLocationsLabel = new TextBlock();
            var allowedLocationValue = block.IsAllowedLocationsEnabled ? block.AllowedLocations.Length.ToString() : "not used";
            allowedLocationsLabel.Text = $"Allowed locations: {allowedLocationValue}";

            Grid.SetRow(allowedLocationsLabel, 2);
            Grid.SetColumn(allowedLocationsLabel, 0);
            grid.Children.Add(allowedLocationsLabel);

            return grid;
        }

        private Grid GetGridDefinition()
        {
            // Create the Grid
            Grid myGrid = new Grid();

            //myGrid.Width = 250;
            //myGrid.Height = 100;
            myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            myGrid.VerticalAlignment = VerticalAlignment.Top;
            myGrid.ShowGridLines = false;

            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            myGrid.ColumnDefinitions.Add(colDef1);
            myGrid.ColumnDefinitions.Add(colDef2);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition()
            {
                Height = GridLength.Auto
            };// Block definition label
            RowDefinition rowDef1b = new RowDefinition()
            {
                Height = new GridLength(160)
            }; // Allowed locations, later as checkbox on selected display
            RowDefinition rowDef2 = new RowDefinition()
            {
                Height = GridLength.Auto
            };// Allowed locations, later as checkbox on selected display
            RowDefinition rowDef3 = new RowDefinition()
            {
                Height = GridLength.Auto
            };// HasMesh 
            RowDefinition rowDef4 = new RowDefinition()
            {
                Height = GridLength.Auto
            };// * Mesh side A
            RowDefinition rowDef5 = new RowDefinition()
                            {
                Height = GridLength.Auto
            };// * Mesh side B

            myGrid.RowDefinitions.Add(rowDef1);
            myGrid.RowDefinitions.Add(rowDef1b);
            myGrid.RowDefinitions.Add(rowDef2);
            myGrid.RowDefinitions.Add(rowDef3);
            myGrid.RowDefinitions.Add(rowDef4);
            myGrid.RowDefinitions.Add(rowDef5);

            return myGrid;
        }
    }
}
