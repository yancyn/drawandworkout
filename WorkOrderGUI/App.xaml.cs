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
            //todo: CultureInfo ci = new CultureInfo("zh-CN");
            ci.DateTimeFormat.LongDatePattern = "dd/MM/yyyy HH:mm";
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
    partial class EntryPoint
    {
        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool SetForegroundWindow(IntPtr hWnd);
        //private static Mutex m_Mutex;

        [STAThread]
        public static void Main()
        {
            WorkOrderGUI.App app = null;

            try
            {
                //m_Mutex = new Mutex(true, "QualityQuestStartupMutex", out createdNew);
                app = new App();
                app.InitializeComponent();
                app.Run();
            }
            catch (Exception ex)
            {
                //catch unhandle error especially xaml build error.

                //is this a good practice? will slow down performance?
                //if so will be remove when the application is stable
                //Logger.Error(typeof(EntryPoint), ex);
                new ErrorWindow().ShowDialog(ex);

                // Shutting down the application as an unrecoverable error has occurred.
                if (app != null) app.Shutdown();
            }
        }
    }
}