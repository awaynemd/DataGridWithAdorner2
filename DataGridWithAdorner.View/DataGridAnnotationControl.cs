using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataGridWithAdorner.Library;

namespace DataGridWithAdorner.View
{
    public class DataGridAnnotationControl : Control, INotifyPropertyChanged
    {
        #region [INotifyPropertyChanged]
        public event PropertyChangedEventHandler PropertyChanged;

        // using "virtual" here results in call chain violation CA2214
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        static DataGridAnnotationControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataGridAnnotationControl), new FrameworkPropertyMetadata(typeof(DataGridAnnotationControl)));
        }

        public DataGridAnnotationControl()
        {
            BorderBrush = Brushes.Black;
            Background = Brushes.AliceBlue;
            BorderThickness = new Thickness(20, 20, 20, 20);
        }

        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register("LastName", typeof(string), typeof(DataGridAnnotationControl), new PropertyMetadata(string.Empty));

        public IInnerRow Visit
        {
            get { return (IInnerRow)GetValue(VisitProperty); }
            set { SetValue(VisitProperty, value); }
        }
        public static readonly DependencyProperty VisitProperty =
            DependencyProperty.Register("Visit", typeof(IInnerRow), typeof(DataGridAnnotationControl), new PropertyMetadata(null, (s, e) =>
            {
                var sender = s as DataGridAnnotationControl;
                var visit = (IInnerRow)e.NewValue;

                if (visit != null)
                    sender.LastName = visit.CellName;
            }));

        public ICommand SaveAppointmentCommand
        {
            get { return (ICommand)GetValue(SaveAppointmentCommandProperty); }
            set { SetValue(SaveAppointmentCommandProperty, value); }
        }
        public static DependencyProperty SaveAppointmentCommandProperty = DependencyProperty.Register("SaveAppointmentCommand", typeof(ICommand), typeof(DataGridAnnotationControl));

    }
}