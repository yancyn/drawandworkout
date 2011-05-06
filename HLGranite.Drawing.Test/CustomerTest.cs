using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using HLGranite.Drawing;
using Thought.vCards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLGranite.Drawing.Test
{
    /// <summary>
    ///This is a test class for CustomerTest and is intended
    ///to contain all CustomerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CustomerTest
    {
        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for Customer Constructor
        ///</summary>
        [TestMethod()]
        public void SaveToFileTest()
        {
            //Users users = new Users();
            Customer target = new Customer();
            target.GivenName = "John" + new Random().Next(200);
            target.FamilyName = "Lee";
            target.DisplayName = target.GivenName + " " + target.FamilyName;

            vCardDeliveryAddress add1 = new vCardDeliveryAddress();
            add1.Street = "963 Jalan 6";
            add1.City = "Bukit Mertajam";
            add1.PostalCode = "14020";
            add1.Country = "Malaysia";
            target.DeliveryAddresses.Add(add1);

            target.Phones.Add(new vCardPhone("012-4711134"));
            target.SaveToFile();
            //(target as User).SaveToFile();

            //users.User.Add(target);
            //users.SaveToFile();
        }
        /// <summary>
        /// A test on load vCard into customer object.
        /// </summary>
        [TestMethod()]
        public void LoadToFileTest()
        {
            //creating some test data
            string name = "John" + new Random().Next(200);
            UsersTest.CreateVCard(name);

            name = "Ali" + new Random().Next(200);
            UsersTest.CreateVCard(name);

            //string fileName = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "Data";
            fileName += Path.DirectorySeparatorChar + "Contacts";
            fileName += Path.DirectorySeparatorChar + name + ".vcf";
            vCard card = Customer.LoadFromFile(fileName);
            Customer target = new Customer(card);
            Assert.IsTrue(target.GivenName.Length > 0);
        }
        [TestMethod()]
        public void WriteNameListTest()
        {
            List<string> target = new List<string>();
            target.Add("Ali");
            target.Add("Ahmad");
            SaveToFile("Customers.xml", target);
        }
        /// <summary>
        /// Load customer name list only.
        /// </summary>
        [TestMethod()]
        public void LoadNameListTest()
        {
            List<string> expected = new List<string>();
            expected.Add("Ali");
            expected.Add("Ahmad");

            List<string> actual = LoadFromFile(@"G:\My Projects\DrawAndWorkout\WorkOrderGUI\bin\Debug\Data\Customers.xml", Encoding.UTF8);
            CollectionAssert.AreEqual(expected, actual);
        }

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
        public List<string> LoadFromFile(string fileName, System.Text.Encoding encoding)
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

                return DeserializeStringList(xmlString);
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
    }
}