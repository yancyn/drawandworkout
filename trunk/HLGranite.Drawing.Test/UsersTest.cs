using System;
using HLGranite.Drawing;
using Thought.vCards;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Drawing.Test
{
    /// <summary>
    ///This is a test class for UsersTest and is intended
    ///to contain all UsersTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UsersTest
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

        public static void CreateVCard(string name)
        {
            Customer target = new Customer();
            target.GivenName = name;
            //target.FamilyName = "Lee";
            target.DisplayName = target.GivenName;// +" " + target.FamilyName;

            vCardDeliveryAddress add1 = new vCardDeliveryAddress();
            add1.Street = "963 Jalan 6";
            add1.City = "Bukit Mertajam";
            add1.PostalCode = "14020";
            add1.Country = "Malaysia";
            target.DeliveryAddresses.Add(add1);

            target.Phones.Add(new vCardPhone("012-4711134"));
            target.SaveToFile();
        }

        /// <summary>
        ///A test for Users Constructor
        ///</summary>
        [TestMethod()]
        public void UsersConstructorTest()
        {
            CreateVCard("Amad");
            CreateVCard("Akau");

            int expected = 2;
            int actual = 0;
            Users target = new Users();
            actual = target.User.Count;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.User[0].GivenName.Length > 0);
        }
    }
}