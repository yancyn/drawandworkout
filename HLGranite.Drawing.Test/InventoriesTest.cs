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
            inventory.Stock = new Stock { Code = "BLU", Name1 = "Blue Pearl" };
            inventory.Warehouse = new Warehouse { Name1 = "Bukit Pinang" };
            inventory.Width = 72;
            inventory.Height = 24;

            target.Add(50, inventory);
            //target.SaveToFile();
        }
    }
}