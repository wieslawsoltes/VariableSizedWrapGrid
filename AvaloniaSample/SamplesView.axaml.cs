using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace AvaloniaSample
{
    public partial class SamplesView : UserControl
    {
        public SamplesView()
        {
            InitializeComponent();

            InitColors();
        }

        private void InitColors()
        {
            var colors = typeof(Colors)
                .GetProperties()
                .Select((c, i) => new
                {
                    Color = (Color) c.GetValue(null)!,
                    Name = c.Name,
                    Index = i,
                    ColSpan = ColSpan(i),
                    RowSpan = RowSpan(i)
                });

            DataContext = colors;

            int RowSpan(int i)
            {
                if (i == 0)
                    return 2;
                if (i == 2)
                    return 3;
                if (i == 7)
                    return 2;
                if (i == 14)
                    return 2;
                return 1;
            }

            int ColSpan(int i)
            {
                if (i == 0)
                    return 2;
                if (i == 6)
                    return 3;
                if (i == 14)
                    return 2;
                return 1;
            }
        }
    }
}
