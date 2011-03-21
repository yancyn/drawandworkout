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
        public PageViewModel CurrentPage { get { return this.currentPage; } set { this.currentPage = value; } }
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
        private NewProject newProject;
        public NewProject NewProject { get { return this.newProject; } }
        private RemovePage removePage;
        public RemovePage RemovePage { get { return this.removePage; } }
        private OpenInventory openInventory;
        public OpenInventory OpenInventory { get { return this.openInventory; } }
        #endregion

        public PageManager()
        {
            this.items = new ObservableCollection<PageViewModel>();
            this.newProject = new NewProject(this);
            this.removePage = new RemovePage(this);
            this.openInventory = new OpenInventory(this);
        }

        #region Methods
        public void Add(PageViewModel item)
        {
            if (item.Item is DatabaseObject)
            {
                bool found = false;
                foreach (PageViewModel viewModel in this.Items)
                {
                    if (item.Item.GetType() == viewModel.Item.GetType())
                    {
                        found = true;
                        this.currentPage = item;
                    }
                }

                if (!found)
                {
                    this.items.Add(item);
                    this.currentPage = item;
                }
            }
            else if (this.items.Contains(item))//todo: overwrite Contains with key checking on Guid instead of whole memory stack
            {
                this.currentPage = item;
            }
            else
            {
                this.items.Add(item);
                this.currentPage = item;
            }
        }
        public void Remove(PageViewModel item)
        {
            if (this.items.Contains(item))
                this.items.Remove(item);
            if (this.items.Count > 0) this.currentPage = this.items[this.items.Count - 1];
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
            stocks.Refresh();

            PageViewModel viewModel = new PageViewModel(stocks);
            this.pageManager.Add(viewModel);
        }
        #endregion
    }
}