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

            WorkOrder wo = new WorkOrder();
            wo.Items.Add(new WorkItem { MaxHeight = 24, MaxWidth = 56, Material = new Stock { Name1 = "Tan Brown" } });
            wo.Items.Add(new WorkItem { MaxHeight = 12, MaxWidth = 34, Material = new Stock { Name1 = "Black Galaxy" } });
            target.WorkOrders.Add(wo);

            target.Save();
        }
    }
}