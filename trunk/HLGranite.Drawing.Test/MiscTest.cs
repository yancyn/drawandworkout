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
    public class MiscTest
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

        [TestMethod()]
        public void RoundFeetTest()
        {
            string expected = "6'";
            string actual = string.Empty;
            double target = 72;
            actual = ConvertInchToFeet(target);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void RemainderInchTest()
        {
            string expected = "1' 6\"";
            string actual = string.Empty;
            double target = 18;
            actual = ConvertInchToFeet(target);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void HalfInchTest()
        {
            string expected = "½\"";
            string actual = string.Empty;
            double target = 0.5;
            actual = ConvertToEighthInch(target);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void QuarterInchTest()
        {
            string expected = "¾\"";
            string actual = string.Empty;
            double target = 0.75;
            actual = ConvertToEighthInch(target);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void QuarterLikeInchTest()
        {
            string expected = "¾\"";
            string actual = string.Empty;
            double target = 0.8;
            actual = ConvertToEighthInch(target);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void FifteenPerSixteenInchTest()
        {
            string expected = "15/16\"";
            string actual = string.Empty;
            double target = 0.9375;
            actual = ConvertToEighthInch(target);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void PointNinceInchTest()
        {
            string expected = "⅞\"";
            string actual = string.Empty;
            double target = 0.9;
            actual = ConvertToEighthInch(target);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void DecimalFeetTest()
        {
            string expected = "6' ⅛\"";
            string actual = string.Empty;
            double target = 72.125;
            actual = ConvertInchToFeet(target);
            Assert.AreEqual(expected, actual);

            target = 72.6;
            expected = "6' 9/16\"";
            actual = ConvertInchToFeet(target);
            Assert.AreEqual(expected, actual);
        }


        private string ConvertInchToFeet(double sender)
        {
            string output = string.Empty;
            int i = sender.ToString().IndexOf('.');

            if (i == -1)
            {
                double remainder = sender % 12;
                int result = Convert.ToInt32((sender - remainder) / 12);
                output = result.ToString();
                output += "'";
                if (remainder > 0) output += " " + remainder.ToString() + "\"";
            }
            else
            {
                double roundNumber = Convert.ToDouble(sender.ToString().Substring(0, i));
                double floatingPoint = sender - roundNumber;
                if (floatingPoint + 15 / 16 > 1) //round up if bigger 0.9375
                    output = ConvertInchToFeet(roundNumber + 1);
                else
                    output = ConvertInchToFeet(roundNumber) + " " + ConvertToEighthInch(floatingPoint);
            }

            return output;
        }
        private string ConvertToEighthInch(double sender)
        {
            string output = string.Empty;
            double interval = 0.0625;// 0.125;
            string[] buckets = new string[16] {
                "0", "1/16", 
                "⅛", "3/16",
                "¼", "5/16",
                "⅜", "7/16",
                "½", "9/16",
                "⅝", "11/16",
                "¾", "13/16",
                "⅞", "15/16" };
            for (int i = 0; i < 16; i++)
            {
                if (sender < interval * (i + 1))
                {
                    output += buckets[i];
                    break;
                }
            }
            if (output.Length == 0 && sender < 1) output = "1";

            output += "\"";
            return output;
        }
    }
}