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
using System.Windows.Xps;
using System.IO;
using System.Printing;

using HLGranite.Drawing;
using Thought.vCards;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private PageManager pageManager;
        private ToolbarManager toolbarManager;
        #endregion

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

                this.toolbarManager = new ToolbarManager();
                this.Toolbox.DataContext = toolbarManager;
                this.Toolbox.ItemsSource = toolbarManager.Items;

                //testing only
                this.pageManager = new PageManager();
                Project project = CreateProject();
                PageViewModel page = new PageViewModel(project);
                pageManager.Add(page);
                this.MainGrid.DataContext = pageManager;
            }
            catch (Exception ex)
            {
                Logger.Info(typeof(MainWindow), ex);
                throw ex;
            }
        }
        /// <summary>
        /// Print visual object fit to page.
        /// </summary>
        /// <param name="printDialog"></param>
        /// <param name="project"></param>
        /// <param name="grid"></param>
        /// <see>http://www.a2zdotnet.com/View.aspx?id=66</see>
        private void PrintFitToPage(PrintDialog printDialog, Project project, Grid grid)
        {
            System.Printing.PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);

            //get scale of the print wrt to screen of WPF visual
            double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / grid.ActualWidth,
                capabilities.PageImageableArea.ExtentHeight / grid.ActualHeight);

            //Transform the Visual to scale
            grid.LayoutTransform = new ScaleTransform(scale, scale);

            //get the size of the printer page
            Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

            //update the layout of the visual to the printer page size.
            grid.Measure(sz);
            grid.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

            //now print the visual to printer to fit on the one page.
            printDialog.PrintVisual(grid, project.Guid.ToString());//Fit to Page WPF Print
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
        /// Loading shortcut definitions.
        /// </summary>
        /// <see>http://stackoverflow.com/questions/2382916/binding-a-wpf-shortcut-key-to-a-command-in-the-viewmodel</see>
        private void LoadShortcut()
        {
            this.InputBindings.Add(new KeyBinding(this.pageManager.NewProject, new KeyGesture(Key.N, ModifierKeys.Control)));

            //todo: KeyBinding removeKey = new KeyBinding(this.pageManager.RemovePage, new KeyGesture(Key.W, ModifierKeys.Control));
            //removeKey.CommandParameter = this.pageManager.CurrentPage;
            //this.InputBindings.Add(removeKey);

            //KeyBinding removeKey2 = new KeyBinding(this.pageManager.RemovePage, new KeyGesture(Key.F4, ModifierKeys.Control));
            //removeKey2.CommandParameter = this.pageManager.CurrentPage;
            //this.InputBindings.Add(removeKey2);
        }

        /// <summary>
        /// Find visual child appear to interface after binding which visible or currently selected only.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        private Project CreateProject()
        {
            Employee creator = new Employee();
            creator.EmailAddresses.Add(new vCardEmailAddress { Address = "yancyn@gmail.com" });

            Customer agent = new Customer { GivenName = "One Kitchen" + new Random().Next(20) };

            Customer customer = new Customer { GivenName = "Ah Shing" };
            customer.Phones.Add(new vCardPhone { FullNumber = "012-4711134" });
            vCardDeliveryAddress deliver = new vCardDeliveryAddress();
            deliver.Street = "963 Jalan 6\nMachang Bubok";
            deliver.City = "Bukit Mertajam";
            deliver.Region = "Penang";
            deliver.PostalCode = "14020";
            deliver.Country = "Malaysia";
            customer.DeliveryAddresses.Add(deliver);
            customer.Latitude = 5.33398f;
            customer.Longitude = 100.50754f;

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


            WorkOrder wo = new WorkOrder();
            wo.Items.Add(w1);
            wo.Items.Add(w2);
            wo.Items.Add(w3);
            target.WorkOrders.Add(wo);

            return target;
        }
        /// <summary>
        /// todo: Try to convert Window content into DataTemplate.
        /// </summary>
        /// <remarks>Fail.</remarks>
        private void ConvertWindowToDataTemplate()
        {
            /*
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
            */
        }
        #endregion

        #region Events
        private void Toolbox_Checked(object sender, RoutedEventArgs e)
        {
            this.toolbarManager.SelectedToolbar = (sender as RadioButton).DataContext as ToolbarViewModel;
            Logger.Info(typeof(MainWindow), this.toolbarManager.SelectedToolbar);
            if (this.toolbarManager.SelectedToolbar == null)
                this.StatusBar1.Text = "Ready";
            else if (this.toolbarManager.SelectedToolbar.Name.Length == 0)
                this.StatusBar1.Text = "Ready";
            else
                this.StatusBar1.Text = this.toolbarManager.SelectedToolbar.Name;
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            //testing only
            //ToolbarManager manager = this.Toolbox.DataContext as ToolbarManager;
            //manager.Items[2].Children[3].ReplaceParent();
        }
        private void PrintMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.pageManager.CurrentPage.Item is Project)
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    //ContentPresenter tabChildren = FindVisualChild<ContentPresenter>(this.MainTabControl);//key
                    //Canvas canvas = (this.FindResource("ProjectTemplate") as DataTemplate).FindName("DrawingArea", tabChildren) as Canvas;
                    //printDialog.PrintVisual(canvas, this.Title);

                    Project project = this.pageManager.CurrentPage.Item as Project;
                    PrintingWindow printingWin = new PrintingWindow(project);
                    printingWin.Show();
                    Grid grid = printingWin.Content as Grid;

                    //scale smaller to allow put in numbering specification at right bottom
                    Canvas canvas = (Canvas)grid.FindName("DrawingArea");
                    canvas.LayoutTransform = new ScaleTransform(0.9, 0.9);//key

                    //get selected printer capabilities
                    PrintFitToPage(printDialog, project, grid);
                }
            }
        }

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