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
        /// <summary>
        /// Parent WorkItem for this child RectItem or EllipseItem.
        /// </summary>
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
        protected InventoryWIP sourceField;
        /// <summary>
        /// Indicate source inventory as raw material.
        /// </summary>
        [XmlIgnore()]
        public InventoryWIP Source
        {
            get { return this.sourceField; }
            set
            {
                if (this.sourceField != null)
                {
                    if (this.sourceField.Equals(value) != true)
                    {
                        this.sourceField = value;
                        this.OnPropertyChanged("Source");
                    }
                }
                else
                {
                    this.sourceField = value;
                    this.OnPropertyChanged("Source");
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
        public override string ToString()
        {
            return this.guidField.ToString();
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
            this.logsField = new ObservableCollection<Log>();
            //this.sourceField = null;
            //this.uomField = (Unit)Enum.Parse(typeof(Unit), "British");
            //todo: this.uomField = (Unit)System.Configuration.ConfigurationSettings.AppSettings["uom"].ToString();// Unit.British;
        }
        /// <summary>
        /// Remove element from own ObservableCollection.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool RemoveElement(WorkItem sender)
        {
            return MatchElement(this.elementsField, sender);
        }
        private bool MatchElement(ObservableCollection<ShapeItem> source, WorkItem sender)
        {
            bool match = false;

            try
            {
                for (int i = source.Count - 1; i >= 0; i--)
                {
                    if (source[i] is WorkItem)
                    {
                        if ((source[i] as WorkItem).Guid.Equals(sender.Guid))
                        {
                            bool isRectItem = (source[i] is RectItem) ? true : false;
                            source.RemoveAt(i);
                            System.Diagnostics.Debug.WriteLine("WorkItem.Elements[i] has been removed.");
                            //remove the dotted line as well
                            //the dotted line must exist before RectItem normally
                            if (isRectItem)
                            {
                                if (i > 0)
                                {
                                    if (source[i - 1] is VerticalLine) source.RemoveAt(i - 1);
                                    else if (source[i - 1] is HorizontalLine) source.RemoveAt(i - 1);
                                }
                            }
                            return true;
                        }

                        match = MatchElement((source[i] as WorkItem).Elements, sender);
                        if (match) return true;
                    }
                }

                return match;
            }
            finally
            {
                int counter = 0;
                foreach (ShapeItem item in this.elementsField)
                    if (item is RectItem) counter++;

                //if there only left 1 RectItem in collection, just remove the other as well
                //the parent will represent the whole size!
                if (counter == 1)
                {
                    for (int i = this.elementsField.Count - 1; i >= 0; i--)
                    {
                        if (this.elementsField[i] is RectItem)
                        {
                            this.elementsField.RemoveAt(i);
                            break;
                        }
                    }
                }

                SetLineSpacing(counter);
            }
        }
        /// <summary>
        /// Configure all the lines spacing after each time addition or deletion of child element.
        /// </summary>
        /// <param name="counter"></param>
        private void SetLineSpacing(int counter)
        {
            int drawingWidth = 200;
            if (this.modelField == "RectItem00")
            {
                foreach (ShapeItem item in this.elementsField)
                {
                    if (item is VerticalLine)
                        (item as VerticalLine).Left = drawingWidth / counter;
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
                if (item is RectItem) counter++;

            if (counter == 0)
            {
                this.elementsField.Add(new RectItem(this.materialField));
                counter++;
            }

            if (this.modelField == "RectItem00")
                this.elementsField.Add(new VerticalLine());
            else
                this.elementsField.Add(new HorizontalLine());
            this.elementsField.Add(sender);
            counter++;

            SetLineSpacing(counter);
        }
        public void AddHorizontalElement(RectItem sender)
        {
            throw new NotImplementedException();
        }
        public void AddVerticalElement(RectItem sender)
        {
            throw new NotImplementedException();
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
                System.Diagnostics.Debug.WriteLine("adding ShapeItem");
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