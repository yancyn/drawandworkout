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
    public class PageManager
    {
        #region Properties
        private PageViewModel currentPage;
        public PageViewModel CurrentPage { get { return this.currentPage; } }
        private ObservableCollection<PageViewModel> items;
        public ObservableCollection<PageViewModel> Items { get { return this.items; } set { this.items = value; } }
        private NewProject newProject;
        public NewProject NewProject { get { return this.newProject; } }
        private RemovePage removePage;
        public RemovePage RemovePage { get { return this.removePage; } }
        #endregion

        public PageManager()
        {
            this.items = new ObservableCollection<PageViewModel>();
            this.newProject = new NewProject(this);
            this.removePage = new RemovePage(this);
        }

        #region Methods
        public void Add(PageViewModel item)
        {
            if (!this.items.Contains(item)) this.items.Add(item);
            this.currentPage = item;
        }
        public void Remove(PageViewModel item)
        {
            if (this.items.Contains(item))
                this.items.Remove(item);
            if (this.items.Count > 0) this.currentPage = this.items[this.items.Count - 1];
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

            throw new NotImplementedException();
        }

        #endregion
    }
}