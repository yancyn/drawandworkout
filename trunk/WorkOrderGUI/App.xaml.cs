using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;

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
            //WorkOrderGUI.Properties.Settings.Default.Language
            CultureInfo ci = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);//"en-US"
            //CultureInfo ci = new CultureInfo("zh-CN");
            ci.DateTimeFormat.LongDatePattern = "dd/MM/yyyy HH:mm";
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            string theme = (WorkOrderGUI.Properties.Settings.Default.Theme == HLGranite.Drawing.Theme.Classic)
                ? "Classic" : "Professional";
            App.Current.Resources.MergedDictionaries.Add((
                System.Windows.ResourceDictionary)System.Windows.Application.LoadComponent(
                new System.Uri("/WorkOrderGUI;component/Themes/" + theme + ".xaml",
                    System.UriKind.Relative)));
        }
    }
    partial class EntryPoint
    {
        [STAThread]
        public static void Main()
        {
            WorkOrderGUI.App app = null;

            try
            {
                app = new App();
                app.InitializeComponent();
                app.Run();
            }
            catch (Exception ex)
            {
                new ErrorWindow().ShowDialog(ex);

                // Shutting down the application as an unrecoverable error has occurred.
                if (app != null) app.Shutdown();
            }
        }
    }
}