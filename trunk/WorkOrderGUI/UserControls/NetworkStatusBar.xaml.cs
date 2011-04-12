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
using System.Windows.Threading;
using System.Net.NetworkInformation;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for NetworkStatusBar.xaml
    /// </summary>
    /// <remarks>
    /// Not support proxy temporarity. Wirewall need to test as well.
    /// todo: use other than DispatcherTimmer http://msdn.microsoft.com/en-us/library/ms741870.aspx
    /// </remarks>
    public partial class NetworkStatusBar : UserControl
    {
        private DispatcherTimer timer;
        public NetworkStatusBar()
        {
            InitializeComponent();

            this.timer = new DispatcherTimer();

            int interval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["interval"]);
            this.timer.Interval = new TimeSpan(0, 0, interval);
            this.timer.Tick += new EventHandler(timer_Tick);
            this.timer.Start();
        }
        /// <summary>
        /// TODO: Handle svn update status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            long time = PingTime("www.google.com");
            Logger.Info(typeof(NetworkStatusBar), "ping www.google.com:" + time);
            if (time > 0)
            {
                this.Message.Text = "Online";
                string packUri = "pack://application:,,,/Images/status_online.png";
                this.Icon.Source = new ImageSourceConverter().ConvertFrom(packUri) as ImageSource;
            }
            else
            {
                this.Message.Text = "Offline";
                string packUri = "pack://application:,,,/Images/status_offline.png";
                this.Icon.Source = new ImageSourceConverter().ConvertFrom(packUri) as ImageSource;
            }
        }
        /// <summary>
        /// Get ping echo time.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <seealso>http://www.dotnetspider.com/resources/28741-Check-URL-States-Using-Ping-Command.aspx</seealso>
        public long PingTime(string url)
        {
            long time = -1;
            Ping info = new Ping();
            try
            {
                PingReply reply = info.Send(url);
                if (reply.Status == IPStatus.Success)
                    time = reply.RoundtripTime;
                return time;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(NetworkStatusBar), ex);
                return -1;
            }
        }
    }
}