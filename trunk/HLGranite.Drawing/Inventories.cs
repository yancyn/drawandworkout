using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HLGranite.Drawing
{
    public partial class Inventories
    {
        private AddInventoryCommand addInventoryCommand;
        public AddInventoryCommand AddInventoryCommand { get { return this.addInventoryCommand; } }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Inventories()
        {
            //base.fileName = string.Empty;
            this.inventoryField = new ObservableCollection<Inventory>();

            //initialize collection
            string directory = "Data" + Path.DirectorySeparatorChar + "Inventory";
            if (Directory.Exists(directory))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    Guid guid = new Guid(Path.GetFileNameWithoutExtension(file.FullName));
                    Inventory item = new Inventory(guid);
                    this.inventoryField.Add(item);
                }
            }

            this.addInventoryCommand = new AddInventoryCommand(this);
        }
        public void Add(int quantity, Inventory source)
        {
            string prefix = source.Stock.Code + source.Width.ToString() + source.Height.ToString();
            int number = GetLastSerialNumber(prefix, source);
            for (int i = 0; i < quantity; i++)
            {
                number++;

                //save into file
                Inventory item = new Inventory(source);
                item.Serial = prefix + "-" + number.ToString("00000");// GetSerial(i, item);
                item.Save();

                //add into inventories collection
                this.inventoryField.Add(item);


                //assign to stock's inventory collection
                Stock stock = (from f in DatabaseObject.Stocks.Stock
                               where f.Name1.Equals(source.Stock.Name1)
                               select f).First();
                stock.Inventories.Add(item);
            }
        }
        public void Remove(Inventory sender)
        {
            string directory = "Data" + Path.DirectorySeparatorChar + "Inventory";
            string file = directory + Path.DirectorySeparatorChar + sender.Guid.ToString() + ".xml";
            if (File.Exists(file)) File.Delete(file);
        }
        private int GetLastSerialNumber(string prefix, Inventory sender)
        {
            int output = 0;
            if (sender.Stock.Code.Length == 0) return output;
            Inventory result = (from f in this.inventoryField
                                where f.Serial.Contains(prefix)
                                select f)
                                .OrderByDescending(f => f.Serial)
                                .FirstOrDefault();
            if (result != null)
            {
                string lastSerial = result.Serial;
                string lastNumber = lastSerial.TrimStart(prefix.ToCharArray());
                lastNumber = lastNumber.TrimStart(new char[] { '-' });
                output = Convert.ToInt32(lastNumber);
            }

            return output;
        }
    }
    public class AddInventoryCommand : ICommand
    {
        private Inventories manager;
        public AddInventoryCommand(Inventories sender)
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
            if (parameter is InventoryParameters)
            {
                Inventory inventory = new Inventory();
                inventory.Warehouse = (parameter as InventoryParameters).Warehouse;
                inventory.Stock = (parameter as InventoryParameters).Stock;
                inventory.Width = (parameter as InventoryParameters).Width;
                inventory.Height = (parameter as InventoryParameters).Height;
                manager.Add((parameter as InventoryParameters).Quantity, inventory);
                return;
            }

            throw new NotImplementedException("Not supported type of " + parameter.GetType());
        }
        #endregion
    }
    /// <summary>
    /// HACK: Enable multibinding to CommandParameter during call AddInventoryCommand.
    /// </summary>
    /// <see>http://japikse.blogspot.com/2009/07/multibinding-and-command-parameters-in.html</see>
    public class InventoryParameters
    {
        public Warehouse Warehouse { get; set; }
        public DateTime PurchaseAt { get; set; }
        public Stock Stock { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int Quantity { get; set; }
    }
}