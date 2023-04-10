using Algorithm.Tangram.Common.Extensions;
using Solver.Tangram.Game.Logic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Tangram.GameParts.Logic.GameParts.Block;
using Tangram.GameParts.Logic.Utilities;

namespace Demo.ViewModel.SolverTabs
{
    // game parts: board and blocks
    public class ElementsDetails : TabBase
    {
        private Game _gameInstance;

        public ElementsDetails(ref Game gameInstance)
        {
            _gameInstance = gameInstance;
            MyCanvasContent = MapBlocksToTabItems();
        }

        public UIElement MyCanvasContent { get; set; }

        private UIElement MapBlocksToTabItems()
        {
            var board = _gameInstance.GameSet.Board;

            var tabItems = _gameInstance
                .GameSet
                .Blocks
                .WithIndex()
                .Select(block =>
                    {
                        var colorName = block.item.Color.IsNamedColor ? block.item.Color.Name : $"ARGB: {block.item.Color.ToArgb()}";
                        return new TabItem()
                        {
                            Content = MapBlockToDetails(block.item, board.SkippedMarkup),
                            Header = $"Block #{block.index + 1}({colorName})",
                        };
                    })
                .ToList();

            var container = new TabControl()
            {
                Background = new SolidColorBrush()
                {
                    Color = Colors.WhiteSmoke,
                },
                ItemsSource = tabItems
            };

            return container;
        }

        private UIElement MapBlockToDetails(BlockBase block, string skipMarkup)
        {
            var grid = GetGridDefinition();
            grid.Margin = new Thickness(8, 8, 8, 8);

            // block definition label
            var blockDefinitionLabel = new TextBlock();
            blockDefinitionLabel.Inlines.Add(new Run("Block definition:") { FontWeight = FontWeights.SemiBold });
            blockDefinitionLabel.Margin = new Thickness(0, 0, 0, 8);

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
            Grid.SetColumnSpan(blockDefinition, 2); // TODO: double check it

            // allowed locations amount or not supported
            grid.Children.Add(blockDefinition);

            var allowedLocationsLabel = new TextBlock();
            var allowedLocationValue = block.IsAllowedLocationsEnabled ? block.AllowedLocations.Length.ToString() : "not used";
            allowedLocationsLabel.Inlines.Add(new Run("Allowed locations count: ") { FontWeight = FontWeights.SemiBold });
            allowedLocationsLabel.Inlines.Add(new Run($"{allowedLocationValue}") { FontWeight = FontWeights.Regular });

            allowedLocationsLabel.Margin = new Thickness(0, 0, 0, 8);

            Grid.SetRow(allowedLocationsLabel, 2);
            Grid.SetColumn(allowedLocationsLabel, 0);
            grid.Children.Add(allowedLocationsLabel);

            var hasMeshLabel = new TextBlock();
            var hasMeshValue = block.IsExtraRistricted;
            hasMeshLabel.Inlines.Add(new Run("Has mesh: ") { FontWeight = FontWeights.SemiBold });
            hasMeshLabel.Inlines.Add(new Run($"{hasMeshValue}") { FontWeight = FontWeights.Regular });
            hasMeshLabel.Margin = new Thickness(0, 0, 0, 8);

            Grid.SetRow(hasMeshLabel, 3);
            Grid.SetColumn(hasMeshLabel, 0);
            grid.Children.Add(hasMeshLabel);

            if (hasMeshValue)
            {
                var meshSideALabel = new TextBlock();
                meshSideALabel.Inlines.Add(new Run("Mesh: ") { FontWeight = FontWeights.SemiBold });
                meshSideALabel.Inlines.Add(new Run("Side A") { FontWeight = FontWeights.Regular });
                meshSideALabel.Margin = new Thickness(0, 0, 0, 8);

                Grid.SetRow(meshSideALabel, 3);
                Grid.SetColumn(meshSideALabel, 1);
                grid.Children.Add(meshSideALabel);

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
                meshAContent.Text = ExtraRestrictionMarkupsHelper.MeshSideToString<string>(
                    block.FieldRestrictionMarkups[0],
                    skipMarkup);

                Grid.SetRow(meshAContent, 4);
                Grid.SetColumn(meshAContent, 1);
                Grid.SetColumnSpan(meshAContent, 1);

                grid.Children.Add(meshAContent);

                var meshSideBLabel = new TextBlock();
                meshSideBLabel.Inlines.Add(new Run("Mesh: ") { FontWeight = FontWeights.SemiBold });
                meshSideBLabel.Inlines.Add(new Run("Side B") { FontWeight = FontWeights.Regular });
                meshSideBLabel.Margin = new Thickness(0, 0, 0, 8);

                Grid.SetRow(meshSideBLabel, 5);
                Grid.SetColumn(meshSideBLabel, 1);
                grid.Children.Add(meshSideBLabel);

                // mesh side B
                UIElement meshSideB = new DisplayBlockHelper()
                   .MapBlockDefinitionToMeshSideB(
                       _gameInstance.GameSet.Board,
                       block,
                       160);

                Grid.SetRow(meshSideB, 6);
                Grid.SetColumn(meshSideB, 0);
                Grid.SetColumnSpan(meshSideB, 1);

                grid.Children.Add(meshSideB);

                var meshBContent = new TextBlock();
                meshBContent.Height = 160;
                meshBContent.Text = ExtraRestrictionMarkupsHelper.MeshSideToString<string>(
                    block.FieldRestrictionMarkups[1],
                    skipMarkup);

                Grid.SetRow(meshBContent, 6);
                Grid.SetColumn(meshBContent, 1);
                Grid.SetColumnSpan(meshBContent, 1);

                grid.Children.Add(meshBContent);
            }

            if (block.IsAllowedLocationsEnabled)
            {
                var allowedLocationsListLabel = new TextBlock();
                allowedLocationsListLabel.Inlines.Add(new Run("Allowed locations:") { FontWeight = FontWeights.SemiBold });
                allowedLocationsListLabel.Margin = new Thickness(0, 0, 0, 8);

                Grid.SetRow(allowedLocationsListLabel, 7);
                Grid.SetColumn(allowedLocationsListLabel, 0);
                grid.Children.Add(allowedLocationsListLabel);

                Grid innerGrid = CreateInnerGrid();
                block.AllowedLocations.ToList().ForEach(_ =>
                {
                    var rowDefinitionLabel = new RowDefinition()
                    {
                        Height = GridLength.Auto
                    };

                    var rowDefinition = new RowDefinition()
                    {
                        Height = new GridLength()
                    };

                    innerGrid.RowDefinitions.Add(rowDefinitionLabel);
                    innerGrid.RowDefinitions.Add(rowDefinition);
                });

                block.AllowedLocations.WithIndex().ToList().ForEach(location =>
                {
                    // label
                    var locationDefinitionLabel = new TextBlock();
                    locationDefinitionLabel.Inlines.Add(new Run("#") { FontWeight = FontWeights.SemiBold });
                    locationDefinitionLabel.Inlines.Add(new Run($"{location.index + 1}") { FontWeight = FontWeights.SemiBold });
                    locationDefinitionLabel.Margin = new Thickness(0, 0, 0, 8);

                    Grid.SetRow(locationDefinitionLabel, 2 * location.index);
                    Grid.SetColumn(locationDefinitionLabel, 0);
                    innerGrid.Children.Add(locationDefinitionLabel);

                    // binary label
                    var binaryLocationDefinitionLabel = new TextBlock();
                    binaryLocationDefinitionLabel.Inlines.Add(new Run($"Binary representative of the location {location.index + 1}:") { FontWeight = FontWeights.SemiBold });
                    binaryLocationDefinitionLabel.Margin = new Thickness(0, 0, 0, 8);

                    Grid.SetRow(binaryLocationDefinitionLabel, 2 * location.index);
                    Grid.SetColumn(binaryLocationDefinitionLabel, 1);
                    innerGrid.Children.Add(binaryLocationDefinitionLabel);

                    // create canvas for every location and add to row
                    UIElement locationDefinition = new DisplayBlockHelper()
                       .MapAllowedLocationToCanvasWithBoard(
                           location.item,
                           _gameInstance.GameSet.Board,
                           block,
                           160);

                    Grid.SetRow(locationDefinition, 2 * location.index + 1);
                    Grid.SetColumn(locationDefinition, 0);
                    innerGrid.Children.Add(locationDefinition);

                    // allowed location binary definition
                    UIElement blockDefinitionBinary = new DisplayBlockHelper()
                        .MapBlockDefinitionToBinaryText(
                            _gameInstance.GameSet.Board,
                            block,
                            location.item,
                            160);

                    Grid.SetRow(blockDefinitionBinary, 2 * location.index + 1);
                    Grid.SetColumn(blockDefinitionBinary, 1);
                    innerGrid.Children.Add(blockDefinitionBinary);
                });

                Grid.SetRow(innerGrid, 8);
                Grid.SetColumn(innerGrid, 0);
                Grid.SetColumnSpan(innerGrid, 2);
                grid.Children.Add(innerGrid);
            }

            return grid;
        }

        private static Grid CreateInnerGrid()
        {
            var innerGrid = new Grid();
            innerGrid.HorizontalAlignment = HorizontalAlignment.Left;
            innerGrid.VerticalAlignment = VerticalAlignment.Top;
            innerGrid.ShowGridLines = false;

            ColumnDefinition columnDef1 = new ColumnDefinition()
            {
                Width = new GridLength(360)
            };
            ColumnDefinition columnDef2 = new ColumnDefinition()
            {
                Width = new GridLength(360)
            };
            innerGrid.ColumnDefinitions.Add(columnDef1);
            innerGrid.ColumnDefinitions.Add(columnDef2);
            return innerGrid;
        }

        private Grid GetGridDefinition()
        {
            // Create the Grid
            Grid myGrid = new Grid();

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
            // 1
            RowDefinition rowDef1 = new RowDefinition()
            {
                Height = GridLength.Auto
            };
            // 2
            RowDefinition rowDef1b = new RowDefinition()
            {
                Height = new GridLength(160)
            };
            // 3
            RowDefinition rowDef2 = new RowDefinition()
            {
                Height = GridLength.Auto
            };
            // 4
            RowDefinition rowDef3 = new RowDefinition()
            {
                Height = GridLength.Auto
            };
            // 5
            RowDefinition rowDef4 = new RowDefinition()
            {
                Height = new GridLength(160)
            };
            // 6
            RowDefinition rowDef5 = new RowDefinition()
            {
                Height = GridLength.Auto
            };
            //7
            RowDefinition rowDef5b = new RowDefinition()
            {
                Height = new GridLength(160)
            };
            //8
            RowDefinition rowDef6a = new RowDefinition()
            {
                Height = GridLength.Auto
            };
            //9
            RowDefinition rowDef6b = new RowDefinition()
            {
                Height = new GridLength(1, GridUnitType.Star)
            };

            myGrid.RowDefinitions.Add(rowDef1);
            myGrid.RowDefinitions.Add(rowDef1b);
            myGrid.RowDefinitions.Add(rowDef2);
            myGrid.RowDefinitions.Add(rowDef3);
            myGrid.RowDefinitions.Add(rowDef4);
            myGrid.RowDefinitions.Add(rowDef5);
            myGrid.RowDefinitions.Add(rowDef5b);
            myGrid.RowDefinitions.Add(rowDef6a);
            myGrid.RowDefinitions.Add(rowDef6b);

            return myGrid;
        }
    }
}
