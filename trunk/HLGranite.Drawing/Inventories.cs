using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;

namespace HLGranite.Drawing
{
    public partial class Inventories
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Inventories()
        {
            fileName = "Inventories.xml";
            this.inventoryField = new ObservableCollection<Inventory>();
        }
        public void Add(Inventory source, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                Inventory item = new Inventory(source);
                this.inventoryField.Add(item);
            }
        }

        #region Serialize/Deserialize
        private static System.Xml.Serialization.XmlSerializer serializer;
        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(Inventories));
                }
                return serializer;
            }
        }

        /// <summary>
        /// Serializes current Inventories object into an XML document
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw ex;
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
        /// Deserializes workflow markup into an Inventories object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output Inventories object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out Inventories obj, out System.Exception exception)
        {
            exception = null;
            obj = default(Inventories);
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
        public static bool Deserialize(string xml, out Inventories obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }
        public static Inventories Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((Inventories)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
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
        /// Serializes current Inventories object into file
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
        /// Deserializes xml markup from file into an Inventories object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output Inventories object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out Inventories obj, out System.Exception exception)
        {
            exception = null;
            obj = default(Inventories);
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
        public static bool LoadFromFile(string fileName, out Inventories obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }
        public static bool LoadFromFile(string fileName, out Inventories obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }
        public static Inventories LoadFromFile()
        {
            return LoadFromFile(fileName, Encoding.UTF8);
        }
        public static Inventories LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
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
        #endregion
    }
}