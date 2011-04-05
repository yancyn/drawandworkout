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
using HLGranite.Drawing;
using Thought.vCards;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        private PageManager pageManager;
        private ToolbarManager toolbarManager;
        private WorkItem selectedWorkItem;
        private Point originalPoint;
        public ProjectWindow()
        {
            InitializeComponent();

            this.toolbarManager = new ToolbarManager();
            this.toolbarManager.SelectedToolbar = this.toolbarManager.Items[1].Children[4];

            this.pageManager = new PageManager();
            Project project = CreateProject();
            PageViewModel page = new PageViewModel(project);
            pageManager.Add(page);
            this.DataContext = pageManager;//.Items[0];
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

            LShapeItem w1 = new LShapeItem();//WorkItem w1 = new WorkItem();
            w1.Tags.Add("LShapeItem04");
            w1.Material = stock;
            w1.Lengths.Add(new LengthItem { Length = 108 });
            w1.Lengths.Add(new LengthItem { Length = 24 });
            w1.Lengths.Add(new LengthItem { Length = 84 });
            w1.Lengths.Add(new LengthItem { Length = 24 });
            w1.Lengths.Add(new LengthItem { Length = 24 });
            w1.Lengths.Add(new LengthItem { Length = 48 });
            w1.MaxHeight = 108;
            w1.MaxWidth = 48;

            RectItem w2 = new RectItem();
            w2.Tags.Add("RectItem00");
            w2.Material = stock;
            w2.Height = 6;
            w2.Width = 24;
            w2.Top = 400;
            w2.Left = 200;

            RectItem w3 = new RectItem();
            w3.Tags.Add("RectItem00");
            w3.Material = stock;
            w3.Height = 6;
            w3.Width = 24;
            //w3.Top = 100;
            //w3.Left = 200;


            WorkOrder wo = new WorkOrder();
            wo.Items.Add(w1);
            wo.Items.Add(w2);
            wo.Items.Add(w3);
            target.WorkOrders.Add(wo);

            return target;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Project project = (this.pageManager.CurrentPage.Item as Project);
            project.Save();//project.Save(this.DrawingArea);
        }

        private void DrawingArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DrawingArea_MouseLeftButtonDown: " + new Random().Next() + " " + e.Device.Target.ToString());
            this.originalPoint = e.GetPosition(this.DrawingArea);
            if (e.Device.Target is Shape)
            {
                this.selectedWorkItem = (e.Device.Target as Shape).DataContext as WorkItem;
                System.Diagnostics.Debug.WriteLine(this.selectedWorkItem.ToString());
            }
        }
        private void ScrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DrawingArea_MouseLeftButtonUp" + new Random().Next());
            if (this.selectedWorkItem == null)
            {
                if (this.toolbarManager.SelectedToolbar != null)
                {
                    LShapeItem wo = new LShapeItem();
                    wo.Tags.Add(this.toolbarManager.SelectedToolbar.Name);
                    (this.pageManager.Items[0].Item as Project).WorkOrders[0].Items.Add(wo);
                }
            }
            else //if (this.selectedWorkItem != null)
            {
                Point newPosition = e.GetPosition(this.DrawingArea);
                this.selectedWorkItem.Left += newPosition.X - originalPoint.X;
                this.selectedWorkItem.Top += newPosition.Y - originalPoint.Y;
                this.selectedWorkItem = null;//reset
            }
        }
    }
}