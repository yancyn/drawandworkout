using System;
using HLGranite.Drawing;
using Thought.vCards;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Drawing.Test
{
    /// <summary>
    ///This is a test class for ProjectTest and is intended
    ///to contain all ProjectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProjectTest
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
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            Employee creator = new Employee();
            creator.EmailAddresses.Add(new vCardEmailAddress { Address = "yancyn@gmail.com" });

            Customer agent = new Customer { GivenName = "John" };

            Customer customer = new Customer { GivenName = "Ah Hock" };
            vCardDeliveryAddress deliver = new vCardDeliveryAddress();
            deliver.Street = "963 Jalan 6";
            deliver.Region = "Machang Bubok";
            deliver.City = "Bukit Mertajam";
            deliver.PostalCode = "05400";
            deliver.Country = "Malaysia";
            customer.DeliveryAddresses.Add(deliver);

            Project target = new Project();
            target.CreatedBy = creator;
            target.DeliverTo = customer;//customer.DeliveryAddresses[0];
            target.OrderBy = agent;
            target.Stage = ProjectStage.Draft;
            //target.

            target.Save();
        }
        [TestMethod()]
        public void AddWorkOrderSaveTest()
        {
            Employee creator = new Employee();
            creator.EmailAddresses.Add(new vCardEmailAddress { Address = "yancyn@gmail.com" });

            Customer agent = new Customer { GivenName = "One Kitchen" + new Random().Next(20) };

            Customer customer = new Customer { GivenName = "Ah Shing" };
            customer.Phones.Add(new vCardPhone { FullNumber = "012-4711134" });
            vCardDeliveryAddress deliver = new vCardDeliveryAddress();
            deliver.Street = "963 Jalan 6\nMachang Bubok";
            deliver.City = "Bukit Mertajam";
            deliver.Region = "Penang";
            deliver.PostalCode = "14020";
            deliver.Country = "Malaysia";
            customer.DeliveryAddresses.Add(deliver);
            customer.Latitude = 5.33398f;
            customer.Longitude = 100.50754f;

            Project target = new Project();
            target.CreatedBy = creator;
            target.DeliverTo = customer;//customer.DeliveryAddresses[0];
            target.OrderBy = agent;
            target.Stage = ProjectStage.Draft;

            Stock stock = new Stock { Name1 = "Blue Pearl" };

            LShapeItem w1 = new LShapeItem("LShapeItem04", stock, 48, 108);
            //w1.Lengths[0].Type = new Bullnose { Model="dep2"};
            w1.Lengths[1].Type = new Bullnose { Model = "dep2" };
            w1.Lengths[2].Type = new Bullnose { Model = "dep2" };
            w1.Lengths[3].Type = new Bullnose { Model = "dep2" };
            w1.Lengths[4].Type = new Bullnose { Model = "dep2" };
            //w1.Lengths[5].Type = new Bullnose { Model = "dep2" };
            //w1.Elements.Add(new VerticalLine(string.Empty, 150));

            RectItem w2 = new RectItem("RectItem00", stock, 6, 24);
            w2.Top = 400;
            w2.Left = 200;

            RectItem w3 = new RectItem("RectItem00", stock, 13, 26);

            WorkOrder wo = new WorkOrder();
            wo.Items.Add(w1);
            wo.Items.Add(w2);
            wo.Items.Add(w3);
            target.WorkOrders.Add(wo);

            target.Save();
        }
        [TestMethod()]
        public void SaveWithoutDueDateTest()
        {
            Employee creator = new Employee();
            creator.EmailAddresses.Add(new vCardEmailAddress { Address = "yancyn@gmail.com" });

            Customer agent = new Customer { GivenName = "John" };

            Customer customer = new Customer { GivenName = "Ah Hock" };
            vCardDeliveryAddress deliver = new vCardDeliveryAddress();
            deliver.Street = "963 Jalan 6";
            deliver.Region = "Machang Bubok";
            deliver.City = "Bukit Mertajam";
            deliver.PostalCode = "05400";
            deliver.Country = "Malaysia";
            customer.DeliveryAddresses.Add(deliver);

            Project target = new Project();
            target.CreatedBy = creator;
            target.DeliverTo = customer;//customer.DeliveryAddresses[0];
            target.OrderBy = agent;
            target.Stage = ProjectStage.Draft;

            target.Save();
        }
        [TestMethod()]
        public void SaveWithDueDateTest()
        {
            Employee creator = new Employee();
            creator.EmailAddresses.Add(new vCardEmailAddress { Address = "yancyn@gmail.com" });

            Customer agent = new Customer { GivenName = "John" };

            Customer customer = new Customer { GivenName = "Ah Hock" };
            vCardDeliveryAddress deliver = new vCardDeliveryAddress();
            deliver.Street = "963 Jalan 6";
            deliver.Region = "Machang Bubok";
            deliver.City = "Bukit Mertajam";
            deliver.PostalCode = "05400";
            deliver.Country = "Malaysia";
            customer.DeliveryAddresses.Add(deliver);

            Project target = new Project();
            target.CreatedBy = creator;
            target.DeliverTo = customer;//customer.DeliveryAddresses[0];
            target.OrderBy = agent;
            target.Stage = ProjectStage.Draft;
            target.DueDate = System.DateTime.Now.AddDays(10);

            target.Save();
        }
    }
}