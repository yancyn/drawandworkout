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

        /// <summary>
        ///A test for Users Constructor
        ///</summary>
        [TestMethod()]
        public void UsersConstructorTest()
        {
            Users target = new Users();
            Assert.IsTrue(target.User.Count > 0);
        }
    }
}
