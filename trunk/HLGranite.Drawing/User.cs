using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Thought.vCards;

namespace HLGranite.Drawing
{
    public partial class User : vCard
    {
        protected static string fileName;
        /// <summary>
        /// Root directory location.
        /// </summary>
        protected static string rootPath = "Data" + Path.DirectorySeparatorChar + "Contacts";

        public User()
        {
            fileName = string.Empty;
            this.Type = UserRole.Employee;
        }
        public User(vCard card)
            : base()
        {
            Copy(card);
        }
        /// <summary>
        /// Cloning vCard object into user object.
        /// </summary>
        /// <param name="item"></param>
        protected void Copy(vCard item)
        {
            //reflection fail due to some public properties no set method
            //System.Reflection.PropertyInfo[] output = this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            //for (int i = 0; i < output.Length; i++)
            //{
            //    try
            //    {
            //        if (item.GetType().GetProperty(output[i].Name) != null)
            //            output[i].SetValue(this, item.GetType().GetProperty(output[i].Name).GetValue(item, null), null);
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.WriteLine(output[i].Name + ": " + ex);
            //    }
            //}

            this.AccessClassification = item.AccessClassification;
            this.AdditionalNames = item.AdditionalNames;
            this.BirthDate = item.BirthDate;
            foreach (string s in item.Categories)
                this.Categories.Add(s);
            foreach (vCardCertificate v in item.Certificates)
                this.Certificates.Add(v);
            foreach (vCardDeliveryAddress v in item.DeliveryAddresses)
                this.DeliveryAddresses.Add(v);
            foreach (vCardDeliveryLabel v in item.DeliveryLabels)
                this.DeliveryLabels.Add(v);
            this.Department = item.Department;
            this.DisplayName = item.DisplayName;
            foreach (vCardEmailAddress v in item.EmailAddresses)
                this.EmailAddresses.Add(v);
            this.FamilyName = item.FamilyName;
            this.FormattedName = item.FormattedName;
            this.Gender = item.Gender;
            this.GivenName = item.GivenName;
            this.Latitude = item.Latitude;
            this.Longitude = item.Longitude;
            this.Mailer = item.Mailer;
            this.NamePrefix = item.NamePrefix;
            this.NameSuffix = item.NameSuffix;
            foreach (string s in item.Nicknames)
                this.Nicknames.Add(s);
            foreach (vCardNote v in item.Notes)
                this.Notes.Add(v);
            this.Office = item.Office;
            this.Organization = item.Organization;
            foreach (vCardPhone v in item.Phones)
                this.Phones.Add(v);
            foreach (vCardPhoto v in item.Photos)
                this.Photos.Add(v);
            this.ProductId = item.ProductId;
            this.RevisionDate = item.RevisionDate;
            this.Role = item.Role;
            foreach (vCardSource v in item.Sources)
                this.Sources.Add(v);
            this.TimeZone = item.TimeZone;
            this.Title = item.Title;
            this.UniqueId = item.UniqueId;
            foreach (vCardWebsite url in item.Websites)
                this.Websites.Add(url);
        }
        public virtual void SaveToFile()
        {
            fileName = this.DisplayName;//key: set file name for vCard.
            if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);
            string path = rootPath + Path.DirectorySeparatorChar + fileName + ".vcf";

            //saving vCard            
            vCardStandardWriter writer = new vCardStandardWriter();
            writer.EmbedInternetImages = false;
            writer.EmbedLocalImages = true;
            writer.Options = vCardStandardWriterOptions.IgnoreCommas;
            writer.Write(this as vCard, path);
        }
        public static vCard LoadFromFile(string fileName)
        {
            return new vCard(fileName);
            //return new User(card);
        }
    }
}