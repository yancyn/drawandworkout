using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;

namespace WorkOrderGUI
{
    /// <summary>
    /// Globalization resources object.
    /// </summary>
    /// <see>http://www.codeproject.com/KB/cs/Globalization_WPF_SL.aspx</see>
    public class GlobaliztionResource : IValueConverter, INotifyPropertyChanged
    {
        public static string AString { get { return "AString"; } }

        #region Constructor
        /// <summary>
        /// Save a reference to myself for changing cultures
        /// </summary>
        public static GlobaliztionResource Myself;
        public GlobaliztionResource()
        {
            Myself = this;
            WorkOrderGUI.Resources.Labels.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
        }
        #endregion Constructor

        #region ResourceManager

        /// <summary>
        /// A resource manager which returns an error string if the string isn't found
        /// </summary>
        private class ResourceManagerWithErrors
              : global::System.Resources.ResourceManager
        {
            public ResourceManagerWithErrors(Type resourceSource)
                : base(resourceSource) { }
            public ResourceManagerWithErrors(string baseName, Assembly assembly)
                : base(baseName, assembly) { }
            public ResourceManagerWithErrors(string baseName, Assembly assembly,
                Type usingResourceSet)
                : base(baseName, assembly, usingResourceSet) { }

            public string NotFoundMessage = "#Missing#";

            public override string GetString(string name)
            {
                return base.GetString(name) ?? NotFoundMessage + name;
            }
            public override string GetString(string name, CultureInfo culture)
            {
                return base.GetString(name, culture) ?? NotFoundMessage + name;
            }
        }

        /// <summary>
        /// String resource manager for the Strings.resx file
        /// </summary>
        private static ResourceManagerWithErrors _stringResourceManager;
        private static ResourceManagerWithErrors StringResourceManager
        {
            get
            {
                if (object.ReferenceEquals(_stringResourceManager, null))
                {
                    _stringResourceManager = new ResourceManagerWithErrors("WorkOrderGUI.Resources.Labels", // be sure to change to your namespace & file!
                        typeof(GlobaliztionResource).Assembly);

                    //customize your error message
                    _stringResourceManager.NotFoundMessage = "#StringResourceMissing#";
                }
                return _stringResourceManager;
            }
        }

        #endregion ResourceManager

        #region IValueConverter

        /// <summary>
        /// Converter to go find a string based on the UI culture
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value == null) || !(value is string))
                return "set Binding Path/Source!";

            return StringResourceManager.GetString(
                    (string)parameter, System.Threading.Thread.CurrentThread.CurrentUICulture);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("No reason to do this.");
        }
        #endregion IValueConverter

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Change the culture for the application.
        /// </summary>
        /// <param name="culture">Full culture name. ie. en-US, zh-CN.</param>
        public void ChangeCulture(string culture)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            WorkOrderGUI.Resources.Labels.Culture = System.Threading.Thread.CurrentThread.CurrentUICulture;

            // notify that the culture has changed
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null) handler(this, new PropertyChangedEventArgs("AString"));
        }
        #endregion
    }
}