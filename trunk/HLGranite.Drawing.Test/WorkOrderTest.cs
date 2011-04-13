using System;
using System.Collections.Generic;
using HLGranite.Drawing;
using Thought.vCards;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Drawing.Test
{
    /// <summary>
    /// This is a test class for WorkOrderTest and is intended
    /// to contain all WorkOrderTest Unit Tests
    /// </summary>
    [TestClass()]
    public class WorkOrderTest
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
        ///A test for WorkOrder Constructor
        ///</summary>
        [TestMethod()]
        public void WorkOrderUniqueGuidTest()
        {
            Stock stock = new Stock { Name1 = "Blue Pearl" };
            LShapeItem w1 = new LShapeItem("LShapeItem04", stock, 48, 108);
            w1.Lengths[1].Type = new Bullnose("dep2");
            w1.Lengths[2].Type = new Bullnose("dep2");
            w1.Lengths[3].Type = new Bullnose("dep2");
            w1.Lengths[4].Type = new Bullnose("dep2");

            RectItem w2 = new RectItem("RectItem00", stock, 24, 6);
            w2.Top = 400;
            w2.Left = 200;
            w2.AddElement();

            RectItem w3 = new RectItem("RectItem00", stock, 28, 13);
            w3.Top = 100;
            w3.AddElement();
            w3.AddElement();

            RectItem w4 = new RectItem("RectItem00", stock, 36, 4);
            w4.Top = 100;
            w4.AddElement();
            w4.AddElement();
            w4.AddElement();

            WorkOrder target = new WorkOrder();
            target.Items.Add(w1);
            target.Items.Add(w2);
            target.Items.Add(w3);
            target.Items.Add(w4);

            //using dictionary to insert unique key for all ShapeItem and the children collection.
            Dictionary<string, ShapeItem> actual = new Dictionary<string, ShapeItem>();
            RetrieveShapeItem(target.Items, ref actual);
            //if cause exception means fail
            System.Diagnostics.Debug.WriteLine(actual.Count);
        }
        private void RetrieveShapeItem(IList<WorkItem> collection, ref Dictionary<string, ShapeItem> output)
        {
            foreach (WorkItem item in collection)
            {
                output.Add(item.Guid.ToString(), item);
                RetrieveShapeItem(item.Elements, ref output);
            }
        }
        private void RetrieveShapeItem(IList<ShapeItem> collection, ref Dictionary<string, ShapeItem> output)
        {
            foreach (ShapeItem item in collection)
            {
                if (item is WorkItem)
                {
                    output.Add((item as WorkItem).Guid.ToString(), item);
                    RetrieveShapeItem((item as WorkItem).Elements, ref output);
                }
            }
        }
    }
}