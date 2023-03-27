using Algorithm.Tangram.Common.Extensions;
using Solver.Tangram.Game.Logic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
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
                        var colorName = block.item.Color.IsNamedColor ? block.item.Color.Name : $"ARGB: {block.item.Color.ToArgb()}";
                        return new TabItem()
                        {
                            Content = MapBlockToDetails(block.item),
                            Header = $"Block #{block.index}({colorName})",
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
            blockDefinitionLabel.Margin = new Thickness(0,0,0,8);

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
            allowedLocationsLabel.Margin = new Thickness(0, 0, 0, 8);

            Grid.SetRow(allowedLocationsLabel, 2);
            Grid.SetColumn(allowedLocationsLabel, 0);
            grid.Children.Add(allowedLocationsLabel);

            var hasMeshLabel = new TextBlock();
            var hasMeshValue = block.IsExtraRistricted;
            hasMeshLabel.Text = $"Has mesh: {hasMeshValue}";
            hasMeshLabel.Margin = new Thickness(0, 0, 0, 8);

            Grid.SetRow(hasMeshLabel, 3);
            Grid.SetColumn(hasMeshLabel, 0);
            grid.Children.Add(hasMeshLabel);

            if (hasMeshValue)
            {
                // mesh side A
                UIElement meshSideA = new DisplayBlockHelper()
                   .MapBlockDefinitionToMeshSideA(
                       _gameInstance.GameSet.Board,
                       block,
                       160);

                Grid.SetRow(meshSideA, 4);
                Grid.SetColumn(meshSideA, 0);
                Grid.SetColumnSpan(meshSideA, 1);

                grid.Children.Add(meshSideA);

                var meshAContent = new TextBlock();
                meshAContent.Height = 160;
                meshAContent.Text = MeshSideToString(block, 0); // TODO: visualize it

                Grid.SetRow(meshAContent, 4);
                Grid.SetColumn(meshAContent, 1);
                Grid.SetColumnSpan(meshAContent, 1);

                grid.Children.Add(meshAContent);

                // mesh side B
                UIElement meshSideB = new DisplayBlockHelper()
                   .MapBlockDefinitionToMeshSideB(
                       _gameInstance.GameSet.Board,
                       block,
                       160);

                Grid.SetRow(meshSideB, 5);
                Grid.SetColumn(meshSideB, 0);
                Grid.SetColumnSpan(meshSideB, 1);

                grid.Children.Add(meshSideB);

                var meshBContent = new TextBlock();
                meshBContent.Height = 160;
                meshBContent.Text = MeshSideToString(block, 1); // TODO: visualize it

                Grid.SetRow(meshBContent, 5);
                Grid.SetColumn(meshBContent, 1);
                Grid.SetColumnSpan(meshBContent, 1);

                grid.Children.Add(meshBContent);
            }

            return grid;
        }

        // provide it from polishgamebaseblock later on
        private static string MeshSideToString(BlockBase block, int sideNumber, string markupToClean = "&&&")
        {
            var mesh = block.FieldRestrictionMarkups[sideNumber];
            var columns = mesh.ColumnsCount();
            var rows = mesh.RowsCount();

            // TODO: maybe use the fieldtransformer here or create new one 

            var builder = new StringBuilder();
            for(var rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                var currentRow = mesh.GetRow(rowIndex);
                var currentRowAsStrings = currentRow.Select(p => (string)p).ToArray();
                var currentRowAsString = string.Join(' ', currentRowAsStrings).Replace(markupToClean, "  ");
                // TODO: replace not field tag by empty
                builder.Append(currentRowAsString);
                if (rowIndex < rows -1) builder.AppendLine();
            }

            return builder.ToString();
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
            ColumnDefinition colDef1 = new ColumnDefinition()
            {
                Width = new GridLength(360)
            };
            ColumnDefinition colDef2 = new ColumnDefinition()
            {
                Width = new GridLength(360)
            };
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
            };// MEsh Side A
            RowDefinition rowDef4 = new RowDefinition()
            {
                Height = new GridLength(160)
            }; ;// * Mesh side B
            RowDefinition rowDef5 = new RowDefinition()
            {
                Height = new GridLength(160)
            };

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
