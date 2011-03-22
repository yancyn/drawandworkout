using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Globalization;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //define own date format for application instead of rely on Windows environment.
            CultureInfo ci = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
            ci.DateTimeFormat.LongDatePattern = "dd/MM/yyyy HH:mm";
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }
    }
}