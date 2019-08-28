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

namespace Stajirovka2.Module.BusinessObjects.Stajirovka
{

    [NavigationItem("Стажировка")]
    [XafDisplayName("Материал операции заказа")]
    [Persistent("AMMOrderMaterial")]
    //[Appearance("Number", TargetItems = "Number", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]

    public class OrderMaterial : BaseObject
    {
      
        public OrderMaterial(Session session) : base(session) { }  


        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Number= Session.Query<OrderMaterial>().Select(o=>o.Number).ToArray().DefaultIfEmpty().Max() + 1;
          
        }
        protected override void OnDeleting()
        {
            base.OnDeleting();
            OrderOperation orderOperation =Session.Query<OrderMaterial>().Where(o=> o.Oid==this.Oid).Select(o => o.OrderOperation).ToArray()[0];
              orderOperation.MaterialOrdersSum();
            
            Session.Save(orderOperation);

        }
		protected override void OnChanged(string propertyName, object oldValue, object newValue)
		{

			//doCalculateSum = true;
			switch (propertyName)
			{
				case "Sum":
					// Если в спецификации изменилась Summa, то надо пересчитать Summa в самом документе
					this.OrderOperation.SetBit("Sum");
					break;
			}



			if ((propertyName == "Price") || (propertyName == "Qty"))
			{
				CalculateSum();
			}
		}

		[DisplayName("Номер")]      
        public int Number
        {
            get  ;
            set ;
        }
       
        [DisplayName("Номенклатурная позиция")]
        public string Material { get; set; }
		#region Qty
		private decimal qty;
		[Persistent]
		[DisplayName("Количество")]
		public decimal Qty
		{
			get => qty;
			set => SetPropertyValue(nameof(this.Qty), ref qty, value);
		}
		#endregion

		#region Price
		private decimal price;
		[Persistent]
		[DisplayName("Цена")]
		public decimal Price
		{
			get => price;
			set => SetPropertyValue(nameof(this.Price), ref price, value);
		}
		#endregion
		#region Sum
		private decimal sum;
		[Persistent]
		[DisplayName("Сумма")]
		public decimal Sum
		{
			get => sum;
			set => SetPropertyValue(nameof(this.Sum), ref sum, value);
		}
		public void CalculateSum()
		{
			Sum = this.Qty * this.Price;
		}
		#endregion
		//Операция
		[DisplayName("Операция")]
        [Association]
        public OrderOperation OrderOperation { get; set; }


    }
}
