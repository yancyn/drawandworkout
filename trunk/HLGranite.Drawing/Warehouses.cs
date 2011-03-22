using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HLGranite.Drawing
{
    public partial class Warehouses
    {
        #region ICommand
        private NewWarehouse newWarehouse;
        /// <summary>
        /// ICommand to create a new Warehouse.
        /// </summary>
        public NewWarehouse NewWarehouse { get { return this.newWarehouse; } }
        private RemoveWarehouse removeWarehouse;
        /// <summary>
        /// ICommand to remove a Warehouse from collection.
        /// </summary>
        public RemoveWarehouse RemoveWarehouse { get { return this.removeWarehouse; } }
        private SaveWarehouse saveWarehouse;
        public SaveWarehouse SaveWarehouse { get { return this.saveWarehouse; } }
        private RefreshWarehouse refreshWarehouse;
        public RefreshWarehouse RefreshWarehouse { get { return this.refreshWarehouse; } }
        #endregion

        public Warehouses()
        {
            fileName = "Warehouses.xml";
            this.warehouseField = new ObservableCollection<Warehouse>();
            this.newWarehouse = new NewWarehouse(this);
            this.removeWarehouse = new RemoveWarehouse(this);
            this.saveWarehouse = new SaveWarehouse(this);
            this.refreshWarehouse = new RefreshWarehouse(this);
        }

        #region Methods
        public void Add()
        {
            this.warehouseField.Add(new Warehouse());
        }
        public void Remove(Warehouse item)
        {
            if (item != null && this.warehouseField.Contains(item))
                this.warehouseField.Remove(item);
        }
        /// <summary>
        /// Discard all the changes and read again from database file.
        /// </summary>
        public void Refresh()
        {
            //sort collection by name1 alphabetically
            Warehouses target = new Warehouses();
            target = DatabaseObject.LoadFromFile() as Warehouses;
            var hold = from f in target.Warehouse select f;
            List<Warehouse> temp = hold.OrderBy(f => f.Name1).ToList();

            if (temp.Count > 0) this.warehouseField.Clear();//tips: don't use new it will break all the binding = new ObservableCollection<Warehouse>();
            foreach (Warehouse s in temp)
                this.warehouseField.Add(s);
        }
        #endregion
    }

    public class NewWarehouse : ICommand
    {
        private Warehouses manager;
        public NewWarehouse(Warehouses sender)
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
    public class RemoveWarehouse : ICommand
    {
        private Warehouses manager;
        public RemoveWarehouse(Warehouses sender)
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
            if (parameter is Warehouse)
            {
                this.manager.Remove((parameter as Warehouse));
                return;
            }

            throw new NotImplementedException("Not recognized parameter type");
        }
        #endregion
    }
    public class SaveWarehouse : ICommand
    {
        private Warehouses manager;
        public SaveWarehouse(Warehouses sender)
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
    public class RefreshWarehouse : ICommand
    {
        private Warehouses manager;
        public RefreshWarehouse(Warehouses sender)
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