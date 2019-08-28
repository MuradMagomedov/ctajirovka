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
using System.ComponentModel;

namespace Stajirovka2.Module.BusinessObjects.Stajirovka
{

    [NavigationItem("Стажировка")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Операция заказа")]
    [Persistent("AMMOrderOperation")]
    public class OrderOperation : BaseObject, IOptionsAdjust
    { 
        public OrderOperation(Session session) : base(session) { }
		private bool doCalculateDateBegin = false;
		private bool doCalculateDateEnd = false;
		public override void AfterConstruction()
        {
            base.AfterConstruction();
            Number = Session.Query<OrderOperation>().Select(o => o.Number).ToArray().DefaultIfEmpty().Max() + 1;
           // Session.Query<OrderOperation>().Where (o=> o.MaterialOrders.)

         
        }
		protected override void OnDeleting()
		{
			base.OnDeleting();
			this.ExternalOrder.CalcilateDateBegin();
			this.ExternalOrder.CalcilateDateEnd();
		}

		protected override void OnSaving()
        {
            base.OnSaving();

			if (doCalculateDateBegin)
				this.ExternalOrder.CalcilateDateBegin();
			if (doCalculateDateEnd)
				this.ExternalOrder.CalcilateDateEnd();
			Adjust();

			//  MaterialOrdersSum();

			//Sum = this.MaterialOrders.Select(o => o.Sum).ToArray().DefaultIfEmpty().Sum();
		}
        public void MaterialOrdersSum() {
            Sum = this.MaterialOrders.Select(o => o.Sum).ToArray().DefaultIfEmpty().Sum();
        }
       protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {

            base.OnChanged(propertyName, oldValue, newValue);
            if ((propertyName == "DateBegin") || (propertyName == "DateEnd"))
            {
				doCalculateDateBegin = true;
				doCalculateDateEnd = true;
                if (DateBegin != null && DateEnd!=null)
                {
                   
                     Duration = DateEnd - DateBegin;
                  
                }
            }
			Adjust();
			// Sum = this.Operation.Select(o => o.Sum).ToArray().DefaultIfEmpty().Sum();



		} /**/

		#region Adjust
		private long _optionsAdjust;
		[Browsable(false)]
		[VisibleInListView(false)]
		[VisibleInDetailView(false)]
		[VisibleInLookupListView(false)]
		[Persistent]
		public long OptionsAdjust
		{
			get { return _optionsAdjust; }
			set { SetPropertyValue<long>("OptionsAdjust", ref _optionsAdjust, value); }
		}
		/// <summary>
		/// Установка бита пересчета
		/// </summary>
		/// <param name="bit">Номер бита.</param>
		public void SetBit(byte bit)
		{
			OptionsAdjust = OptionsAdjustHelper.SetBit(OptionsAdjust, bit);
		}

		/// <summary>
		/// Снятие бита пересчета
		/// </summary>
		/// <param name="bit">Номер бита.</param>        
		public void SetBitZero(byte bit)
		{
			OptionsAdjust = OptionsAdjustHelper.SetBitZero(OptionsAdjust, bit);
		}

		/// <summary>
		/// Установка всех битов в 1
		/// </summary>
		public virtual void SetAllBit()
		{
			OptionsAdjust = OptionsAdjustHelper.SetAllBit();
		}

		/// <summary>
		/// Возвращает номер бита (алгоритма пересчета) по наименованию поля, по которму должен осуществляться пересчет.
		/// </summary>
		/// <param name="propertyName">Наименование поля (Шаблон: Наименование_класса.Наименование_поля)</param>
		/// <returns>Возвращает номер бита (алгоритма пересчета).</returns>
		public virtual long GetBit(string propertyName)
		{
			long res = 0;

			switch (propertyName)
			{
				case "Sum":
					res = OptionsAdjustHelper.SetBit(res, 1);
					break;
			}


			return res;
		}

		/// <summary>
		/// Установливает бит алгоритма пересчета по наименованию поля, которое было изменено.
		/// </summary>
		/// <param name="propertyName">Наименование поля (Шаблон: Наименование_класса.Наименование_поля)</param>
		public virtual void SetBit(string propertyName)
		{
			OptionsAdjustHelper.SetBit(this, propertyName);
		}

		/// <summary>
		/// Связка функций пересчета и бит.
		/// Биты пересчетов.
		/// 1 – пересчет свойства Summa
		/// </summary>
		public virtual void Adjust(byte bit)
		{
			switch (bit)
			{
				case 1:
					CalculateSum();
					break;
			}
			this.SetBitZero(bit);

		}

		/// <summary>
		/// Запуск алгоритма пересчета по наименованию
		/// </summary>
		/// <param name="propertyName">Наименование поля (Шаблон: Наименование_класса.Наименование_поля)</param>
		public virtual void Adjust(string propertyName)
		{
			OptionsAdjustHelper.AdjustFromPropertyName(this, propertyName);
		}

		/// <summary>
		/// Проверка установленого параметра пересчетов и запуск пересчетов.
		/// </summary>
		public virtual void Adjust()
		{
			for (byte i = 1; i <= 32; i++)
			{
				if ((OptionsAdjust & (1 << i)) != 0)
				{
					Adjust(i);
				}
			}
			OptionsAdjust = 0;
		}

		#endregion


		[DevExpress.Xpo.DisplayName("Номер")]
        public int Number { get; set; }
        [DevExpress.Xpo.DisplayName("Наименование")]
        public string NameOperation { get; set; }
        [DevExpress.Xpo.DisplayName("Место")]
        public string Place { get; set; }
        [DevExpress.Xpo.DisplayName("Дата начала")]
        public DateTime DateBegin { get; set; }
        [DevExpress.Xpo.DisplayName("Дата окончания")]
        public DateTime DateEnd { get; set; }
       [DevExpress.Xpo.DisplayName("Длительность")]
      
        public TimeSpan Duration { get; set; }


		#region Sum
		private decimal sum;
		[Persistent]
		[DevExpress.Xpo.DisplayName("Сумма заказа")]
		public decimal Sum
		{
			get => sum;
			set => SetPropertyValue(nameof(this.Sum), ref sum, value);
		}
		public void CalculateSum()
		{
			Sum = this.MaterialOrders.Select(o => o.Sum).DefaultIfEmpty().ToArray().Sum();
		}
		#endregion

		[DevExpress.Xpo.DisplayName("Заказ")]
        [Association]
        public ExternalOrder ExternalOrder { get; set; }

        [DevExpress.Xpo.DisplayName("Материалы")]       
        [Association, Aggregated]
        public XPCollection<OrderMaterial> MaterialOrders
        {
            get { return GetCollection<OrderMaterial>("MaterialOrders"); }
        }
    }
}
