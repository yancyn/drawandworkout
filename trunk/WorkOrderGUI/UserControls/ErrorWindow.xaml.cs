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
using System.Windows.Shapes;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    /// <remarks>
    /// <example>
    /// <code>
    /// new ErrorWindow().ShowDialog(ex);
    /// </code>
    /// </example>
    /// </remarks>
    public partial class ErrorWindow : Window
    {
        /// <summary>
        /// Error message from Window.
        /// </summary>
        private string message;
        public ErrorWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Show dialog of the error message.
        /// </summary>
        /// <param name="sender"></param>
        public void ShowDialog(object sender)
        {
            Logger.Error(typeof(WorkOrderGUI.MainWindow), sender.ToString());
            if (sender is Exception)
                message = (sender as Exception).Message;
            else
                message = sender.ToString();
            this.Message.Text = message;
            this.ShowDialog();
        }
        /// <summary>
        /// Close the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Activate email client from desktop.
        /// This need mapi32.dll to be installed in Windows.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //found a better MAPI class to attach file. Thanks to David M Brooks
                Mapi mapi = new Mapi();
                string recipient = System.Configuration.ConfigurationManager.AppSettings["mailto"].ToString();
                mapi.AddRecipientTo(recipient);
                string path = AppDomain.CurrentDomain.BaseDirectory + "error.log";//todo: retrieve from log4net variable
                //string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + System.IO.Path.DirectorySeparatorChar + "SLIQ" + System.IO.Path.DirectorySeparatorChar + "error.log";
                mapi.AddAttachment(path);
                mapi.SendMailPopup("WorkOrderGUI Error", this.message);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ErrorWindow), ex);
                return;
            }
        }
    }
}