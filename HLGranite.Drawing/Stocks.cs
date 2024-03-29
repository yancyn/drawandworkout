﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HLGranite.Drawing
{
    public partial class Stocks
    {
        #region ICommand
        private NewStock newStock;
        /// <summary>
        /// ICommand to create a new stock.
        /// </summary>
        public NewStock NewStock { get { return this.newStock; } }
        private RemoveStock removeStock;
        /// <summary>
        /// ICommand to remove a stock from collection.
        /// </summary>
        public RemoveStock RemoveStock { get { return this.removeStock; } }
        private SaveStock saveStock;
        public SaveStock SaveStock { get { return this.saveStock; } }
        private RefreshStock refreshStock;
        public RefreshStock RefreshStock { get { return this.refreshStock; } }
        #endregion

        public Stocks()
        {
            base.fileName = "Stocks.xml";
            this.stockField = new ObservableCollection<Stock>();
            this.newStock = new NewStock(this);
            this.removeStock = new RemoveStock(this);
            this.saveStock = new SaveStock(this);
            this.refreshStock = new RefreshStock(this);
            //Initialize();//will cause stack overflow
        }

        #region Methods
        //protected void Initialize()
        //{
        //    if (DatabaseObject.LoadFromFile() != null)
        //        this.stockField = (DatabaseObject.LoadFromFile() as Stocks).Stock;
        //}
        public void Add()
        {
            this.stockField.Add(new Stock());
        }
        public void Remove(Stock item)
        {
            if (item != null && this.stockField.Contains(item))
                this.stockField.Remove(item);
        }
        /// <summary>
        /// Discard all the changes and read again from database file.
        /// </summary>
        public void Refresh()
        {
            //DatabaseObject.Stocks.Stock
            //sort collection by name1 alphabetically
            Stocks target = new Stocks();
            target = target.LoadFromFile() as Stocks;
            if (target != null)
            {
                var hold = from f in target.Stock select f;
                List<Stock> temp = hold.OrderBy(f => f.Name1).ToList();
                if (temp.Count > 0) this.stockField.Clear();//tips: don't use new it will break all the binding = new ObservableCollection<Stock>();
                foreach (Stock s in temp)
                    this.stockField.Add(s);
            }
        }
        /// <summary>
        /// Load from database.
        /// </summary>
        /// <returns></returns>
        public DatabaseObject LoadFromFile()
        {
            Stocks output = base.LoadFromFile() as Stocks;
            foreach (Stock item in output.Stock)
            {
                var inventories = from f in DatabaseObject.Inventories.Inventory
                                  where f.Stock.Name1.Equals(item.Name1)
                                  select f;
                foreach (Inventory i in inventories)
                    item.Inventories.Add(i);
            }

            return output;
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    /// <see>http://blogs.msdn.com/b/mikehillberg/archive/2009/03/20/icommand-is-like-a-chocolate-cake.aspx</see>
    public class NewStock : ICommand
    {
        private Stocks manager;
        public NewStock(Stocks sender)
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
            this.manager.Add();
        }
        #endregion
    }
    public class RemoveStock : ICommand //,INotification
    {
        private Stocks manager;
        public RemoveStock(Stocks sender)
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
            if (parameter == null) return;
            if (parameter is Stock)
            {
                this.manager.Remove((parameter as Stock));
                return;
            }

            throw new NotImplementedException("Not recognized parameter type");
        }
        #endregion
    }
    public class SaveStock : ICommand
    {
        private Stocks manager;
        public SaveStock(Stocks sender)
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
            this.manager.SaveToFile();
        }
        #endregion
    }
    public class RefreshStock : ICommand
    {
        private Stocks manager;
        public RefreshStock(Stocks sender)
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
            this.manager.Refresh();
        }
        #endregion
    }
}