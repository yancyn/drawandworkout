using System;
using System.Text;
using System.IO;
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
            fileName += Path.DirectorySeparatorChar + name+".vcf";
            vCard card = Customer.LoadFromFile(fileName);
            Customer target = new Customer(card);
            Assert.IsTrue(target.GivenName.Length > 0);
        }
    }
}