using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HLGranite.Drawing
{
    public partial class WorkOrder
    {
        #region Properties
        private RemoveWorkItemCommand removeWorkItemCommand;
        public RemoveWorkItemCommand RemoveWorkItemCommand { get { return this.removeWorkItemCommand; } }
        private WorkItem selectedItem;
        /// <summary>
        /// Current selected WorkItem for move position use.
        /// </summary>
        public WorkItem SelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.selectedItem != null)
                {
                    if (this.selectedItem.Equals(value) != true)
                    {
                        this.selectedItem = value;
                        this.OnPropertyChanged("SelectedItem");
                    }
                }
                else
                {
                    this.selectedItem = value;
                    this.OnPropertyChanged("SelectedItem");
                }

                if (value != null) this.lastSelectedItem = value;//assume as last selected item as well but not when reset to null case
            }
        }
        private WorkItem lastSelectedItem;
        /// <summary>
        /// Last picked WorkItem for key deletion use.
        /// </summary>
        public WorkItem LastSelectedItem
        {
            get { return this.selectedItem; }
            set
            {
                if (this.lastSelectedItem != null)
                {
                    if (this.selectedItem.Equals(value) != true)
                    {
                        this.lastSelectedItem = value;
                        this.OnPropertyChanged("LastSelectedItem");
                    }
                }
                else
                {
                    this.lastSelectedItem = value;
                    this.OnPropertyChanged("LastSelectedItem");
                }
            }
        }
        #endregion

        public WorkOrder()
        {
            this.guidField = Guid.NewGuid();
            this.itemsField = new ObservableCollection<WorkItem>();
            this.removeWorkItemCommand = new RemoveWorkItemCommand(this);
        }

        #region Methods
        public void RemoveItem()
        {
            if (this.lastSelectedItem != null)
            {
                if (this.Items.Contains(this.lastSelectedItem))
                    this.Items.Remove(this.lastSelectedItem);
            }
        }
        public void RemoveItem(WorkItem sender)
        {
            bool match = false;
            //if (this.Items.Contains(sender))
            for (int i = this.Items.Count - 1; i >= 0; i--)
            {
                if (match) return;
                if (this.Items[i].Guid.Equals(sender.Guid))
                {
                    this.Items.RemoveAt(i);
                    return;
                }
                match = MatchElement(this.Items[i].Elements, sender);
            }
        }
        private bool MatchElement(ObservableCollection<ShapeItem> source, WorkItem sender)
        {
            bool match = false;
            for (int i = source.Count - 1; i >= 0; i--)
            {
                if (match) return true;
                if (source[i] is WorkItem)
                {
                    if ((source[i] as WorkItem).Guid.Equals(sender.Guid))
                    {
                        source.RemoveAt(i);
                        return true;
                    }
                }
                match = MatchElement((source[i] as WorkItem).Elements, sender);
            }

            return match;
        }
        #endregion
    }
    public class RemoveWorkItemCommand : ICommand
    {
        private WorkOrder manager;
        public RemoveWorkItemCommand(WorkOrder sender)
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
            if (parameter is WorkItem)
            {
                this.manager.RemoveItem(parameter as WorkItem);
                return;
            }

            throw new NotImplementedException("Not recognized parameter type");
        }
        #endregion
    }
}