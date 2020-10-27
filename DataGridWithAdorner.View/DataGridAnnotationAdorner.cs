using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using DataGridWithAdorner.Library;

namespace DataGridWithAdorner.View
{
    public class DataGridAnnotationAdorner : Adorner
    {
        public DataGridAnnotationControl Control { get; }
        public Grid AdornedDataGrid { get; }

        public DataGridAnnotationAdorner(Grid adornedDataGrid, IInnerRow visit, object dc)
            : base(adornedDataGrid)
        {
            AdornedDataGrid = adornedDataGrid;

            Control = new DataGridAnnotationControl();

            var cmdBinding = new Binding("SaveAppointmentCommand")
            {
                Source = dc
            };
            BindingOperations.SetBinding(Control, DataGridAnnotationControl.SaveAppointmentCommandProperty, cmdBinding);

            var lastNameBinding = new Binding("LastName")
            {
                Source = dc,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(Control, DataGridAnnotationControl.LastNameProperty, lastNameBinding);

            var visitBinding = new Binding("Visit")
            {
                Source = dc,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            BindingOperations.SetBinding(Control, DataGridAnnotationControl.VisitProperty, visitBinding);

            AddLogicalChild(Control);
            AddVisualChild(Control);

            Control.Visit = visit;
        }

        #region Measure/Arrange      
        protected override Size MeasureOverride(Size constraint)
        {
            Control.Measure(constraint);
            return Control.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = (AdornedDataGrid.ActualWidth - Control.DesiredSize.Width) / 2;
            var y = (AdornedDataGrid.ActualHeight - Control.DesiredSize.Height) / 2;
            var p = new Point(x, y);
            var r = new Rect(p, finalSize);

            Control.Arrange(r);

            return finalSize;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var screenBrush = new SolidColorBrush
            {
                Color = Colors.Crimson,
                Opacity = 0.3
            };
            screenBrush.Freeze();
            var rectangle = new Rect(new Point(0, 0), AdornedElement.DesiredSize);

            drawingContext.DrawRectangle(screenBrush, null, rectangle);

            base.OnRender(drawingContext);
        }
        #endregion

        #region [Visual Children]
        protected override int VisualChildrenCount => 1;
        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            return Control;
        }
        #endregion

        #region Logical Children
        protected override IEnumerator LogicalChildren => Enumerable.Repeat(Control, 1).GetEnumerator();
        #endregion
    }
}
