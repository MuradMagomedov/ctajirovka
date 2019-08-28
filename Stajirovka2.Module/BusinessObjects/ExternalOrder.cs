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
using DevExpress.ExpressApp.DC;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AssociationAttribute = DevExpress.Xpo.AssociationAttribute;
using AggregatedAttribute = DevExpress.Xpo.AggregatedAttribute;

namespace Stajirovka2.Module.BusinessObjects.Stajirovka
{

    [NavigationItem("Стажировка")]
    [DomainComponent, DefaultClassOptions, ImageName("BO_Order"), XafDisplayName("Заказ")]
    [Persistent("AMMExternalOrder")]
    // [Appearance("NumberOrder", TargetItems = "NumberOrder",Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)  ]
    [Appearance("Product1", TargetItems = "Product", Criteria = "Product = ''",  BackColor = "LemonChiffon")]
    [Appearance("Product2", TargetItems = "Product", Criteria = "DateBegin >'01.01.2000'",  FontColor = "red")]
    [Appearance("DateBegin1", TargetItems = "*;Product;Qty;DateBegin;NameOrder;DateOrder;Sum;Product;OrderStatus;NumberOrder;OrderKind;Operations",
    Criteria = "DateBegin Is Null", Enabled = false)]
    [RuleCriteria( "DateEnd > DateBegin and DateBegin >'01.01.2000'")]
   
    public class ExternalOrder : BaseObject, IExternalOrder
	{
        public ExternalOrder(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();         
            Number = Session.Query<ExternalOrder>().Select(o => o.Number).ToArray().DefaultIfEmpty().Max() + 1;
            OrderStatus = Session.Query<OrderStatus>().Where(o=>o.Code== "01").ToArray()[0];
            // Sum =Qty * Session.Query<ExternalOrder>().Where(o=>o.Operation.Oid==;
            Qty = 1;
        }
        protected override void OnSaving()
        {

            base.OnSaving();
            Sum = this.Operation.Select(o => o.Sum).ToArray().DefaultIfEmpty().Sum();
        }
        [DisplayName("Номер заказа")]       
        public  int Number { get; set; }

        [DisplayName("Наименование")]
        public string NameOrder { get; set; }

        [DisplayName("Дата заказа")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy.MM.dd}")]
        public DateTime DateOrder { get; set; }
        [DisplayName("Сумма заказа")]
       
        public decimal Sum { get; set; }

        //Номенклатурная
        [DisplayName("Номенклатурная позиция")]
       
        [RuleRequiredField, RuleUniqueValue]
        [RuleRegularExpression(@"\w")]       

        public string Product { get; set; }
        [DisplayName("Количество")]

        [RuleRange( 0, 100)]

        public decimal Qty { get; set; }

        [DisplayName("Статус заказа")]
        public OrderStatus OrderStatus { get; set; }
        [DisplayName("Вид заказа")]
        public OrderKind OrderKind { get; set; }

        [DisplayName("Операции")]
        [Association, Aggregated]
        public XPCollection<OrderOperation> Operation
        {
            get { return GetCollection<OrderOperation>("Operation"); }
        }
		private DateTime dateBegin;

		[Persistent]
		[DisplayName("Дата начала")]
		public DateTime DateBegin { get =>dateBegin;
			set => SetPropertyValue(nameof(this.DateBegin), ref dateBegin, value);

		}
		public void CalcilateDateBegin()
		{
			DateBegin = this.Operation.Select(o => o.DateBegin).DefaultIfEmpty().ToArray().Min();
		}

		[Persistent]
		[DisplayName("Дата окончания")]
		public DateTime DateEnd { get; set; }
		public void CalcilateDateEnd()
		{
			DateEnd = this.Operation.Select(o => o.DateBegin).DefaultIfEmpty().ToArray().Max();
		}
	}
}
