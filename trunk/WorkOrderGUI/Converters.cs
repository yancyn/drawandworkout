﻿using System;
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
        #region IValueConverter Members
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
        #endregion
    }
    /// <summary>
    /// Convert date time, decimal to desire format.
    /// </summary>
    public class NumberConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime target = (DateTime)value;
                return target.ToString("yyMMdd-HHmm");
            }
            if (value is Decimal)
            {
                Decimal target = (Decimal)value;
                return target.ToString("00.00");
            }

            throw new NotImplementedException();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// Convert to percentage value with % sign.
    /// </summary>
    public class PercentageConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Int32)
            {
                Int32 target = (Int32)value;
                return target.ToString("00.00" + "%");
            }
            else if (value is Decimal)
            {
                Decimal target = (Decimal)value;
                target /= 100;
                return target.ToString("00.00" + "%");//todo: check PercentageConverter correctness
            }

            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}