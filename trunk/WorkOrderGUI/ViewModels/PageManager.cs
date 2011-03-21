using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using HLGranite.Drawing;

namespace WorkOrderGUI
{
    /// <summary>
    /// Page collection manager for Tab Control use.
    /// </summary>
    public class PageManager : System.ComponentModel.INotifyPropertyChanged
    {
        #region Properties
        private PageViewModel currentPage;
        /// <summary>
        /// Gets or sets current selected page.
        /// </summary>
        public PageViewModel CurrentPage
        {
            get { return this.currentPage; }
            set
            {
                if (this.currentPage != null)
                {
                    if (this.currentPage.Equals(value) != true)
                    {
                        this.currentPage = value;
                        this.OnPropertyChanged("CurrentPage");
                    }
                }
                else
                {
                    this.currentPage = value;
                    this.OnPropertyChanged("CurrentPage");
                }
            }
        }
        private int currentPageIndex;
        /// <summary>
        /// Gets or sets current page index.
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                this.currentPageIndex = this.items.IndexOf(this.currentPage);
                return this.currentPageIndex;
            }
            set
            {
                this.currentPageIndex = value;
                this.currentPage = this.items[value];
                this.OnPropertyChanged("CurrentPageIndex");
            }
        }
        private ObservableCollection<PageViewModel> items;
        public ObservableCollection<PageViewModel> Items
        {
            get { return this.items; }
            set
            {
                if (this.items != null)
                {
                    if (this.items.Equals(value) != true)
                    {
                        this.items = value;
                        this.OnPropertyChanged("Items");
                    }
                }
                else
                {
                    this.items = value;
                    this.OnPropertyChanged("Items");
                }
            }
        }
        #endregion

        #region ICommand
        private NewProject newProject;
        public NewProject NewProject { get { return this.newProject; } }
        private RemovePage removePage;
        public RemovePage RemovePage { get { return this.removePage; } }
        private SelectPage selectPage;
        public SelectPage SelectPage { get { return this.selectPage; } }
        private OpenInventory openInventory;
        public OpenInventory OpenInventory { get { return this.openInventory; } }
        private OpenContact openContact;
        public OpenContact OpenContact { get { return this.openContact; } }
        private OpenWarehouse openWarehouse;
        public OpenWarehouse OpenWarehouse { get { return this.openWarehouse; } }
        #endregion

        public PageManager()
        {
            this.items = new ObservableCollection<PageViewModel>();
            this.newProject = new NewProject(this);
            this.removePage = new RemovePage(this);
            this.selectPage = new SelectPage(this);

            this.openInventory = new OpenInventory(this);
            this.openContact = new OpenContact(this);
            this.openWarehouse = new OpenWarehouse(this);
        }

        #region Methods
        public void Add(PageViewModel item)
        {
            /*bool found = false;
            foreach (PageViewModel viewModel in this.items)
            {
                if (viewModel.Item == null)
                {
                    viewModel.IsSelected = false;
                    continue;
                }

                if (viewModel.Item.GetType() == typeof(Project))
                {
                    //todo: overwrite Contains with key checking on Guid instead of whole memory stack
                    if ((viewModel.Item as Project).Guid.ToString()
                        .Equals((item.Item as Project).Guid.ToString()))
                    {
                        found = true;
                        viewModel.IsSelected = true;
                        this.currentPage = viewModel;
                    }
                    else
                        viewModel.IsSelected = false;
                }
                else if (viewModel.Item.GetType() == item.Item.GetType())
                {
                    found = true;
                    viewModel.IsSelected = true;
                    this.currentPage = viewModel;
                }
                else
                    viewModel.IsSelected = false;
            }

            if (!found)
            {
                this.items.Add(item);
                this.items[this.items.Count - 1].IsSelected = true;
                this.currentPage = item;
            }*/


            
            if (item.Item is DatabaseObject)
            {
                bool found = false;
                foreach (PageViewModel viewModel in this.Items)
                {
                    if (viewModel.Item == null)
                    {
                        viewModel.IsSelected = false;
                        continue;
                    }

                    if (item.Item.GetType() == viewModel.Item.GetType())
                    {
                        found = true;
                        viewModel.IsSelected = true;
                        this.currentPage = item;
                    }
                    else
                        viewModel.IsSelected = false;
                }

                if (!found)
                {
                    this.items.Add(item);
                    this.currentPage = this.items[this.items.Count - 1];
                    this.items[this.items.Count - 1].IsSelected = true;
                }
            }
            else if (this.items.Contains(item))
            //else if(item.Item.GetType() == typeof(Project))
            {
                this.currentPage = item;
                foreach (PageViewModel viewModel in this.items)
                    viewModel.IsSelected = false;
                int index = this.items.IndexOf(item);
                if (index > -1) this.items[index].IsSelected = true;
            }
            else
            {
                this.items.Add(item);
                this.currentPage = item;
                this.items[this.items.Count - 1].IsSelected = true;
            }
            

            System.Diagnostics.Debug.WriteLine("currentPage: " + this.currentPage.ToString());
        }
        public void Remove(PageViewModel item)
        {
            if (this.items.Contains(item))
                this.items.Remove(item);
            if (this.items.Count > 0) this.currentPage = this.items[this.items.Count - 1];
        }
        public void Select(PageViewModel item)
        {
            if (item.Item is DatabaseObject)
            {
                //bool found = false;
                foreach (PageViewModel viewModel in this.Items)
                {
                    if (viewModel.Item == null)
                    {
                        viewModel.IsSelected = false;
                        continue;
                    }

                    if (item.Item.GetType() == viewModel.Item.GetType())
                    {
                        //found = true;
                        viewModel.IsSelected = true;
                        this.currentPage = item;
                    }
                    else
                        viewModel.IsSelected = false;
                }

                //if (!found)
                //{
                //    this.items.Add(item);
                //    this.currentPage = this.items[this.items.Count - 1];
                //    this.items[this.items.Count - 1].IsSelected = true;
                //}
            }
            else if (this.items.Contains(item))
            //else if(item.Item.GetType() == typeof(Project))
            {
                this.currentPage = item;
                foreach (PageViewModel viewModel in this.items)
                    viewModel.IsSelected = false;
                int index = this.items.IndexOf(item);
                if (index > -1) this.items[index].IsSelected = true;
            }
            //else
            //{
            //    this.items.Add(item);
            //    this.currentPage = item;
            //    this.items[this.items.Count - 1].IsSelected = true;
            //}


            System.Diagnostics.Debug.WriteLine("currentPage: " + this.currentPage.ToString());
        }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    /// <summary>
    /// Create a new project.
    /// </summary>
    /// <see>http://shujaatsiddiqi.blogspot.com/2010/07/using-icommand-for-light-weight-views.html</see>
    public class NewProject : ICommand
    {
        private PageManager pageManager;
        public NewProject(PageManager sender)
        {
            this.pageManager = sender;
        }
        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            PageViewModel viewModel = new PageViewModel(new Project());
            this.pageManager.Add(viewModel);
        }
        #endregion
    }
    /// <summary>
    /// Removing a page from tab control.
    /// </summary>
    public class RemovePage : ICommand
    {
        private PageManager manager;
        public RemovePage(PageManager sender)
        {
            this.manager = sender;
        }
        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            if (parameter is PageViewModel)
            {
                this.manager.Remove(parameter as PageViewModel);
                return;
            }

            throw new NotImplementedException("Not recognized parameter type");
        }
        #endregion
    }
    /// <summary>
    /// Select this page.
    /// </summary>
    public class SelectPage : ICommand
    {
        private PageManager manager;
        public SelectPage(PageManager sender)
        {
            this.manager = sender;
        }
        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            if (parameter is PageViewModel)
            {
                this.manager.Select(parameter as PageViewModel);
                return;
            }

            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>
    /// Open inventory page if not exist otherwise bring it to focus.
    /// </summary>
    public class OpenInventory : ICommand
    {
        private PageManager pageManager;
        public OpenInventory(PageManager sender)
        {
            this.pageManager = sender;
        }
        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Retrieve stocks from database.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Stocks stocks = new Stocks();
            stocks = DatabaseObject.LoadFromFile() as Stocks;
            if (stocks == null)
                stocks = new Stocks();
            else
                stocks.Refresh();

            PageViewModel viewModel = new PageViewModel(stocks);
            this.pageManager.Add(viewModel);
        }
        #endregion
    }

    /// <summary>
    /// Open a warehouse maintenance page.
    /// </summary>
    public class OpenWarehouse : ICommand
    {
        private PageManager pageManager;
        public OpenWarehouse(PageManager sender)
        {
            this.pageManager = sender;
        }
        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Retrieve warehouse from database.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Warehouses items = new Warehouses();
            items = DatabaseObject.LoadFromFile() as Warehouses;
            if (items == null)
                items = new Warehouses();
            else
            {
                //stocks.Refresh();
            }

            PageViewModel viewModel = new PageViewModel(items);
            this.pageManager.Add(viewModel);
        }
        #endregion
    }

    /// <summary>
    /// Open a contact maintenance page.
    /// </summary>
    public class OpenContact : ICommand
    {
        private PageManager pageManager;
        public OpenContact(PageManager sender)
        {
            this.pageManager = sender;
        }
        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Retrieve warehouse from database.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Users items = new Users();
            items = DatabaseObject.LoadFromFile() as Users;
            if (items == null)
                items = new Users();
            else
            {
                //stocks.Refresh();
            }

            PageViewModel viewModel = new PageViewModel(items);
            this.pageManager.Add(viewModel);
        }
        #endregion
    }
}