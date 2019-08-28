using DevExpress.Xpo;
using Stajirovka2.Module.BusinessObjects.Stajirovka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stajirovka2.Module.BusinessObjects
{
	interface IExternalOrder
	{
		[DisplayName("Номер заказа")]
		int Number { get; set; }

		[DisplayName("Наименование")]
		string NameOrder { get; set; }

		
		DateTime DateOrder { get; set; }
		[DisplayName("Сумма заказа")]

		decimal Sum { get; set; }
		string Product { get; set; }
		[DisplayName("Количество")]	

		decimal Qty { get; set; }

		[DisplayName("Статус заказа")]
		OrderStatus OrderStatus { get; set; }
		[DisplayName("Вид заказа")]
		OrderKind OrderKind { get; set; }

		[DisplayName("Операции")]
		[Association, Aggregated]
		XPCollection<OrderOperation> Operation { get; }
		

		[DisplayName("Дата начала")]
		
		DateTime DateBegin { get; set; }
		[DisplayName("Дата окончания")]
		
		DateTime DateEnd { get; set; }
	}
}
