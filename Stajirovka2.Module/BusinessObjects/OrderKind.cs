using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using System.Drawing;
using DevExpress.Persistent.Base.General;
using System.ComponentModel;
using Xafari.SmartDesign;
using Xafari.BC.DC;

namespace Stajirovka2.Module.BusinessObjects.Stajirovka
{

    [NavigationItem("Стажировка")]
  [DevExpress.ExpressApp.DC.XafDisplayName("Виды заказа2")]
    [Persistent("AMMOrderKind2")]
	[SmartDesignStrategy(typeof(XafariSmartDesignStrategy))]
	[CreateListView(EditorAlias = "OrderKind2_ListView", Id = "OrderKind2_ListView", Layout = "Code,Name,Parent;", IsDefaultGridListView = true)]
	[CreateListView(EditorAlias = "OrderKind2_TreeView", Id = "OrderKind2_TreeView", Layout = "Code,Name,Parent", IsDefaultTreeListView = true)]
	[CreateDetailView(Id = "OrderKind2_DetailView", Layout = "Code,Name,Parent;")]
	[CreateListView(EditorAlias = "OrderKind2_LookupListView", Id = "OrderKind2_LookupListView", Layout = "Code,Name,Parent", ListViewType = ListViewType.LookupListView)]

	public class OrderKind2 : BaseObject, ITreeNode
    {
        public OrderKind2(Session session) : base(session) { }


        /*  [DevExpress.ExpressApp.DC.XafDisplayName("Код")]
          public string Code { get; set; }
          [DevExpress.ExpressApp.DC.XafDisplayName("Наименование")]
          public string Name { get; set; }

          [DevExpress.ExpressApp.DC.XafDisplayName("Виды заказа")]      
          public List<OrderKind> OrderKinds { get; set; }

  //Реализация интерфейса

          string ITreeNode.Name => "Виды заказа";
          ITreeNode ITreeNode.Parent => this;

          IBindingList ITreeNode.Children => GetCollection<OrderKind>(nameof(Children));*/
        [DevExpress.ExpressApp.DC.XafDisplayName("Код")]
        public string Code { get; set; }
        [DevExpress.ExpressApp.DC.XafDisplayName("Наименование")]
        public string Name { get; set; }

        string ITreeNode.Name => this.Name;

        [Association("Parent-Child")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Наименование верхнего уровня")]
        public OrderKind2 Parent { get; set; }

        ITreeNode ITreeNode.Parent => this.Parent;

        [Association("Parent-Child")]
        public XPCollection<OrderKind2> Children { get { return GetCollection<OrderKind2>(nameof(Children)); } }
        IBindingList ITreeNode.Children => this.Children;

    }

	[NavigationItem("Стажировка")]
	[DevExpress.ExpressApp.DC.XafDisplayName("Виды заказа")]
	[Persistent("AMMOrderKind")]
	public class OrderKind : BaseObject, ITreeNode
	{
		public OrderKind(Session session) : base(session) { }


		/*  [DevExpress.ExpressApp.DC.XafDisplayName("Код")]
          public string Code { get; set; }
          [DevExpress.ExpressApp.DC.XafDisplayName("Наименование")]
          public string Name { get; set; }

          [DevExpress.ExpressApp.DC.XafDisplayName("Виды заказа")]      
          public List<OrderKind> OrderKinds { get; set; }

  //Реализация интерфейса

          string ITreeNode.Name => "Виды заказа";
          ITreeNode ITreeNode.Parent => this;

          IBindingList ITreeNode.Children => GetCollection<OrderKind>(nameof(Children));*/
		[DevExpress.ExpressApp.DC.XafDisplayName("Код")]
		public string Code { get; set; }
		[DevExpress.ExpressApp.DC.XafDisplayName("Наименование")]
		public string Name { get; set; }

		string ITreeNode.Name => this.Name;

		[Association("Parent-Child")]
		[DevExpress.ExpressApp.DC.XafDisplayName("Наименование верхнего уровня")]
		public OrderKind Parent { get; set; }

		ITreeNode ITreeNode.Parent => this.Parent;

		[Association("Parent-Child")]
		public XPCollection<OrderKind> Children { get { return GetCollection<OrderKind>(nameof(Children)); } }
		IBindingList ITreeNode.Children => this.Children;

	}
}
