// ------------------------------------------------------------------------------
//  <auto-generated>
//    Generated by Xsd2Code. Version 3.4.0.33213
//    <NameSpace>HLGranite.Drawing</NameSpace><Collection>List</Collection><codeType>CSharp</codeType><EnableDataBinding>False</EnableDataBinding><EnableLazyLoading>False</EnableLazyLoading><TrackingChangesEnable>False</TrackingChangesEnable><GenTrackingClasses>False</GenTrackingClasses><HidePrivateFieldInIDE>False</HidePrivateFieldInIDE><EnableSummaryComment>False</EnableSummaryComment><VirtualProp>False</VirtualProp><IncludeSerializeMethod>False</IncludeSerializeMethod><UseBaseClass>False</UseBaseClass><GenBaseClass>False</GenBaseClass><GenerateCloneMethod>False</GenerateCloneMethod><GenerateDataContracts>False</GenerateDataContracts><CodeBaseTag>Net35</CodeBaseTag><SerializeMethodName>Serialize</SerializeMethodName><DeserializeMethodName>Deserialize</DeserializeMethodName><SaveToFileMethodName>SaveToFile</SaveToFileMethodName><LoadFromFileMethodName>LoadFromFile</LoadFromFileMethodName><GenerateXMLAttributes>False</GenerateXMLAttributes><EnableEncoding>False</EnableEncoding><AutomaticProperties>False</AutomaticProperties><GenerateShouldSerialize>False</GenerateShouldSerialize><DisableDebug>False</DisableDebug><PropNameSpecified>Default</PropNameSpecified><Encoder>UTF8</Encoder><CustomUsings></CustomUsings><ExcludeIncludedTypes>False</ExcludeIncludedTypes><EnableInitializeFields>True</EnableInitializeFields>
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


    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Project))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(WorkItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryWIP))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Inventory))]
    public partial class BaseAttribute
    {

        private string guidField;

        private Employee creatorField;

        protected System.DateTime date;

        private string notesField;

        private Object tagField;

        public BaseAttribute()
        {
            this.tagField = new Object();
            this.creatorField = new Employee();
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

        public Employee creator
        {
            get
            {
                return this.creatorField;
            }
            set
            {
                this.creatorField = value;
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

        public Object Tag
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

    public partial class Employee : User
    {
    }

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Employee))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Customer))]
    public partial class User
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

        private Object tagField;

        public BaseElement()
        {
            this.tagField = new Object();
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

        public Object Tag
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

    public partial class Object
    {
    }

    public partial class Warehouse : BaseElement
    {
    }

    public partial class Stock : BaseElement
    {
    }

    public partial class Customer : User
    {
    }

    public partial class Project : BaseAttribute
    {

        private System.DateTime createdAtField;

        private Employee createdByField;

        private decimal progressField;

        public Project()
        {
            this.createdByField = new Employee();
        }

        public System.DateTime CreatedAt
        {
            get
            {
                return this.createdAtField;
            }
            set
            {
                this.createdAtField = value;
            }
        }

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

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(WorkItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(InventoryWIP))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Inventory))]
    public partial class BaseItem : BaseAttribute
    {

        private int widthField;

        private int heightField;

        private Unit uomField;

        public int width
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

        public int height
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

    public partial class WorkItem : BaseItem
    {

        private System.DateTime createdAtField;

        private Employee workedByField;

        private int minWidthField;

        private int minHeightField;

        private decimal progressField;

        private List<WorkItem> childField;

        public WorkItem()
        {
            this.childField = new List<WorkItem>();
            this.workedByField = new Employee();
        }

        public System.DateTime CreatedAt
        {
            get
            {
                return this.createdAtField;
            }
            set
            {
                this.createdAtField = value;
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

        public int MinWidth
        {
            get
            {
                return this.minWidthField;
            }
            set
            {
                this.minWidthField = value;
            }
        }

        public int MinHeight
        {
            get
            {
                return this.minHeightField;
            }
            set
            {
                this.minHeightField = value;
            }
        }

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

        public List<WorkItem> Child
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

    public partial class InventoryWIP : BaseItem
    {

        private Employee workedByField;

        private System.DateTime workedAtField;

        private int widthField;

        private int heightField;

        public InventoryWIP()
        {
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
                return this.workedAtField;
            }
            set
            {
                this.workedAtField = value;
            }
        }

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

    public partial class Inventory : BaseItem
    {

        private System.DateTime purchaseAtField;

        private int widthField;

        private int heightField;

        private Stock stockField;

        private Warehouse warehouseField;

        public Inventory()
        {
            this.warehouseField = new Warehouse();
            this.stockField = new Stock();
        }

        public System.DateTime PurchaseAt
        {
            get
            {
                return this.purchaseAtField;
            }
            set
            {
                this.purchaseAtField = value;
            }
        }

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

    public partial class Ellipse : Shape
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

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Rectangle))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Polygon))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(U))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Triangle))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(L))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Ellipse))]
    public partial class Shape
    {
    }

    public partial class Rectangle : Shape
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

    [System.Xml.Serialization.XmlIncludeAttribute(typeof(U))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Triangle))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(L))]
    public partial class Polygon : Shape
    {
    }

    public partial class U : Polygon
    {
    }

    public partial class Triangle : Polygon
    {
    }

    public partial class L : Polygon
    {
    }

    public partial class Inventories
    {

        private List<Inventory> inventoryField;

        public Inventories()
        {
            this.inventoryField = new List<Inventory>();
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

    public partial class Projects
    {

        private List<Project> projectField;

        public Projects()
        {
            this.projectField = new List<Project>();
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

    public partial class Stocks
    {

        private List<Stock> stockField;

        public Stocks()
        {
            this.stockField = new List<Stock>();
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

    public partial class WorkItemCollection
    {

        private List<WorkItem> workItemField;

        public WorkItemCollection()
        {
            this.workItemField = new List<WorkItem>();
        }

        public List<WorkItem> WorkItem
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

    public partial class WorkOrder
    {

        private string guidField;

        private List<WorkItem> workItemField;

        public WorkOrder()
        {
            this.workItemField = new List<WorkItem>();
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

        public List<WorkItem> WorkItem
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
}