using HLGranite.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HLGranite.Drawing.Test
{
    /// <summary>
    ///This is a test class for WorkItemTest and is intended
    ///to contain all WorkItemTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WorkItemTest
    {
        public WorkItemTest()
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
        /// A test to overwrite other length items value
        /// when there is first item set.
        /// </summary>
        [TestMethod()]
        public void LengthValueChangedTest()
        {
            LShapeItem l = new LShapeItem();
            int start = 3;
            Bullnose bullnose = new Bullnose("r");
            l.Lengths[start].Type = bullnose;
            for (int i = start; i < l.Lengths.Count; i++)
                Assert.AreEqual(bullnose.Model, l.Lengths[i].Type.Model);
        }
    }
}