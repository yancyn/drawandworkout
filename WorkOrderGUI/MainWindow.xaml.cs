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
using HLGranite.Drawing;
using Thought.vCards;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// <remarks>
    /// todo: still buggy for popup multiple toolbar.
    /// TODO: change the selected shape toolbar if there are muliple choices.
    /// todo: toolbox imcompatible on different screen resolution.
    /// </remarks>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region Create example page
            PageManager pageManager = new PageManager();
            Project project = CreateProject();
            PageViewModel page = new PageViewModel(project);
            pageManager.Add(page);

            Project project2 = CreateProject();
            PageViewModel page2 = new PageViewModel(project2);
            pageManager.Add(page2);

            Stocks stocks = new Stocks();
            PageViewModel page3 = new PageViewModel(stocks);
            pageManager.Add(page3);
            #endregion
            this.MainGrid.DataContext = pageManager;

            ToolbarManager toolbarManager = new ToolbarManager();
            this.Toolbox.ItemsSource = toolbarManager.Items;
            //if (this.FindResource("MenuLShapeCollection") != null)
            //{
            //    ContextMenu menu = (this.FindResource("MenuLShapeCollection") as ContextMenu);
            //    ToolbarManager toolbarManager = new ToolbarManager();
            //    System.Diagnostics.Debug.WriteLine(toolbarManager.Items.Count);
            //    //this.LShapeToolbar.ItemsSource = toolbarManager.Items;
            //    if (menu.Items.Count > 0) ((System.Windows.Controls.ItemsControl)(menu.Items[0])).ItemsSource = toolbarManager.Items;
            //}
        }
        private Project CreateProject()
        {
            Employee creator = new Employee();
            creator.EmailAddresses.Add(new vCardEmailAddress { Address = "yancyn@gmail.com" });

            Customer agent = new Customer { GivenName = "John" };

            Customer customer = new Customer { GivenName = "Ah Hock" };
            vCardDeliveryAddress deliver = new vCardDeliveryAddress();
            deliver.Street = "963 Jalan 6";
            deliver.Region = "Machang Bubok";
            deliver.City = "Bukit Mertajam";
            deliver.PostalCode = "05400";
            deliver.Country = "Malaysia";
            customer.DeliveryAddresses.Add(deliver);

            Project target = new Project();
            target.CreatedBy = creator;
            target.DeliverTo = customer;//customer.DeliveryAddresses[0];
            target.OrderBy = agent;
            target.Stage = ProjectStage.Draft;

            WorkOrder wo = new WorkOrder();
            wo.Items.Add(new WorkItem { MaxHeight = 24, MaxWidth = 56, Material = new Stock { Name1 = "Tan Brown" } });
            wo.Items.Add(new WorkItem { MaxHeight = 12, MaxWidth = 34, Material = new Stock { Name1 = "Black Galaxy" } });
            target.WorkOrders.Add(wo);

            return target;
        }

        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

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
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            string version = "ver " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MessageBox.Show("yancyn@gmail.com" + "\n" + version);
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}