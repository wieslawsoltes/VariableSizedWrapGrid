using System.Collections.Generic;
using Avalonia.Media;
using ReactiveUI;

namespace AvaloniaSample.ViewModels
{
    public class TileViewModel : ViewModelBase
    {
        private int _columnSpan;
        private int _rowSpan;
        private List<int> _columnSpans;
        private List<int> _rowSpans;
        private IBrush _background;

        public int ColumnSpan
        {
            get => _columnSpan;
            set => this.RaiseAndSetIfChanged(ref _columnSpan, value);
        }

        public int RowSpan
        {
            get => _rowSpan;
            set => this.RaiseAndSetIfChanged(ref _rowSpan, value);
        }

        public List<int> ColumnSpans
        {
            get => _columnSpans;
            set => this.RaiseAndSetIfChanged(ref _columnSpans, value);
        }

        public List<int> RowSpans
        {
            get => _rowSpans;
            set => this.RaiseAndSetIfChanged(ref _rowSpans, value);
        }

        public IBrush Background
        {
            get => _background;
            set => this.RaiseAndSetIfChanged(ref _background, value);
        }
    }
}