using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace AvaloniaSample
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

#if DEBUG
            this.AttachDevTools();
#endif

            var colors = typeof(Colors)
                           // using System.Reflection;
                           .GetProperties()
                           .Select((c, i) => new
                           {
                               Color = (Color)c.GetValue(null)!,
                               Name = c.Name,
                               Index = i,
                               ColSpan = ColSpan(i),
                               RowSpan = RowSpan(i)
                           });

            DataContext = colors;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private object RowSpan(int i)
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

        private object ColSpan(int i)
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
