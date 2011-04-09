using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using HLGranite.Drawing;

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
    /// Convert full date into short date format (ie. dd/MM/yyyy).
    /// </summary>
    public class ShortDateConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (value is DateTime)
            {
                DateTime date = (DateTime)value;
                return date.ToString("dd/MM/yyyy");
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
    /// <summary>
    /// Convert Metric to cm and British to inch.
    /// </summary>
    public class UomStringConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((HLGranite.Drawing.Unit)value == HLGranite.Drawing.Unit.British)
                return "inch";
            else if ((HLGranite.Drawing.Unit)value == HLGranite.Drawing.Unit.Metric)
                return "cm";

            throw new NotImplementedException();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// Convert Metric to cm and British to ".
    /// </summary>
    public class UomUnitConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((HLGranite.Drawing.Unit)value == HLGranite.Drawing.Unit.British)
                return "\"";
            else if ((HLGranite.Drawing.Unit)value == HLGranite.Drawing.Unit.Metric)
                return "cm";

            throw new NotImplementedException();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class FeetUnitConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                return ConvertInchToFeet(System.Convert.ToDouble(value));
            }

            throw new NotImplementedException();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
        private string ConvertInchToFeet(double sender)
        {
            string output = string.Empty;
            int i = sender.ToString().IndexOf('.');

            if (i == -1)
            {
                double remainder = sender % 12;
                int result = System.Convert.ToInt32((sender - remainder) / 12);
                if (result > 0)
                {
                    output = result.ToString();
                    output += "'";
                }
                if (remainder > 0) output += " " + remainder.ToString() + "\"";
            }
            else
            {
                double roundNumber = System.Convert.ToDouble(sender.ToString().Substring(0, i));
                double floatingPoint = sender - roundNumber;
                if (floatingPoint + 15 / 16 > 1) //round up if bigger 0.9375
                    output = ConvertInchToFeet(roundNumber + 1);
                else
                    output = ConvertInchToFeet(roundNumber) + " " + ConvertToEighthInch(floatingPoint);
            }

            return output;
        }
        private string ConvertToEighthInch(double sender)
        {
            string output = string.Empty;
            double interval = 0.0625;// 0.125;
            string[] buckets = new string[16] {
                "0", "1/16", 
                "⅛", "3/16",
                "¼", "5/16",
                "⅜", "7/16",
                "½", "9/16",
                "⅝", "11/16",
                "¾", "13/16",
                "⅞", "15/16" };
            for (int i = 0; i < 16; i++)
            {
                if (sender < interval * (i + 1))
                {
                    output += buckets[i];
                    break;
                }
            }
            if (output.Length == 0 && sender < 1) output = "1";//this should be exception case

            output += "\"";
            return output;
        }
    }
    /// <summary>
    /// Get index of collection.
    /// </summary>
    /// <see>http://blogs.microsoft.co.il/blogs/davids/archive/2010/04/17/how-to-bind-to-the-index-of-a-collection-in-wpf.aspx</see>
    public class IndexConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length > 1)
            {
                if (values[0] is LengthItem && values[1] is ObservableCollection<LengthItem>)
                {
                    LengthItem item = values[0] as LengthItem;
                    ObservableCollection<LengthItem> collection = values[1] as ObservableCollection<LengthItem>;
                    return "L" + (collection.IndexOf(item) + 1).ToString();
                }
                if (values[0] is WorkItem && values[1] is ObservableCollection<WorkItem>)
                {
                    WorkItem item = values[0] as WorkItem;
                    ObservableCollection<WorkItem> collection = values[1] as ObservableCollection<WorkItem>;
                    return (collection.IndexOf(item) + 1).ToString() + ".";
                }
            }

            //throw new NotImplementedException();
            return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return null;
        }
        #endregion
    }
    /// <summary>
    /// Auto numbering work item collection for those only required to working on
    /// especially only RectItem take into account.
    /// </summary>
    public class LabelingConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length > 1)
            {
                string number = string.Empty;
                if (!(values[0] is RectItem)) return number;
                if (!(values[1] is WorkOrder)) return number;

                RectItem rect = values[0] as RectItem;
                WorkOrder wo = values[1] as WorkOrder;
                int i = 0;
                MatchItemInCollection(wo.Items, rect, ref i);
                number = i.ToString() + ".";//todo: to desire format based on setting

                return number;
            }

            throw new NotImplementedException();
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
        private bool MatchItemInCollection(ObservableCollection<WorkItem> collection, WorkItem source, ref int counter)
        {
            bool match = false;
            foreach (WorkItem item in collection)
            {
                if (item is RectItem) counter++;
                if (item.Guid.Equals(source.Guid))
                {
                    match = true;
                    return match;
                }

                if (!match && item.Elements.Count > 0)
                    match = MatchItemInCollection(item.Elements, source, ref counter);
                if (match == true) return match;
            }

            return match;
        }
    }
    /// <summary>
    /// Convert top and left into margin measurement.
    /// </summary>
    public class MarginConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length > 1)
            {
                double left = (double)values[0];
                double top = (double)values[1];

                Thickness output = new Thickness(left, top, 0, 0);
                return output;
            }

            return null;//throw new NotImplementedException();
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
            //throw new NotImplementedException();
        }
        #endregion
    }
    /// <summary>
    /// Return order by or delivery to name (which is not empty).
    /// </summary>
    public class SoldToConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length > 1)
            {
                if (values[0] is string && values[1] is string)
                {
                    if (values[0].ToString().Length == 0)
                        return values[1].ToString();
                    else
                        return values[0].ToString();
                }

                throw new NotImplementedException();
            }

            throw new NotImplementedException();
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class CompanyAllInfoConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string output = string.Empty;
            output += WorkOrderGUI.Properties.Settings.Default.CompanyProfile.DisplayName;
            output += "\n" + WorkOrderGUI.Properties.Settings.Default.CompanyProfile.DeliveryAddresses[0].Street;

            output += "\n" + WorkOrderGUI.Properties.Settings.Default.CompanyProfile.DeliveryAddresses[0].City;
            output += " " + WorkOrderGUI.Properties.Settings.Default.CompanyProfile.DeliveryAddresses[0].PostalCode;

            output += "\n" + "Tel/Fax: " + WorkOrderGUI.Properties.Settings.Default.CompanyProfile.Phones[0].FullNumber;
            output += "\n" + "hp: " + WorkOrderGUI.Properties.Settings.Default.CompanyProfile.Phones[2].FullNumber;
            output += "  " + WorkOrderGUI.Properties.Settings.Default.CompanyProfile.Phones[3].FullNumber;

            output += "\n" + "Email: " + WorkOrderGUI.Properties.Settings.Default.CompanyProfile.EmailAddresses[0].Address;

            return output;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}