using System;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace AvaloniaSample.ViewModels
{
    public class TilePanelViewModel : ViewModelBase
    {
        private double _itemHeight;
        private double _itemWidth;
        private int _maximumRowsOrColumns;
        private double _panelHeight;
        private double _panelWidth;
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
        
        public double PanelHeight
        {
            get => _panelHeight;
            set => this.RaiseAndSetIfChanged(ref _panelHeight, value);
        }
        
        public double PanelWidth
        {
            get => _panelWidth;
            set => this.RaiseAndSetIfChanged(ref _panelWidth, value);
        }

        public ObservableCollection<TileViewModel> Tiles
        {
            get => _tiles;
            set => this.RaiseAndSetIfChanged(ref _tiles, value);
        }

        public TilePanelViewModel()
        {
            this.WhenAnyValue(x => x.PanelHeight)
                .Subscribe(x =>
                {
                    // TODO: Update ItemHeight
                });

            this.WhenAnyValue(x => x.PanelWidth)
                .Subscribe(x =>
                {
                    // TODO: Update ItemWidth
                });
        }
    }
}