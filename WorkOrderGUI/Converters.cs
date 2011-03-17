using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace WorkOrderGUI
{
    /// <summary>
    /// If there is zero row count just hidden otherwise visible.
    /// </summary>
    public class RowCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return ((int)value) > 0 ? Visibility.Visible : Visibility.Hidden; //todo: check Visibility.Collapsed
            }
            else
            {
                throw new ArgumentException("Value should be of type boolean.");
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}