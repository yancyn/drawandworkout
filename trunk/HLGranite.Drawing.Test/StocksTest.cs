using HLGranite.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Drawing.Test
{
    /// <summary>
    ///This is a test class for StocksTest and is intended
    ///to contain all StocksTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StocksTest
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
        public void SaveToFileTest()
        {
            Stocks target = new Stocks();
            Stock apple = new Stock { Code = "APPLE", Name1 = "Apple", Name2 = "苹果" };
            Stock banana = new Stock { Code = "BANANA", Name1 = "Banana", Name2 = "香蕉", Notes = string.Empty };
            target.Stock.Add(apple);
            target.Stock.Add(banana);
            target.SaveToFile();//"Stocks.xml");
        }
        /// <summary>
        ///A test for LoadFromFile
        ///</summary>
        [TestMethod()]
        public void LoadFromFileTest()
        {
            //string fileName = "Stocks.xml";
            Stocks expected = new Stocks();
            Stock apple = new Stock { Code = "APPLE", Name1 = "Apple", Name2 = "苹果" };
            Stock banana = new Stock { Code = "BANANA", Name1 = "Banana", Name2 = "香蕉", Notes = string.Empty };
            expected.Stock.Add(apple);
            expected.Stock.Add(banana);
            expected.SaveToFile();//fileName);

            Stocks actual;
            actual = DatabaseObject.LoadFromFile() as Stocks;
            CollectionAssert.AreEqual(expected.Stock, actual.Stock);
        }
    }
}