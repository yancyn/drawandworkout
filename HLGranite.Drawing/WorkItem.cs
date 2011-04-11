using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace HLGranite.Drawing
{
    public partial class WorkItem
    {
        #region Properties
        protected WorkItem parentField;
        [XmlIgnore()]
        public WorkItem Parent
        {
            get
            {
                return this.parentField;
            }
            set
            {
                if ((this.parentField != null))
                {
                    if ((parentField.Equals(value) != true))
                    {
                        this.parentField = value;
                        this.OnPropertyChanged("Parent");
                    }
                }
                else
                {
                    this.parentField = value;
                    this.OnPropertyChanged("Parent");
                }
            }
        }
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WorkItem()
            : base()
        {
            Initialize();
        }
        /// <summary>
        /// Recommended constructor.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stock"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public WorkItem(string model, Stock stock, double width, double height)
            : base(width, height)
        {
            Initialize();
            this.modelField = model;
            this.materialField = stock;
        }

        #region Methods & Events
        private void Initialize()
        {
            this.elementsField = new ObservableCollection<ShapeItem>();
            this.elementsField.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(elementsField_CollectionChanged);
            this.lengthsField = new ObservableCollection<LengthItem>();
            this.lengthsField.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(lengthsField_CollectionChanged);
            this.leftField = 0;
            this.materialField = new Stock();
            this.modelField = string.Empty;
            this.parentField = null;
            this.progressField = 0;
            this.topField = 0;
            this.workedByField = new Employee();
            //this.uomField = (Unit)Enum.Parse(typeof(Unit), "British");
            //todo: this.uomField = (Unit)System.Configuration.ConfigurationSettings.AppSettings["uom"].ToString();// Unit.British;
        }

        protected void lengthsField_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int count = (sender as ObservableCollection<LengthItem>).Count;
                ((sender as ObservableCollection<LengthItem>)[count - 1] as LengthItem).PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Type_PropertyChanged);
            }
        }
        private void elementsField_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                int count = (sender as ObservableCollection<ShapeItem>).Count;
                if ((sender as ObservableCollection<ShapeItem>)[count - 1] is WorkItem)
                {
                    ((sender as ObservableCollection<ShapeItem>)[count - 1] as WorkItem).Parent = this;
                }
            }
        }
        protected void Type_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Type_PropertyChanged");
            LengthItem source = (sender as LengthItem);
            if (source.Type.Model.Length > 0)
            {
                bool start = false;
                foreach (LengthItem item in this.lengthsField)
                {
                    if (start) item.Type = source.Type;
                    if (item.Length == source.Length)//todo: stronger checking for LengthItem collection
                        start = true;
                }
            }
        }
        #endregion
    }
}