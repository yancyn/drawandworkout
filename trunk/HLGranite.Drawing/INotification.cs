using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HLGranite.Drawing
{
    /// <summary>
    /// No use temp.
    /// </summary>
    public interface INotification
    {
        void NotifyUserOfFailure(string message);
        bool ConfirmUserAction(string message);
    }
}