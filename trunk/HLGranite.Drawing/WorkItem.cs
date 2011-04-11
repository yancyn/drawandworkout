using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Windows.Input;

namespace HLGranite.Drawing
{
    public partial class WorkItem
    {
        #region Properties
        protected int numberOfSides;
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

        #region ICommand
        private AddElementCommand addElementCommand;
        /// <summary>
        /// Add a new element or split into a new RectItem.
        /// </summary>
        public AddElementCommand AddElementCommand { get { return this.addElementCommand; } }
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
        /// New constructor.
        /// </summary>
        public WorkItem(string model)
            : base(model)
        {
            Initialize();
        }
        public WorkItem(Stock stock)
            : base()
        {
            Initialize();
            this.materialField = stock;
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

        #region Methods
        private void Initialize()
        {
            this.addElementCommand = new AddElementCommand(this);

            this.elementsField = new ObservableCollection<ShapeItem>();
            this.elementsField.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(elementsField_CollectionChanged);
            this.lengthsField = new ObservableCollection<LengthItem>();
            this.lengthsField.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(lengthsField_CollectionChanged);
            this.materialField = new Stock();
            this.parentField = null;
            this.progressField = 0;
            this.workedByField = new Employee();
            this.numberOfSides = 1;
            this.statusField = WorkStatus.NotStarted;
            //this.uomField = (Unit)Enum.Parse(typeof(Unit), "British");
            //todo: this.uomField = (Unit)System.Configuration.ConfigurationSettings.AppSettings["uom"].ToString();// Unit.British;
        }
        #endregion

        #region Events
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
            //System.Diagnostics.Debug.WriteLine("Type_PropertyChanged");
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
    public class AddElementCommand : ICommand
    {
        private WorkItem manager;
        public AddElementCommand(WorkItem sender)
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
            //if (manager.Parent != null)
            manager.Elements.Add(new RectItem(manager.Material));
        }
        #endregion
    }
}