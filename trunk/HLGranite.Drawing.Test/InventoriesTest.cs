using HLGranite.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Drawing.Test
{
    /// <summary>
    ///This is a test class for InventoriesTest and is intended
    ///to contain all InventoriesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InventoriesTest
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
        ///A test for SaveToFile
        ///</summary>
        [TestMethod()]
        public void AddInventoryTest()
        {
            Inventories target = DatabaseObject.Inventories;

            Inventory inventory = new Inventory();
            inventory.Stock = new Stock { Code = "TAN", Name1 = "Tan Brown" };
            inventory.Warehouse = new Warehouse { Name1 = "Bukit Pinang" };
            inventory.Width = 72;
            inventory.Height = 24;

            target.Add(50, inventory);
            //target.SaveToFile();
        }
        [TestMethod()]
        public void AddInventoryWIPTest()
        {
            Inventories target = DatabaseObject.Inventories;

            Inventory inventory = new Inventory();
            inventory.Stock = new Stock { Code = "TAN", Name1 = "Tan Brown" };
            inventory.Warehouse = new Warehouse { Name1 = "Bukit Pinang" };
            inventory.Width = 72;
            inventory.Height = 24;

            InventoryWIP w1 = new InventoryWIP();
            w1.Width = 50;
            w1.Height = 24;

            InventoryWIP w2 = new InventoryWIP();
            w2.Width = 22;
            w2.Height = 12;

            InventoryWIP w3 = new InventoryWIP();
            w3.Width = 22;
            w3.Height = 12;

            inventory.Collection.Add(w1);
            inventory.Collection.Add(w2);
            inventory.Collection.Add(w3);
            inventory.Save();
            //target.Add(50, inventory);
            //target.SaveToFile();
        }
    }
}