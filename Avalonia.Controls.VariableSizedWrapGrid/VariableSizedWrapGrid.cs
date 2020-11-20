using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;

namespace AvaloniaSample
{
    public class VariableSizedWrapGrid : Panel, IScrollable
    {
        public static readonly StyledProperty<HorizontalAlignment> HorizontalChildrenAlignmentProperty =
            AvaloniaProperty.Register<VariableSizedWrapGrid, HorizontalAlignment>(nameof(HorizontalChildrenAlignment), HorizontalAlignment.Left);

        public static readonly StyledProperty<double> ItemHeightProperty =
            AvaloniaProperty.Register<VariableSizedWrapGrid, double>(nameof(ItemHeight), double.NaN);

        public static readonly StyledProperty<double> ItemWidthProperty =
            AvaloniaProperty.Register<VariableSizedWrapGrid, double>(nameof(ItemWidth), double.NaN);

        public static readonly StyledProperty<bool> LatchItemSizeProperty =
            AvaloniaProperty.Register<VariableSizedWrapGrid, bool>(nameof(LatchItemSize), true);

        public static readonly StyledProperty<int> MaximumRowsOrColumnsProperty =
            AvaloniaProperty.Register<VariableSizedWrapGrid, int>(nameof(MaximumRowsOrColumns), -1);

        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<VariableSizedWrapGrid, Orientation>(nameof(Orientation), Orientation.Vertical);

        public static readonly StyledProperty<bool> StrictItemOrderProperty =
            AvaloniaProperty.Register<VariableSizedWrapGrid, bool>(nameof(StrictItemOrder), true);

        public static readonly StyledProperty<VerticalAlignment> VerticalChildrenAlignmentProperty =
            AvaloniaProperty.Register<VariableSizedWrapGrid, VerticalAlignment>(nameof(VerticalChildrenAlignment), VerticalAlignment.Top);

        public static readonly AttachedProperty<int> ColumnSpanProperty =
            AvaloniaProperty.RegisterAttached<VariableSizedWrapGrid, Control, int>("ColumnSpan", 1);

        public static readonly AttachedProperty<int> RowSpanProperty =
            AvaloniaProperty.RegisterAttached<VariableSizedWrapGrid, Control, int>("RowSpan", 1);

        public static int GetColumnSpan(Control element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            return element!.GetValue(ColumnSpanProperty);
        }

        public static void SetColumnSpan(Control element, int value)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            element!.SetValue(ColumnSpanProperty, value);
        }

        public static int GetRowSpan(Control element)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            return element!.GetValue(RowSpanProperty);
        }

        public static void SetRowSpan(Control element, int value)
        {
            Contract.Requires<ArgumentNullException>(element != null);
            element!.SetValue(RowSpanProperty, value);
        }

        static VariableSizedWrapGrid()
        {
            AffectsArrange<VariableSizedWrapGrid>(
                HorizontalChildrenAlignmentProperty,
                ItemHeightProperty,
                ItemWidthProperty,
                LatchItemSizeProperty,
                MaximumRowsOrColumnsProperty,
                OrientationProperty,
                StrictItemOrderProperty,
                VerticalChildrenAlignmentProperty);

            AffectsMeasure<VariableSizedWrapGrid>(
                ItemHeightProperty,
                ItemWidthProperty,
                LatchItemSizeProperty,
                MaximumRowsOrColumnsProperty,
                OrientationProperty,
                StrictItemOrderProperty);

            AffectsParentArrange<VariableSizedWrapGrid>(ColumnSpanProperty, RowSpanProperty);

            AffectsParentMeasure<VariableSizedWrapGrid>(ColumnSpanProperty, RowSpanProperty);
        }

        public HorizontalAlignment HorizontalChildrenAlignment
        {
            get { return GetValue(HorizontalChildrenAlignmentProperty); }
            set { SetValue(HorizontalChildrenAlignmentProperty, value); }
        }

        public double ItemHeight
        {
            get { return GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public double ItemWidth
        {
            get { return GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public bool LatchItemSize
        {
            get { return GetValue(LatchItemSizeProperty); }
            set { SetValue(LatchItemSizeProperty, value); }
        }

        public int MaximumRowsOrColumns
        {
            get { return GetValue(MaximumRowsOrColumnsProperty); }
            set { SetValue(MaximumRowsOrColumnsProperty, value); }
        }

        public Orientation Orientation
        {
            get { return GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public bool StrictItemOrder
        {
            get { return GetValue(StrictItemOrderProperty); }
            set { SetValue(StrictItemOrderProperty, value); }
        }

        public VerticalAlignment VerticalChildrenAlignment
        {
            get { return GetValue(VerticalChildrenAlignmentProperty); }
            set { SetValue(VerticalChildrenAlignmentProperty, value); }
        }

        private class PlotSorterVertical : IComparer<Rect>
        {
            public int Compare(Rect x, Rect y)
            {
                if (x.Left < y.Left)
                    return -1;
                if (x.Left > y.Left)
                    return 1;
                if (x.Top < y.Top)
                    return -1;
                if (x.Top > y.Top)
                    return 1;
                return 0;
            }
        }

        private class PlotSorterHorizontal : IComparer<Rect>
        {
            public int Compare(Rect x, Rect y)
            {
                if (x.Top < y.Top)
                    return -1;
                if (x.Top > y.Top)
                    return 1;
                if (x.Left < y.Left)
                    return -1;
                if (x.Left > y.Left)
                    return 1;
                return 0;
            }
        }

        private IEnumerable<Rect> ReserveAcreage(Rect acreage, Rect plot)
        {
            if(acreage.Intersects(plot))
            {
                // Above?
                if(plot.Top < acreage.Top)
                {
                    var rest = new Rect(plot.Position, new Size(plot.Width, acreage.Top - plot.Top));
                    yield return rest;
                }

                // Below?
                if(plot.Bottom > acreage.Bottom)
                {
                    var rest = new Rect(new Point(plot.Left, acreage.Bottom), new Size(plot.Width, plot.Bottom - acreage.Bottom));
                    yield return rest;
                }

                // Left?
                if (plot.Left < acreage.Left)
                {
                    var rest = new Rect(plot.Position, new Size(acreage.Left - plot.Left, plot.Height));
                    yield return rest;
                }

                // Right?
                if (plot.Right > acreage.Right)
                {
                    var rest = new Rect(new Point(acreage.Right, plot.Top), new Size(plot.Right - acreage.Right, plot.Height));
                    yield return rest;
                }
            }
            else
            {
                yield return plot;
            }
        }

        private Point PlaceElement(Size requiredSize, ref List<Rect> plots, double itemWidth, double itemHeight)
        {
            var location = new Point();

            foreach (var plot in plots)
            {
                if ((plot.Height >= requiredSize.Height) && (plot.Width >= requiredSize.Width))
                {
                    var acreage = new Rect(plot.Position, requiredSize);

                    Rect innerRect;
                    Rect outerRect;
                    IComparer<Rect> plotSorter;

                    if(Orientation == Orientation.Vertical)
                    {
                        innerRect = new Rect(0, 0, acreage.X + itemWidth, acreage.Y);
                        outerRect = new Rect(0, 0, acreage.X, double.MaxValue);
                        plotSorter = new PlotSorterVertical();
                    }
                    else
                    {
                        innerRect = new Rect(0, 0, acreage.X, acreage.Y + itemHeight);
                        outerRect = new Rect(0, 0, double.MaxValue, acreage.Y);
                        plotSorter = new PlotSorterHorizontal();
                    }

                    List<Rect> localPlots;

                    if (StrictItemOrder)
                    {
                        localPlots = plots.SelectMany(p => ReserveAcreage(acreage, p))
                            .SelectMany(p => ReserveAcreage(outerRect, p))
                            .SelectMany(p => ReserveAcreage(innerRect, p)).Distinct().ToList();
                    }
                    else
                    {
                        localPlots = plots.SelectMany(p => ReserveAcreage(acreage, p)).Distinct().ToList();
                    }

                    localPlots.RemoveAll(x => localPlots.Any(y => y.Contains(x) && !y.Equals(x)));
                    localPlots.Sort(plotSorter);
                    plots = localPlots;

                    location = acreage.Position;
                    break;
                }
            }

            return location;
        }

        private Rect ArrangeElement(Rect acreage, Size desiredSize, Vector offset)
        {
            var rect = acreage;

            // Adjust horizontal location and size for alignment
            switch (HorizontalChildrenAlignment)
            {
                case HorizontalAlignment.Center:
                    rect = rect.WithX(rect.X + Math.Max(0, (acreage.Width - desiredSize.Width) / 2));
                    rect = rect.WithWidth(desiredSize.Width);
                    break;
                case HorizontalAlignment.Left:
                    rect = rect.WithWidth(desiredSize.Width);
                    break;
                case HorizontalAlignment.Right:
                    rect = rect.WithX(rect.X + Math.Max(0, acreage.Width - desiredSize.Width));
                    rect = rect.WithWidth(desiredSize.Width);
                    break;
                case HorizontalAlignment.Stretch:
                default:
                    break;
            }

            // Adjust vertical location and size for alignment
            switch (VerticalChildrenAlignment)
            {
                case VerticalAlignment.Bottom:
                    rect = rect.WithY(rect.Y + Math.Max(0, acreage.Height - desiredSize.Height));
                    rect = rect.WithHeight(desiredSize.Height);
                    break;
                case VerticalAlignment.Center:
                    rect = rect.WithY(rect.Y + Math.Max(0, (acreage.Height - desiredSize.Height) / 2));
                    rect = rect.WithHeight(desiredSize.Height);
                    break;
                case VerticalAlignment.Top:
                    rect = rect.WithHeight(desiredSize.Height);
                    break;
                case VerticalAlignment.Stretch:
                default:
                    break;
            }

            // Adjust location for scrolling offset
            var position = rect.Position - offset;
            rect = new Rect(position.X, position.Y, rect.Width, rect.Height);

            return rect;
        }

        double _itemHeight;
        double _itemWidth;
        // TODO: private ScrollViewer _owner;
        private Size _extent = new Size();
        private Size _viewport = new Size();
        private Vector _offset = new Vector();

        private void SetViewport(Size size)
        {
            if (_viewport != size)
            {
                _viewport = size;
                // TODO: 
                //if (_owner != null)
                //{
                //    _owner.InvalidateScrollInfo();
                //}
            }
        }

        private void SetExtent(Size size)
        {
            if (_extent != size)
            {
                _extent = size;
                // TODO: 
                //if (_owner != null)
                //{
                //    _owner.InvalidateScrollInfo();
                //}
            }
        }

        private IList<Rect>? _finalRects;

        protected override Size MeasureOverride(Size availableSize)
        {
            var desiredSizeMin = new Size();
            var elementSizes = new List<Size>(Children.Count);

            _itemHeight = ItemHeight;
            _itemWidth = ItemWidth;

            foreach (var element in Children)
            {
                Size elementSize = LatchItemSize ?
                    new Size(double.IsNaN(_itemWidth) ? double.MaxValue : _itemWidth * GetColumnSpan((Control)element), double.IsNaN(_itemHeight) ? double.MaxValue : _itemHeight * GetRowSpan((Control)element)) :
                    new Size(double.IsNaN(ItemWidth) ? double.MaxValue : _itemWidth * GetColumnSpan((Control)element), double.IsNaN(ItemHeight) ? double.MaxValue : _itemHeight * GetRowSpan((Control)element));

                // Measure each element providing allocated plot size.
                element.Measure(elementSize);

                // Use the elements desired size as item size in the undefined dimension(s)
                if (double.IsNaN(_itemHeight) || (!LatchItemSize && double.IsNaN(ItemHeight)))
                {
                    elementSize = elementSize.WithHeight(element.DesiredSize.Height);
                }

                if (double.IsNaN(_itemWidth) || (!LatchItemSize && double.IsNaN(ItemWidth)))
                {
                    elementSize = elementSize.WithWidth(element.DesiredSize.Width);
                }

                if (double.IsNaN(_itemHeight))
                {
                    _itemHeight = element.DesiredSize.Height / GetRowSpan((Control)element);
                }

                if (double.IsNaN(_itemWidth))
                {
                    _itemWidth = element.DesiredSize.Width / GetColumnSpan((Control)element);
                }

                // The minimum size of the panel is equal to the largest element in each dimension.
                desiredSizeMin = new Size(
                    Math.Max(desiredSizeMin.Width, elementSize.Width),
                    Math.Max(desiredSizeMin.Height, elementSize.Height));

                elementSizes.Add(elementSize);
            }

            // Always use at least the available size for the panel unless infinite.
            var desiredSize = new Size(
                double.IsPositiveInfinity(availableSize.Width) ? 0 : availableSize.Width,
                double.IsPositiveInfinity(availableSize.Height) ? 0 : availableSize.Height);

            // Available plots on the panel real estate
            var plots = new List<Rect>();

            // Calculate maximum size
            var maxSize = (MaximumRowsOrColumns > 0) ?
                new Size(_itemWidth * MaximumRowsOrColumns, _itemHeight * MaximumRowsOrColumns) :
                new Size(double.MaxValue, double.MaxValue);

            // Add the first plot covering the entire estate.
            var bigPlot = new Rect(new Point(0, 0), (Orientation == Orientation.Vertical) ?
                new Size(double.MaxValue, Math.Max(Math.Min(availableSize.Height, maxSize.Height), desiredSizeMin.Height)) :
                new Size(Math.Max(Math.Min(availableSize.Width, maxSize.Width), desiredSizeMin.Width), double.MaxValue));

            plots.Add(bigPlot);

            _finalRects = new List<Rect>(Children.Count);

            using (var sizeEnumerator = elementSizes.GetEnumerator())
            {
                foreach (var element in Children)
                {
                    sizeEnumerator.MoveNext();
                    var elementSize = sizeEnumerator.Current;

                    // Find a plot able to hold this element.
                    var acreage = new Rect(
                        PlaceElement(elementSize, ref plots, _itemWidth, _itemHeight), elementSize);

                    _finalRects.Add(acreage);

                    // Keep track of panel size...
                    desiredSize = new Size(
                        Math.Max(desiredSize.Width, acreage.Right),
                        Math.Max(desiredSize.Height, acreage.Bottom));
                }
            }

            SetViewport(availableSize);
            SetExtent(desiredSize);

            return desiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_finalRects is null)
            {
                throw new NullReferenceException(nameof(_finalRects));
            }

            var actualSize = new Size(
                double.IsPositiveInfinity(finalSize.Width) ? 0 : finalSize.Width,
                double.IsPositiveInfinity(finalSize.Height) ? 0 : finalSize.Height);

            using (var rectEnumerator = _finalRects.GetEnumerator())
            {
                foreach (var element in Children)
                {
                    rectEnumerator.MoveNext();
                    var acreage = rectEnumerator.Current;

                    // Keep track of panel size...
                    actualSize = new Size(
                        Math.Max(actualSize.Width, acreage.Right),
                        Math.Max(actualSize.Height, acreage.Bottom));

                    // Arrange each element using allocated plot location and size.
                    element.Arrange(ArrangeElement(acreage, element.DesiredSize, _offset));
                }
            }

            // Adjust offset when the viewport size changes
            SetHorizontalOffset(Math.Max(0, Math.Min(_offset.X, _extent.Width - _viewport.Width)));
            SetVerticalOffset(Math.Max(0, Math.Min(_offset.Y, _extent.Height - _viewport.Height)));

            return actualSize;
        }

        Size IScrollable.Extent => _extent;

        Vector IScrollable.Offset
        {
            get => _offset;
            set => _offset = value;
        }

        Size IScrollable.Viewport => _viewport;

        // TODO:
        /*
        // This property is not intended for use in your code. It is exposed publicly to fulfill an interface contract (IScrollInfo). Setting this property has no effect.
        public bool CanVerticallyScroll
        {
            get { return false; }
            set { }
        }

        // This property is not intended for use in your code. It is exposed publicly to fulfill an interface contract (IScrollInfo). Setting this property has no effect.
        public bool CanHorizontallyScroll
        {
            get { return false; }
            set { }
        }

        public double ExtentWidth
        {
            get { return _extent.Width; }
        }

        public double ExtentHeight
        {
            get { return _extent.Height; }
        }

        public double ViewportWidth
        {
            get { return _viewport.Width; }
        }

        public double ViewportHeight
        {
            get { return _viewport.Height; }
        }

        public double HorizontalOffset
        {
            get { return _offset.X; }
        }

        public double VerticalOffset
        {
            get { return _offset.Y; }
        }

        public ScrollViewer ScrollOwner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public void LineUp()
        {
            SetVerticalOffset(VerticalOffset - _itemHeight);
        }

        public void LineDown()
        {
            SetVerticalOffset(VerticalOffset + _itemHeight);
        }

        public void LineLeft()
        {
            SetHorizontalOffset(HorizontalOffset - _itemWidth);
        }

        public void LineRight()
        {
            SetHorizontalOffset(HorizontalOffset + _itemWidth);
        }

        public void PageUp()
        {
            SetVerticalOffset(VerticalOffset - Math.Max(_itemHeight, Math.Max(0, _viewport.Height - _itemHeight)));
        }

        public void PageDown()
        {
            SetVerticalOffset(VerticalOffset + Math.Max(_itemHeight, Math.Max(0, _viewport.Height - _itemHeight)));
        }

        public void PageLeft()
        {
            SetHorizontalOffset(HorizontalOffset - Math.Max(_itemWidth, Math.Max(0, _viewport.Width - _itemWidth)));
        }

        public void PageRight()
        {
            SetHorizontalOffset(HorizontalOffset + Math.Max(_itemWidth, Math.Max(0, _viewport.Width - _itemWidth)));
        }

        public void MouseWheelUp()
        {
            LineUp();
        }

        public void MouseWheelDown()
        {
            LineDown();
        }

        public void MouseWheelLeft()
        {
            LineLeft();
        }

        public void MouseWheelRight()
        {
            LineRight();
        }
        */

        public void SetHorizontalOffset(double offset)
        {
            offset = Math.Max(0, Math.Min(offset, _extent.Width - _viewport.Width));
            if (offset != _offset.X)
            {
                _offset = _offset.WithX(offset);
                // TODO: 
                //if (_owner != null)
                //{
                //    _owner.InvalidateScrollInfo();
                //}
                InvalidateArrange();
            }
        }

        public void SetVerticalOffset(double offset)
        {
            offset = Math.Max(0, Math.Min(offset, _extent.Height - _viewport.Height));
            if (offset != _offset.Y)
            {
                _offset = _offset.WithY(offset);
                // TODO: 
                //if (_owner != null)
                //{
                //    _owner.InvalidateScrollInfo();
                //}
                InvalidateArrange();
            }
        }

        /*
        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            if (rectangle.IsEmpty || visual == null || visual == this || !IsAncestorOf(visual))
            {
                return Rect.Empty;
            }

            rectangle = visual.TransformToAncestor(this).TransformBounds(rectangle);

            Rect viewRect = new Rect(HorizontalOffset, VerticalOffset, ViewportWidth, _viewport.Height);

            // Horizontal
            if (rectangle.Right + HorizontalOffset > viewRect.Right)
            {
                viewRect.X = viewRect.X + rectangle.Right + HorizontalOffset - viewRect.Right;
            }
            if(rectangle.Left + HorizontalOffset < viewRect.Left)
            {
                viewRect.X = viewRect.X - (viewRect.Left - (rectangle.Left + HorizontalOffset));
            }

            // Vertical
            if(rectangle.Bottom + VerticalOffset > viewRect.Bottom)
            {
                viewRect.Y = viewRect.Y + rectangle.Bottom + VerticalOffset- viewRect.Bottom;
            }
            if(rectangle.Top + VerticalOffset < viewRect.Top)
            {
                viewRect.Y = viewRect.Y - (viewRect.Top - (rectangle.Top + VerticalOffset));
            }

            SetHorizontalOffset(viewRect.X);
            SetVerticalOffset(viewRect.Y);

            rectangle.Intersect(viewRect);

            return rectangle;
        }
        */
    }
}
