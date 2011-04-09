using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WorkOrderGUI
{
    public class BullnoseViewModel
    {
        private string model;
        public string Model { get { return this.model; } set { this.model = value; } }
        private string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        private StackPanel content;
        public StackPanel Content { get { return this.content; } set { this.content = value; } }
        public BullnoseViewModel(string model, string name, StackPanel stackPanel)
        {
            this.model = model;
            this.name = name;
            this.content = stackPanel;
        }
    }
}