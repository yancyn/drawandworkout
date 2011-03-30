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
using System.Windows.Markup;
using System.IO;

using HLGranite.Drawing;
using Thought.vCards;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadShortcut();
        }

        #region Methods
        private void Initialize()
        {
            try
            {
                //ConvertWindowToDataTemplate();

                ToolbarManager toolbarManager = new ToolbarManager();
                this.Toolbox.ItemsSource = toolbarManager.Items;

                //testing only
                #region Create example page
                PageManager pageManager = new PageManager();
                Project project = CreateProject();
                PageViewModel page = new PageViewModel(project);
                pageManager.Add(page);

                Project project2 = CreateProject();
                PageViewModel page2 = new PageViewModel(project2);
                pageManager.Add(page2);
                #endregion
                this.MainGrid.DataContext = pageManager;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
        }
        /// <summary>
        /// todo: Try to convert Window content into DataTemplate.
        /// </summary>
        /// <remarks>Fail.</remarks>
        private void ConvertWindowToDataTemplate()
        {
            ProjectWindow win = new ProjectWindow();
            Grid grid = (Grid)win.Content;
            string xaml = "<DataTemplate";
            xaml += " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"";
            xaml += " xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"";
            xaml += " xmlns:w=\"http://schemas.microsoft.com/wpf/2008/toolkit\">";
            //System.Windows.Markup.XamlWriter.Save(
            xaml += System.Windows.Markup.XamlWriter.Save(grid);
            xaml += "</DataTemplate>";
            win.Close();

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xaml));
            ParserContext context = new ParserContext();
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("w", "http://schemas.microsoft.com/wpf/2008/toolkit");
            this.Resources.Add("ProjectTemplate", (DataTemplate)XamlReader.Load(stream, context));

            //win.Close();
            //DataTemplate template = new DataTemplate(grid);
            //this.Resources.Add("ProjectTemplate", template);
        }
        private Project CreateProject()
        {
            Employee creator = new Employee();
            creator.EmailAddresses.Add(new vCardEmailAddress { Address = "yancyn@gmail.com" });

            Customer agent = new Customer { GivenName = "John" + new Random().Next(20) };

            Customer customer = new Customer { GivenName = "Ah Hock" };
            customer.Phones.Add(new vCardPhone { FullNumber = "012-4711134" });
            vCardDeliveryAddress deliver = new vCardDeliveryAddress();
            deliver.Street = "963 Jalan 6";
            deliver.Region = "Machang Bubok";
            deliver.City = "Bukit Mertajam";
            deliver.PostalCode = "05400";
            deliver.Country = "Malaysia";
            customer.DeliveryAddresses.Add(deliver);
            customer.Latitude = 6.09105f;
            customer.Longitude = 100.44629f;

            Project target = new Project();
            target.CreatedBy = creator;
            target.DeliverTo = customer;//customer.DeliveryAddresses[0];
            target.OrderBy = agent;
            target.Stage = ProjectStage.Draft;

            int size = DatabaseObject.Stocks.Stock.Count;
            Stock stock = DatabaseObject.Stocks.Stock[new Random().Next(size)];
            WorkOrder wo = new WorkOrder();
            wo.Items.Add(new WorkItem { MaxHeight = 24, MaxWidth = 56, Material = stock, Progress = 10 });
            wo.Items.Add(new WorkItem { MaxHeight = 12, MaxWidth = 34, Material = stock, Progress = 30 });
            target.WorkOrders.Add(wo);

            return target;
        }
        /// <summary>
        /// Obsolete. Build up menu for application.
        /// </summary>
        /// <returns></returns>
        private List<MenuItem> BuildMenu()
        {
            List<MenuItem> items = new List<MenuItem>();

            MenuItem item1 = new MenuItem { Name = "_File" };
            item1.Items.Add(new MenuItem { Name = "_New" });
            item1.Items.Add(new MenuItem { Name = "_Open" });
            item1.Items.Add(new MenuItem { Name = "_Save" });
            item1.Items.Add(new Separator());
            item1.Items.Add(new MenuItem { Name = "_Close" });

            MenuItem item2 = new MenuItem { Name = "_Edit" };
            //item.Command = "";
            item2.Items.Add(new MenuItem { Name = "_Cut" });
            item2.Items.Add(new MenuItem { Name = "_Copy" });
            item2.Items.Add(new MenuItem { Name = "_Paste" });

            MenuItem item3 = new MenuItem { Name = "_Help" };
            item3.Items.Add(new MenuItem { Name = "_Index" });
            item3.Items.Add(new MenuItem { Name = "_About" });

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            return items;
        }
        /// <summary>
        /// todo: Loading shortcut definitions.
        /// </summary>
        /// <see>http://stackoverflow.com/questions/2382916/binding-a-wpf-shortcut-key-to-a-command-in-the-viewmodel</see>
        private void LoadShortcut()
        {
            this.InputBindings.Add(new KeyBinding(this.NewMenu.Command, new KeyGesture(Key.N, ModifierKeys.Control)));
            //this.InputBindings.Add(new KeyBinding(this.NewMenu.Command, new KeyGesture(Key.W, ModifierKeys.Control)));
        }
        #endregion

        #region Events
        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            string version = "ver " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MessageBox.Show("yancyn@gmail.com" + "\n" + version);
        }
        private void ReportBugLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Hyperlink hyperlink = (sender as Hyperlink);
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(hyperlink.NavigateUri.ToString()));
        }
        #endregion
    }
}