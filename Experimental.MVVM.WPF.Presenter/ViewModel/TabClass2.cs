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
        public object MyCanvasContent { get; set; }

        private object MapBlocksToTabItems()
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

            UIElement blockDefinition = new DisplayBlockHelper()
                .MapBlockDefinitionToCanvasWithBoard(
                    _gameInstance.GameSet.Board,
                    block);

            Grid.SetRow(blockDefinition, 0);
            Grid.SetColumn(blockDefinition, 0);
            //Grid.SetColumnSpan(blockDefinition, );

            grid.Children.Add(blockDefinition);

            return grid;
        }

        private Grid GetGridDefinition()
        {
            // Create the Grid
            Grid myGrid = new Grid();

            myGrid.Width = 250;
            myGrid.Height = 100;
            myGrid.HorizontalAlignment = HorizontalAlignment.Left;
            myGrid.VerticalAlignment = VerticalAlignment.Top;
            myGrid.ShowGridLines = true;

            // Define the Columns
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();
            myGrid.ColumnDefinitions.Add(colDef1);
            myGrid.ColumnDefinitions.Add(colDef2);

            // Define the Rows
            RowDefinition rowDef1 = new RowDefinition(); // Block definition
            RowDefinition rowDef2 = new RowDefinition(); // Allowed locations, later as checkbox on selected display
            RowDefinition rowDef3 = new RowDefinition(); // HasMesh 
            RowDefinition rowDef4 = new RowDefinition(); // * Mesh side A
            RowDefinition rowDef5 = new RowDefinition(); // * Mesh side B

            myGrid.RowDefinitions.Add(rowDef1);
            myGrid.RowDefinitions.Add(rowDef2);
            myGrid.RowDefinitions.Add(rowDef3);
            myGrid.RowDefinitions.Add(rowDef4);
            myGrid.RowDefinitions.Add(rowDef5);

            return myGrid;
        }
    }
}
