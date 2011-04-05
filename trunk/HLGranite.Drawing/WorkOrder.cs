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
        private RemoveWorkItemCommand removeWorkItemCommand;
        public RemoveWorkItemCommand RemoveWorkItemCommand { get { return this.removeWorkItemCommand; } }
        public WorkOrder()
        {
            this.guidField = Guid.NewGuid();
            this.itemsField = new ObservableCollection<WorkItem>();
            this.removeWorkItemCommand = new RemoveWorkItemCommand(this);
        }
        public void RemoveItem(WorkItem item)
        {
            if (this.Items.Contains(item))
                this.Items.Remove(item);
        }
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