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
        public void AddElement()
        {
            AddElement(new RectItem(this.materialField));
        }
        /// <summary>
        /// Add a new RectItem and separate with a dotted line in drawing.
        /// </summary>
        /// <param name="sender"></param>
        public void AddElement(RectItem sender)
        {
            //if it is a first RectItem then have to create 2
            //otherwise just create additional 1
            int counter = 0;
            foreach (ShapeItem item in this.elementsField)
            {
                if (item is RectItem) counter++;
            }

            if (counter == 0)
            {
                this.elementsField.Add(new RectItem(this.materialField));
                counter++;
            }

            this.elementsField.Add(new VerticalLine());
            this.elementsField.Add(sender);
            counter++;

            int drawingWidth = 200;
            int drawingHeight = 80;
            int countLine = 0;
            double buffer = 24;
            if (this.modelField == "RectItem00")
            {
                foreach (ShapeItem item in this.elementsField)
                {
                    if (item is VerticalLine)
                    {
                        //(item as VerticalLine).Left = countLine * drawingWidth / counter;
                        (item as VerticalLine).Left = drawingWidth / counter;
                        //(item as VerticalLine).Top = -countLine * drawingHeight / counter +buffer;
                    }
                }
            }
            else if (this.modelField == "RectItem01")
            {
                foreach (ShapeItem item in this.elementsField)
                {
                    if (item is HorizontalLine)
                        (item as HorizontalLine).Top = drawingWidth / counter;
                }
            }
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
            manager.AddElement();
        }
        #endregion
    }
}