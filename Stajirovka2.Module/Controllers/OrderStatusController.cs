using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using Stajirovka2.Module.BusinessObjects.Stajirovka;

namespace Stajirovka2.Module.Controllers
{
   public class OrderStatusController : ViewController
    {
        public OrderStatusController()
        {
            TargetObjectType = typeof(ExternalOrder);
            TargetViewType = ViewType.Any;
        /*    SimpleAction markCompletedAction = new SimpleAction(
                this, "AMMExternalOrder",
                DevExpress.Persistent.Base.PredefinedCategory.RecordEdit)
            {
                TargetObjectsCriteria =
                    (CriteriaOperator.Parse("Status != ?", OrderStatus.Completed)).ToString(),
                ConfirmationMessage =
                            "Are you sure you want to mark the selected task(s) as 'Completed'?",
                ImageName = "State_Task_Completed"
            };
            markCompletedAction.Execute += (s, e) => {
                foreach (OrderStatus task in e.SelectedObjects)
                {
                    task.EndDate = DateTime.Now;
                    task.Status = ProjectTaskStatus.Completed;
                    View.ObjectSpace.SetModified(task);
                }
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            };*/
        }
    }
}
