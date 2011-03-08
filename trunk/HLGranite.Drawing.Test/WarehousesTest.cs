using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Thought.vCards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLGranite.Drawing.Test
{
    /// <summary>
    /// Summary description for WarehousesTest
    /// </summary>
    [TestClass]
    public class WarehousesTest
    {
        public WarehousesTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        ///A test for SaveToFile
        ///</summary>
        [TestMethod()]
        public void SaveToFileTest()
        {
            Warehouses target = new Warehouses();
            Warehouse w1 = new Warehouse { Code = "WH01", Name1 = "Bukit Pinang" };

            vCardDeliveryAddress address = new vCardDeliveryAddress();
            address.Street = "563 Jalan 6";
            address.City = "Bukit Pinang";
            address.Region = "Kedah";
            w1.Addresses.Add(address);

            target.Warehouse.Add(w1);
            target.SaveToFile();
        }
    }
}