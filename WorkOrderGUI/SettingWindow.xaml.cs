using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HLGranite.Drawing;
using Thought.vCards;

namespace WorkOrderGUI
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();

            string name = WorkOrderGUI.Properties.Settings.Default.vCard;
            if (name.Length == 0)
            {
                vCard vcard = new vCard();
                vcard.DeliveryAddresses.Add(new vCardDeliveryAddress());
                vcard.Phones.Add(new vCardPhone { IsWork = true });
                vcard.Phones.Add(new vCardPhone { IsFax = true });
                vcard.Phones.Add(new vCardPhone { IsCellular = true });
                vcard.Phones.Add(new vCardPhone { IsCellular = true });
                vcard.Phones.Add(new vCardPhone { IsCellular = true });
                vcard.Phones.Add(new vCardPhone { IsCellular = true });
                vcard.Phones.Add(new vCardPhone { IsCellular = true });
                vcard.Phones.Add(new vCardPhone { IsCellular = true });
                vcard.EmailAddresses.Add(new vCardEmailAddress());
                WorkOrderGUI.Properties.Settings.Default.CompanyProfile = vcard;
            }
            else
            {
                string fileName = "Data";
                fileName += System.IO.Path.DirectorySeparatorChar + "Contacts";
                fileName += System.IO.Path.DirectorySeparatorChar + name + ".vcf";
                vCard vcard = User.LoadFromFile(fileName);
                WorkOrderGUI.Properties.Settings.Default.CompanyProfile = vcard;
            }
        }

        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            WorkOrderGUI.Properties.Settings.Default.vCard = WorkOrderGUI.Properties.Settings.Default.CompanyProfile.DisplayName;
            WorkOrderGUI.Properties.Settings.Default.Save();
            Save(WorkOrderGUI.Properties.Settings.Default.CompanyProfile);
        }
        private void Save(vCard vcard)
        {
            User user = new User(vcard);
            user.SaveToFile();
        }
    }
}