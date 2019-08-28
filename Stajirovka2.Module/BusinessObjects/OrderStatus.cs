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
    [DomainComponent, DefaultClassOptions, ImageName("BO_Status"),XafDisplayName("Статус")]
    [Persistent("AMMOrderStatus")]
    public class OrderStatus : BaseObject
    {
        public OrderStatus(Session session) : base(session) { }
        [DisplayName("Код")]
        public string Code { get; set; }
        [DisplayName("Наименование")]
        public string Name { get; set; }

       public List<OrderStatus> OrderStatuses { get; set; }
    }
}
