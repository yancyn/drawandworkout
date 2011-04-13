using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;

namespace HLGranite.Drawing
{
    public partial class Inventories
    {
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
        }
        public void Add(int quantity, Inventory source)
        {
            string prefix = source.Stock.Code + source.Width.ToString() + source.Height.ToString();
            int number = GetLastSerialNumber(prefix, source);
            for (int i = 0; i < quantity; i++)
            {
                number++;

                Inventory item = new Inventory(source);
                item.Serial = prefix + "-" + number.ToString("00000");// GetSerial(i, item);
                item.Save();

                this.inventoryField.Add(item);
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
                                where f.Serial.CompareTo(prefix) >= 0
                                select f).ToList<Inventory>()
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
}