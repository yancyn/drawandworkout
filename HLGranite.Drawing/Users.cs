using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Thought.vCards;

namespace HLGranite.Drawing
{
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(Users))]
    public partial class Users
    {
        /// <summary>
        /// Defautl constructor.
        /// </summary>
        public Users()
        {
            fileName = "Users.xml";
            this.userField = new List<User>();
            Load();
        }
        protected void Load()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("Data" + Path.DirectorySeparatorChar + "Contacts");
            foreach (FileInfo f in directoryInfo.GetFiles())
            {
                vCard card = new vCard(f.FullName);
                User user = new User(card);
                this.User.Add(user);
            }
        }
    }
}