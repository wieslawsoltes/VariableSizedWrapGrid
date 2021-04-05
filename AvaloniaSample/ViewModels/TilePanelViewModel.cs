using System.Collections.ObjectModel;
using ReactiveUI;

namespace AvaloniaSample.ViewModels
{
    public class TilePanelViewModel : ViewModelBase
    {
        private double _itemHeight;
        private double _itemWidth;
        private int _maximumRowsOrColumns;
        private ObservableCollection<TileViewModel> _tiles;

        public double ItemHeight
        {
            get => _itemHeight;
            set => this.RaiseAndSetIfChanged(ref _itemHeight, value);
        }
        
        public double ItemWidth
        {
            get => _itemWidth;
            set => this.RaiseAndSetIfChanged(ref _itemWidth, value);
        }
        
        public int MaximumRowsOrColumns
        {
            get => _maximumRowsOrColumns;
            set => this.RaiseAndSetIfChanged(ref _maximumRowsOrColumns, value);
        }
        
        public ObservableCollection<TileViewModel> Tiles
        {
            get => _tiles;
            set => this.RaiseAndSetIfChanged(ref _tiles, value);
        }
    }
}