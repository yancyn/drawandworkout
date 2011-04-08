using HLGranite.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Drawing.Test
{
    /// <summary>
    ///This is a test class for BullnosesTest and is intended
    ///to contain all BullnosesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BullnosesTest
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
        /// A test for Bullnoses Constructor
        /// </summary>
        [TestMethod()]
        public void BullnosesConstructorTest()
        {
            Bullnoses target = new Bullnoses();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
        [TestMethod()]
        public void SaveToFileTest()
        {
            Bullnoses target = new Bullnoses();
            target.Bullnose.Add(new Bullnose { Model = "db" });
            target.Bullnose.Add(new Bullnose { Model = "dep" });
            target.Bullnose.Add(new Bullnose { Model = "drp" });
            target.Bullnose.Add(new Bullnose { Model = "dcp" });
            target.Bullnose.Add(new Bullnose { Model = "dog" });
            target.Bullnose.Add(new Bullnose { Model = "ap" });
            target.Bullnose.Add(new Bullnose { Model = "ep" });
            target.Bullnose.Add(new Bullnose { Model = "cp" });
            target.Bullnose.Add(new Bullnose { Model = "r" });
            target.SaveToFile();
        }
    }
}