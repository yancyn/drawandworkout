using System.ComponentModel;
using System.Configuration;
using HLGranite.Drawing;
using Thought.vCards;

namespace WorkOrderGUI.Properties
{
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    public sealed partial class Settings
    {
        public Settings()
        {
            System.Diagnostics.Debug.WriteLine("initialize Settings");
            if (this.vCard.Length == 0)
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
                this.CompanyProfile = vcard;
            }
            else
            {
                string fileName = "Data";
                fileName += System.IO.Path.DirectorySeparatorChar + "Contacts";
                fileName += System.IO.Path.DirectorySeparatorChar + this.vCard + ".vcf";
                vCard vcard = User.LoadFromFile(fileName);
                this.CompanyProfile = vcard;
            }
        }
        /// <summary>
        /// Company profile.
        /// </summary>
        //public vCard CompanyProfile
        //{
        //    get
        //    {
        //        return ((vCard)(this["CompanyProfile"]));
        //    }
        //    set
        //    {
        //        this["CompanyProfile"] = value;
        //        this.OnPropertyChanged(this, new PropertyChangedEventArgs("CompanyProfile"));
        //    }
        //}
        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
        {
            // Add code to handle the SettingChangingEvent event here.
        }
        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
        {
            // Add code to handle the SettingsSaving event here.
        }
        public override void Save()
        {
            this.vCard = this.CompanyProfile.DisplayName;
            base.Save();

            System.Diagnostics.Debug.WriteLine("saving company vcard");
            User user = new User(this.CompanyProfile);
            user.SaveToFile();
        }
    }
}