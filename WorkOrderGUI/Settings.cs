using System.ComponentModel;
using System.Configuration;
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
        ///// <summary>
        ///// Company profile.
        ///// </summary>
        //public vCard CompanyProfile
        //{
        //    get { return ((vCard)(this["CompanyProfile"])); }
        //    set { this["CompanyProfile"] = value; }
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
            base.Save();
            System.Diagnostics.Debug.WriteLine("saving company vcard");
        }
    }
}