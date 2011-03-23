using HLGranite.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Drawing.Test
{
    /// <summary>
    /// This is a test class for DatabaseObjectTest and is intended
    /// to contain all DatabaseObjectTest Unit Tests
    /// </summary>
    [TestClass()]
    public class DatabaseObjectTest
    {
        public DatabaseObjectTest()
        {
        }

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
        /// A singleton test for Stocks ensure there is always only 1 instance.
        ///</summary>
        [TestMethod()]
        public void StocksTest()
        {
            int expected = 2;
            DatabaseObject.Stocks.Stock.Add(new Stock());
            DatabaseObject.Stocks.Stock.Add(new Stock());
            int actual = DatabaseObject.Stocks.Stock.Count;
            Assert.AreEqual(expected, actual);

            Stocks target = DatabaseObject.Stocks;
            target.Stock.RemoveAt(target.Stock.Count - 1);

            expected = 1;
            actual = DatabaseObject.Stocks.Stock.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}