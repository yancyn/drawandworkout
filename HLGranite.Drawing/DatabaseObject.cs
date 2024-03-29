﻿using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HLGranite.Drawing
{
    /// <summary>
    /// Database object core class.
    /// </summary>
    /// <remarks>
    /// Refer to <see>Xsd2Code</see>.
    /// </remarks>
    public partial class DatabaseObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the database location to stored.
        /// </summary>
        protected string fileName;

        private static Users users;
        /// <summary>
        /// Gets users collection from a stored database.
        /// </summary>
        public static Users Users
        {
            get
            {
                if (users == null)
                {
                    users = new Users();
                    users = users.LoadFromFile() as Users;
                    if (users == null) users = new Users();
                }

                return users;
            }
        }
        private static Stocks stocks;
        /// <summary>
        /// Gets stocks collection from a stored database.
        /// </summary>
        public static Stocks Stocks
        {
            get
            {
                if (stocks == null)
                {
                    stocks = new Stocks();
                    stocks = stocks.LoadFromFile() as Stocks;
                    if (stocks == null) stocks = new Stocks();

                    //sort collection by name1 alphabetically
                    /*Stocks target = new Stocks();
                    target = DatabaseObject.LoadFromFile() as Stocks;
                    if (target != null)
                    {
                        var hold = from f in target.Stock select f;
                        List<Stock> temp = hold.OrderBy(f => f.Name1).ToList();
                        //if (temp.Count > 0) this.stockField.Clear();//tips: don't use new it will break all the binding = new ObservableCollection<Stock>();
                        foreach (Stock s in temp)
                            stocks.Stock.Add(s);
                    }
                    else
                        stocks = new Stocks();
                    */
                }

                return stocks;
            }
        }
        private static Inventories inventories;
        /// <summary>
        /// Gets inventories collection from a stored database.
        /// </summary>
        public static Inventories Inventories
        {
            get
            {
                if (inventories == null)
                {
                    inventories = new Inventories();
                    inventories = inventories.LoadFromFile() as Inventories;
                    if (inventories == null) inventories = new Inventories();
                }

                return inventories;
            }
        }
        private static Warehouses warehouses;
        /// <summary>
        /// Gets warehouses collection from a stored database.
        /// </summary>
        public static Warehouses Warehouses
        {
            get
            {
                if (warehouses == null)
                {
                    warehouses = new Warehouses();
                    warehouses = warehouses.LoadFromFile() as Warehouses;
                    if (warehouses == null) warehouses = new Warehouses();
                }

                return warehouses;
            }
        }

        private static Projects projects;
        /// <summary>
        /// Gets project collection from a stored database.
        /// </summary>
        public static Projects Projects
        {
            get
            {
                if (projects == null) projects = new Projects();
                return projects;
            }
        }
        #endregion

        #region Serialize/Deserialize
        private static System.Xml.Serialization.XmlSerializer serializer;
        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(DatabaseObject));
                }
                return serializer;
            }
        }

        /// <summary>
        /// Serializes current DatabaseObject object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public virtual string Serialize(System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, this);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }
        public virtual string Serialize()
        {
            return Serialize(Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an DatabaseObject object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output DatabaseObject object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out DatabaseObject obj, out System.Exception exception)
        {
            exception = null;
            obj = default(DatabaseObject);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }
        public static bool Deserialize(string xml, out DatabaseObject obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }
        public static DatabaseObject Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((DatabaseObject)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Serializes current DatabaseObject object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }
        public virtual bool SaveToFile(string fileName, out System.Exception exception)
        {
            return SaveToFile(fileName, Encoding.UTF8, out exception);
        }
        public virtual void SaveToFile()
        {
            SaveToFile(fileName, Encoding.UTF8);
        }
        public virtual void SaveToFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(encoding);

                string path = "Data";
                if (this.GetType() == typeof(Project)) //todo: test save projects scenario
                    path += Path.DirectorySeparatorChar + "Projects";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += Path.DirectorySeparatorChar + fileName;

                streamWriter = new System.IO.StreamWriter(path, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an DatabaseObject object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output DatabaseObject object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        protected bool LoadFromFile(string fileName, System.Text.Encoding encoding, out DatabaseObject obj, out System.Exception exception)
        {
            exception = null;
            obj = default(DatabaseObject);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }
        protected bool LoadFromFile(string fileName, out DatabaseObject obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }
        protected bool LoadFromFile(string fileName, out DatabaseObject obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }
        public DatabaseObject LoadFromFile()
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }
        public DatabaseObject LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                string path = "Data";
                //if(fileName.ToLower().Contains("project")) //todo: how to handle if accidently save a project object?
                //    path += Path.DirectorySeparatorChar + "Projects";
                path += Path.DirectorySeparatorChar + fileName;
                if (!File.Exists(path)) return null;

                file = new System.IO.FileStream(path, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }

        //for serialize and deserialize string array use ie. Agent list.
        public List<string> DeserializeStringList(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                return ((List<string>)(s.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally { if (stringReader != null) stringReader.Dispose(); }
        }
        public void LoadFromFile(string fileName, System.Text.Encoding encoding, out List<string> output)
        {
            output = new List<string>();
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();

                output = DeserializeStringList(xmlString);
            }
            finally
            {
                if (file != null) file.Dispose();
                if (sr != null) sr.Dispose();
            }
        }
        public virtual void SaveToFile(string fileName, object sender)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(Encoding.UTF8, sender);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
            }
            finally { if (streamWriter != null) streamWriter.Dispose(); }
        }
        public virtual string Serialize(System.Text.Encoding encoding, object sender)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(sender.GetType());
                s.Serialize(xmlWriter, sender);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if (streamReader != null) streamReader.Dispose();
                if (memoryStream != null) memoryStream.Dispose();
            }
        }
        #endregion
    }
}