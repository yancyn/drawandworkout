using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;

namespace WorkOrderGUI
{
    public class ToolbarViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        #region Properties
        private Shape item;
        public Shape Item
        {
            get { return this.item; }
            set
            {
                if (this.item != null)
                {
                    if (this.item.Equals(value) != true)
                    {
                        this.item = value;
                        this.OnPropertyChanged("Item");
                    }
                }
                else
                {
                    this.item = value;
                    this.OnPropertyChanged("Item");
                }

            }
        }
        private ObservableCollection<ToolbarViewModel> children;
        public ObservableCollection<ToolbarViewModel> Children
        {
            get { return this.children; }
            set
            {
                if (this.children != null)
                {
                    if (this.children.Equals(value) != true)
                    {
                        this.children = value;
                        this.OnPropertyChanged("Children");
                    }
                }
                else
                {
                    this.children = value;
                    this.OnPropertyChanged("Children");
                }
            }
        }
        private ToolbarViewModel parent;
        /// <summary>
        /// Gets or sets parent for this toolbar shape.
        /// </summary>
        public ToolbarViewModel Parent
        {
            get { return this.parent; }
            set
            {
                if (this.parent != null)
                {
                    if (this.parent.Equals(value) != true)
                    {
                        this.parent = value;
                        this.OnPropertyChanged("Parent");
                    }
                }
                else
                {
                    this.parent = value;
                    this.OnPropertyChanged("Parent");
                }
            }
        }
        #endregion

        #region ICommand
        private ReplaceParentToolbarCommand replaceParentToolbarCommand;
        public ReplaceParentToolbarCommand ReplaceParentToolbarCommand { get { return this.replaceParentToolbarCommand; } }
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="shape"></param>
        public ToolbarViewModel(Shape shape)
        {
            this.item = shape;
            this.children = new ObservableCollection<ToolbarViewModel>();
            this.replaceParentToolbarCommand = new ReplaceParentToolbarCommand(this);
        }
        public void ReplaceParent()
        {
            Path path = new Path();
            path.Data = (this.item as Path).Data;
            path.Stroke = Brushes.Black;
            this.parent.Item = path;
        }
        #region INotifyPropertyChanged Members
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = this.PropertyChanged;
            if ((handler != null))
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    public class ReplaceParentToolbarCommand : ICommand
    {
        private ToolbarViewModel manager;
        public ReplaceParentToolbarCommand(ToolbarViewModel sender)
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
            this.manager.ReplaceParent();//parameter as Shape);
            //throw new NotImplementedException();
        }
        #endregion
    }
}