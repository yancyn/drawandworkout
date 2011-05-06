using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.ObjectModel;
using Thought.vCards;
using System.Windows.Input;

namespace HLGranite.Drawing
{
    //[System.Xml.Serialization.XmlIncludeAttribute(typeof(Users))]
    public partial class Users
    {
        #region Properties
        private ObservableCollection<Customer> agentField;
        /// <summary>
        /// Gets or sets customer collection.
        /// </summary>
        public ObservableCollection<Customer> Agent
        {
            get { return this.agentField; }
            set
            {
                if (this.agentField != null)
                {
                    if (this.agentField.Equals(value) != true)
                    {
                        this.agentField = value;
                        this.OnPropertyChanged("Agent");
                    }
                }
                else
                {
                    this.agentField = value;
                    this.OnPropertyChanged("Agent");
                }
            }
        }
        #endregion

        #region ICommand
        private NewUser newUser;
        public NewUser NewUser { get { return this.newUser; } }
        private RemoveUser removeUser;
        public RemoveUser RemoveUser { get { return this.removeUser; } }
        private SaveUser saveUser;
        public SaveUser SaveUser { get { return this.saveUser; } }
        private RefreshUser refreshUser;
        public RefreshUser RefreshUser { get { return this.refreshUser; } }
        #endregion

        /// <summary>
        /// Defautl constructor.
        /// </summary>
        public Users()
        {
            base.fileName = "Users.xml";
            this.userField = new ObservableCollection<User>();
            this.agentField = new ObservableCollection<Customer>();
            this.newUser = new NewUser(this);
            this.removeUser = new RemoveUser(this);
            this.saveUser = new SaveUser(this);
            this.refreshUser = new RefreshUser(this);
            Load();
        }

        #region Methods
        public void Add()
        {
            this.userField.Add(new User());
        }
        public void Remove(User item)
        {
            if (item != null && this.userField.Contains(item))
            {
                this.userField.Remove(item);

                string path = "Data" + Path.DirectorySeparatorChar + "Contacts" + Path.DirectorySeparatorChar + item.DisplayName + ".vcf";
                if (File.Exists(path)) File.Delete(path);
            }
        }
        public void Refresh()
        {
            Users target = new Users();
            target.Load();// as Stocks;
            if (target == null) return;

            var hold = from f in target.User select f;
            List<User> temp = hold.OrderBy(f => f.DisplayName).ToList();
            if (temp.Count > 0) this.userField.Clear();//tips: don't use new it will break all the binding = new ObservableCollection<Stock>();
            foreach (User item in temp)
                this.userField.Add(item);
        }
        public void Load()
        {
            string directory = "Data" + Path.DirectorySeparatorChar + "Contacts";
            if (!Directory.Exists(directory)) return;

            List<string> agents = new List<string>();
            LoadFromFile("Data" + Path.DirectorySeparatorChar + "Agents.xml", Encoding.UTF8, out agents);

            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            foreach (FileInfo f in directoryInfo.GetFiles())
            {
                string fileNameOnly = Path.GetFileNameWithoutExtension(f.FullName);
                vCard card = new vCard(f.FullName);
                User user = null;
                user = new User(card);
                if (agents.Contains(fileNameOnly))
                {
                    user = new Customer(card);
                    user.Type = UserRole.Agent;
                    this.agentField.Add(user as Customer);
                }
                else
                    user = new User(card);
                this.userField.Add(user);
            }
        }
        public override void SaveToFile()
        {
            foreach (User item in this.userField)
                item.SaveToFile();
        }
        #endregion
    }
    public class NewUser : ICommand
    {
        private Users manager;
        public NewUser(Users sender)
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
            this.manager.Add();
        }
        #endregion
    }
    public class RemoveUser : ICommand
    {
        private Users manager;
        public RemoveUser(Users sender)
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
            if (parameter is User)
            {
                this.manager.Remove(parameter as User);
                return;
            }

            throw new NotImplementedException("Not recognized parameter type");
        }
        #endregion
    }
    public class SaveUser : ICommand
    {
        private Users manager;
        public SaveUser(Users sender)
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
            this.manager.SaveToFile();
        }
        #endregion
    }
    public class RefreshUser : ICommand
    {
        private Users manager;
        public RefreshUser(Users sender)
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
            this.manager.Refresh();
        }
        #endregion
    }
}