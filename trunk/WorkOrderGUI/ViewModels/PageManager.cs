using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WorkOrderGUI
{
    public class PageManager
    {
        private PageViewModel currentPage;
        public PageViewModel CurrentPage { get { return this.currentPage; } }
        private ObservableCollection<PageViewModel> items;
        public ObservableCollection<PageViewModel> Items { get { return this.items; } set { this.items = value; } }

        public PageManager()
        {
            this.items = new ObservableCollection<PageViewModel>();
        }
        public void Add(PageViewModel item)
        {
            if (!this.items.Contains(item)) this.items.Add(item);
            this.currentPage = item;
        }
    }
}