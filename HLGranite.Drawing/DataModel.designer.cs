// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.38967
//    <NameSpace>HLGranite.Drawing</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>True</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net35</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>False</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>True</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
//  </auto-generated>
// ------------------------------------------------------------------------------
namespace HLGranite.Drawing
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Collections;
    using System.Xml.Schema;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Windows.Shapes;
    using Thought.vCards;

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Project))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(WorkItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryWIP))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Inventory))]
    public partial class BaseAttribute
    {

        private string guidField;

        protected DateTime date;

        private string notesField;

        private List<Object> tagField;

        /// <summary>
        /// BaseAttribute class constructor
        /// </summary>
        public BaseAttribute()
        {
            this.tagField = new System.Collections.Generic.List<Object>();
        }

        public string Guid
        {
            get
            {
                return this.guidField;
            }
            set
            {
                this.guidField = value;
            }
        }

        public string Notes
        {
            get
            {
                return this.notesField;
            }
            set
            {
                this.notesField = value;
            }
        }

        public List<Object> Tag
        {
            get
            {
                return this.tagField;
            }
            set
            {
                this.tagField = value;
            }
        }
    }


    public partial class WorkOrder
    {

        private string guidField;

        private Stock stockField;

        private List<WorkItem> itemsField;

        /// <summary>
        /// WorkOrder class constructor
        /// </summary>
        public WorkOrder()
        {
            this.itemsField = new System.Collections.Generic.List<WorkItem>();
            this.stockField = new Stock();
        }

        public string Guid
        {
            get
            {
                return this.guidField;
            }
            set
            {
                this.guidField = value;
            }
        }

        public Stock Stock
        {
            get
            {
                return this.stockField;
            }
            set
            {
                this.stockField = value;
            }
        }

        public List<WorkItem> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }


    public partial class Stock : BaseElement
    {
    }

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Warehouse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Stock))]
    public partial class BaseElement
    {

        private string codeField;

        private string name1Field;

        private string name2Field;

        private string notesField;

        private List<Object> tagField;

        /// <summary>
        /// BaseElement class constructor
        /// </summary>
        public BaseElement()
        {
            this.tagField = new System.Collections.Generic.List<Object>();
        }

        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        public string Name1
        {
            get
            {
                return this.name1Field;
            }
            set
            {
                this.name1Field = value;
            }
        }

        public string Name2
        {
            get
            {
                return this.name2Field;
            }
            set
            {
                this.name2Field = value;
            }
        }

        public string Notes
        {
            get
            {
                return this.notesField;
            }
            set
            {
                this.notesField = value;
            }
        }

        public List<Object> Tag
        {
            get
            {
                return this.tagField;
            }
            set
            {
                this.tagField = value;
            }
        }
    }


    public partial class Warehouse : BaseElement
    {

        private vCardDeliveryAddressCollection addressesField;

        /// <summary>
        /// Warehouse class constructor
        /// </summary>
        public Warehouse()
        {
            this.addressesField = new vCardDeliveryAddressCollection();
        }

        public vCardDeliveryAddressCollection Addresses
        {
            get
            {
                return this.addressesField;
            }
            set
            {
                this.addressesField = value;
            }
        }
    }

    public partial class WorkItem : BaseItem
    {

        private Employee workedByField;

        private ShapeItem shapeItemField;

        private Stock materialField;

        private ShapeItem elementField;

        private WorkStatus statusField;

        private decimal progressField;

        /// <summary>
        /// WorkItem class constructor
        /// </summary>
        public WorkItem()
        {
            this.elementField = new ShapeItem();
            this.materialField = new Stock();
            this.shapeItemField = new ShapeItem();
            this.workedByField = new Employee();
        }

        public System.DateTime CreatedAt
        {
            get
            {
                return base.date;
            }
            set
            {
                base.date = value;
            }
        }

        public Employee WorkedBy
        {
            get
            {
                return this.workedByField;
            }
            set
            {
                this.workedByField = value;
            }
        }

        public ShapeItem ShapeItem
        {
            get
            {
                return this.shapeItemField;
            }
            set
            {
                this.shapeItemField = value;
            }
        }

        public int MaxWidth
        {
            get
            {
                return base.width;
            }
            set
            {
                base.width = value;
            }
        }

        public int MaxHeight
        {
            get
            {
                return base.height;
            }
            set
            {
                base.height = value;
            }
        }

        public Stock Material
        {
            get
            {
                return this.materialField;
            }
            set
            {
                this.materialField = value;
            }
        }

        public ShapeItem Element
        {
            get
            {
                return this.elementField;
            }
            set
            {
                this.elementField = value;
            }
        }

        public WorkStatus Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <summary>
        /// Progress in percentage for this work item only.
        /// </summary>
        public decimal Progress
        {
            get
            {
                return this.progressField;
            }
            set
            {
                this.progressField = value;
            }
        }
    }

    public partial class Employee : User
    {
    }



    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Employee))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Customer))]
    public partial class User : vCard
    {

        private Role typeField;

        public Role Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    public enum Role
    {

        /// <remarks/>
        Admin,

        /// <remarks/>
        Planner,

        /// <remarks/>
        Employee,

        /// <remarks/>
        Customer,

        /// <remarks/>
        Supplier,
    }

    public partial class Customer : User
    {
    }

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RectItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PolygonItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ZShapeItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UShapeItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(TriangleItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LShapeItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EllipseItem))]
    public partial class ShapeItem : Shape
    {

        private List<ShapeItem> childField;

        /// <summary>
        /// ShapeItem class constructor
        /// </summary>
        public ShapeItem()
        {
            this.childField = new System.Collections.Generic.List<ShapeItem>();
        }

        public List<ShapeItem> Child
        {
            get
            {
                return this.childField;
            }
            set
            {
                this.childField = value;
            }
        }
    }


    public partial class RectItem : ShapeItem
    {

        private int widthField;

        private int heightField;

        public int Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        public int Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }
    }

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ZShapeItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(UShapeItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(TriangleItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LShapeItem))]
    public partial class PolygonItem : ShapeItem
    {

        private List<LengthItem> lengthsField;

        /// <summary>
        /// PolygonItem class constructor
        /// </summary>
        public PolygonItem()
        {
            this.lengthsField = new System.Collections.Generic.List<LengthItem>();
        }

        public List<LengthItem> Lengths
        {
            get
            {
                return this.lengthsField;
            }
            set
            {
                this.lengthsField = value;
            }
        }
    }

    public partial class LengthItem
    {

        private int lengthField;

        private Bullnose typeField;

        /// <summary>
        /// LengthItem class constructor
        /// </summary>
        public LengthItem()
        {
            this.typeField = new Bullnose();
        }

        public int Length
        {
            get
            {
                return this.lengthField;
            }
            set
            {
                this.lengthField = value;
            }
        }

        public Bullnose Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    public partial class Bullnose
    {
    }

    public partial class ZShapeItem : PolygonItem
    {
    }

    public partial class UShapeItem : PolygonItem
    {
    }

    public partial class TriangleItem : PolygonItem
    {
    }


    public partial class LShapeItem : PolygonItem
    {
    }

    public partial class EllipseItem : ShapeItem
    {

        private int widthField;

        private int heightField;

        public int Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        public int Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }
    }

    public enum WorkStatus
    {

        /// <remarks/>
        NotStarted,

        /// <remarks/>
        InProgress,

        /// <remarks/>
        Completed,

        /// <remarks/>
        Rework,
    }

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(WorkItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryWIP))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Inventory))]
    public partial class BaseItem : BaseAttribute
    {

        private Unit uomField;

        /// <summary>
        /// Gets or sets Unit of measurement.
        /// </summary>
        public Unit Uom
        {
            get
            {
                return this.uomField;
            }
            set
            {
                this.uomField = value;
            }
        }
    }

    public enum Unit
    {

        /// <remarks/>
        Cm,

        /// <remarks/>
        Inch,

        /// <remarks/>
        Meter,

        /// <remarks/>
        Feet,
    }

    public partial class InventoryWIP : BaseItem
    {

        private Employee workedByField;

        private Inventory sourceField;

        private WorkItem workItemField;

        /// <summary>
        /// InventoryWIP class constructor
        /// </summary>
        public InventoryWIP()
        {
            this.workItemField = new WorkItem();
            this.sourceField = new Inventory();
            this.workedByField = new Employee();
        }

        public Employee WorkedBy
        {
            get
            {
                return this.workedByField;
            }
            set
            {
                this.workedByField = value;
            }
        }

        public System.DateTime WorkedAt
        {
            get
            {
                return base.date;
            }
            set
            {
                base.date = value;
            }
        }

        public int Width
        {
            get
            {
                return base.width;
            }
            set
            {
                base.width = value;
            }
        }

        public int Height
        {
            get
            {
                return base.height;
            }
            set
            {
                base.height = value;
            }
        }

        public Inventory Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }

        public WorkItem WorkItem
        {
            get
            {
                return this.workItemField;
            }
            set
            {
                this.workItemField = value;
            }
        }
    }

    public partial class Inventory : BaseItem
    {

        private Stock stockField;

        private Warehouse warehouseField;

        /// <summary>
        /// Inventory class constructor
        /// </summary>
        public Inventory()
        {
            this.warehouseField = new Warehouse();
            this.stockField = new Stock();
        }

        public System.DateTime PurchaseAt
        {
            get
            {
                return base.date;
            }
            set
            {
                base.date = value;
            }
        }

        public int Width
        {
            get
            {
                return base.width;
            }
            set
            {
                base.width = value;
            }
        }

        public int Height
        {
            get
            {
                return base.height;
            }
            set
            {
                base.height = value;
            }
        }

        public Stock Stock
        {
            get
            {
                return this.stockField;
            }
            set
            {
                this.stockField = value;
            }
        }

        public Warehouse Warehouse
        {
            get
            {
                return this.warehouseField;
            }
            set
            {
                this.warehouseField = value;
            }
        }
    }

    public partial class Project : BaseAttribute
    {

        private Employee createdByField;

        private List<WorkOrder> workOrdersField;

        private ProjectStage stageField;

        private decimal progressField;

        /// <summary>
        /// Project class constructor
        /// </summary>
        public Project()
        {
            this.workOrdersField = new System.Collections.Generic.List<WorkOrder>();
            this.createdByField = new Employee();
        }

        public System.DateTime CreatedAt
        {
            get
            {
                return base.date;
            }
            set
            {
                base.date = value;
            }
        }

        /// <summary>
        /// Gets or sets created person. Normally is the planner.
        /// </summary>
        public Employee CreatedBy
        {
            get
            {
                return this.createdByField;
            }
            set
            {
                this.createdByField = value;
            }
        }

        /// <summary>
        /// Gets or sets work order collection for this project.
        /// </summary>
        public List<WorkOrder> WorkOrders
        {
            get
            {
                return this.workOrdersField;
            }
            set
            {
                this.workOrdersField = value;
            }
        }

        /// <summary>
        /// Gets or sets latest project status i.e. Draft, Quotation, or delivered.
        /// </summary>
        public ProjectStage Stage
        {
            get
            {
                return this.stageField;
            }
            set
            {
                this.stageField = value;
            }
        }

        /// <summary>
        /// Overall progress in percentage for this project only.
        /// It may contains multiple of work order.
        /// </summary>
        public decimal Progress
        {
            get
            {
                return this.progressField;
            }
            set
            {
                this.progressField = value;
            }
        }
    }

    public enum ProjectStage
    {

        /// <remarks/>
        Draft,

        /// <remarks/>
        Quotation,

        /// <remarks/>
        Manufacture,

        /// <remarks/>
        Delivered,

        /// <remarks/>
        Invoice,

        /// <remarks/>
        Paid,
    }

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Stocks))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Warehouses))]
    //TODO: [System.Xml.Serialization.XmlIncludeAttribute(typeof(Users))]
    //TODO: [System.Xml.Serialization.XmlIncludeAttribute(typeof(Projects))]
    //TODO: [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryWIPs))]
    //TODO: [System.Xml.Serialization.XmlIncludeAttribute(typeof(Inventories))]
    public partial class DatabaseObject
    {
    }

    public partial class Warehouses : DatabaseObject
    {

        private List<Warehouse> warehouseField;

        /// <summary>
        /// Warehouses class constructor
        /// </summary>
        public Warehouses()
        {
            fileName = "Warehouses.xml";
            this.warehouseField = new System.Collections.Generic.List<Warehouse>();
        }

        public List<Warehouse> Warehouse
        {
            get
            {
                return this.warehouseField;
            }
            set
            {
                this.warehouseField = value;
            }
        }
    }

    public partial class Users : DatabaseObject
    {

        private List<User> userField;

        /// <summary>
        /// Users class constructor
        /// </summary>
        public Users()
        {
            this.userField = new System.Collections.Generic.List<User>();
        }

        public List<User> User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }
    }

    public partial class Stocks : DatabaseObject
    {

        private List<Stock> stockField;

        /// <summary>
        /// Stocks class constructor
        /// </summary>
        public Stocks()
        {
            this.stockField = new System.Collections.Generic.List<Stock>();
            fileName = "Stocks.xml";
        }

        public List<Stock> Stock
        {
            get
            {
                return this.stockField;
            }
            set
            {
                this.stockField = value;
            }
        }
    }

    public partial class Projects : DatabaseObject
    {

        private List<Project> projectField;

        /// <summary>
        /// Projects class constructor
        /// </summary>
        public Projects()
        {
            this.projectField = new System.Collections.Generic.List<Project>();
        }

        public List<Project> Project
        {
            get
            {
                return this.projectField;
            }
            set
            {
                this.projectField = value;
            }
        }
    }

    public partial class InventoryWIPs : DatabaseObject
    {

        private List<Inventory> inventoryField;

        /// <summary>
        /// InventoryWIPs class constructor
        /// </summary>
        public InventoryWIPs()
        {
            this.inventoryField = new System.Collections.Generic.List<Inventory>();
        }

        public List<Inventory> Inventory
        {
            get
            {
                return this.inventoryField;
            }
            set
            {
                this.inventoryField = value;
            }
        }
    }

    public partial class Inventories : DatabaseObject
    {

        private List<Inventory> inventoryField;

        /// <summary>
        /// Inventories class constructor
        /// </summary>
        public Inventories()
        {
            this.inventoryField = new System.Collections.Generic.List<Inventory>();
        }

        public List<Inventory> Inventory
        {
            get
            {
                return this.inventoryField;
            }
            set
            {
                this.inventoryField = value;
            }
        }
    }

    public partial class InventoryManager
    {
    }

}