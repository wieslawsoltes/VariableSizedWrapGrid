using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using AvaloniaSample.ViewModels;

namespace AvaloniaSample
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();

            var tilePanel = new TilePanelViewModel()
            {
                ItemHeight = 100,
                ItemWidth = 250,
                MaximumRowsOrColumns = 3,
                Tiles = new ObservableCollection<TileViewModel>()
                {
                    new TileViewModel()
                    {
                        ColumnSpan = 1,
                        RowSpan = 1,
                        ColumnSpans = new List<int>() { 1, 1, 1 },
                        RowSpans = new List<int>() { 1, 1, 1 },
                        Background = SolidColorBrush.Parse("Red")
                    },
                    new TileViewModel()
                    {
                        ColumnSpan = 1,
                        RowSpan = 1,
                        ColumnSpans = new List<int>() { 1, 1, 1 },
                        RowSpans = new List<int>() { 1, 1, 1 },
                        Background = SolidColorBrush.Parse("Green")
                    },
                    new TileViewModel()
                    {
                        ColumnSpan = 1,
                        RowSpan = 1,
                        ColumnSpans = new List<int>() { 1, 1, 1 },
                        RowSpans = new List<int>() { 1, 1, 1 },
                        Background = SolidColorBrush.Parse("Blue")
                    },
                    new TileViewModel()
                    {
                        ColumnSpan = 1,
                        RowSpan = 2,
                        ColumnSpans = new List<int>() { 1, 1, 1 },
                        RowSpans = new List<int>() { 1, 2, 2 },
                        Background = SolidColorBrush.Parse("Yellow")
                    },
                    new TileViewModel()
                    {
                        ColumnSpan = 2,
                        RowSpan = 2,
                        ColumnSpans = new List<int>() { 2, 2, 2 },
                        RowSpans = new List<int>() { 2, 2, 2 },
                        Background = SolidColorBrush.Parse("Black")
                    },
                }
            };

            DataContext = tilePanel;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}