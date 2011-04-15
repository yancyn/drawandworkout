using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkOrderGUI.Sandbox
{
    /// <summary>
    /// Interaction logic for MeasurementIndicator.xaml
    /// </summary>
    public partial class VerticalRulerIndicator : UserControl
    {
        #region Properties
        /// <summary>
        /// Gets or sets the length of indicator line.
        /// </summary>
        /// <remarks>
        /// If Orientation is Horizontal then width = length.
        /// If Orientation is Vertical then height = length.
        /// </remarks>
        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }
        public static DependencyProperty LengthProperty = DependencyProperty.Register(
            "Length",
            typeof(double),
            typeof(VerticalRulerIndicator));
        /// <summary>
        /// Gets or sets value to display in ruler (integer, decimal, or double).
        /// </summary>
        public object Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(object),
            typeof(VerticalRulerIndicator));
        #endregion
        public VerticalRulerIndicator()
        {
            InitializeComponent();
        }
    }
    /// <summary>
    /// Gets top margin for arrow.
    /// </summary>
    public class ArrowTopMarginConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                double length = System.Convert.ToDouble(value);
                return length - 10;
            }
            else if (value is decimal)
            {
                decimal length = System.Convert.ToDecimal(value);
                return length - 10;
            }
            else if (value is int)
            {
                int length = System.Convert.ToInt32(value);
                return length - 10;
            }

            throw new NotImplementedException("Not supported type of " + value.GetType());
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// Gets middle point to determine label position.
    /// </summary>
    public class VerticalMiddlePointConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                double length = System.Convert.ToDouble(value);
                return length / 2;
            }
            else if (value is decimal)
            {
                decimal length = System.Convert.ToDecimal(value);
                return length / 2;
            }
            else if (value is int)
            {
                int length = System.Convert.ToInt32(value);
                return length / 2;
            }

            throw new NotImplementedException("Not supported type of " + value.GetType());
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
