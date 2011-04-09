using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Expression.Interactivity;

namespace WorkOrderGUI
{
    /// <summary>
    /// Prompt a confirmation box before deletion made.
    /// </summary>
    /// <see>http://tombeeby.com/2009/07/09/getting-confirmation-on-an-icommand/</see>
    public class ConfirmCommandAction : TriggerAction<FrameworkElement>
    {
        private string message;
        public string Message
        {
            get { return message ?? "Are you sure you want to do that?"; }
            set { message = value; }
        }
        protected override void Invoke(object parameter)
        {
            if (AssociatedObject.Tag is ICommand)
            {
                var cmnd = AssociatedObject.Tag as ICommand;
                if (cmnd != null && cmnd.CanExecute(AssociatedObject.DataContext))
                {
                    if (MessageBox.Show(this.Message, "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Information)
                        == MessageBoxResult.Yes) //if (HtmlPage.Window.Confirm(Message))
                    {
                        if ((AssociatedObject as System.Windows.Controls.Button).CommandParameter != null)
                            cmnd.Execute((AssociatedObject as System.Windows.Controls.Button).CommandParameter);
                        else
                            cmnd.Execute(AssociatedObject.DataContext);
                    }
                }
            }
        }
    }
}