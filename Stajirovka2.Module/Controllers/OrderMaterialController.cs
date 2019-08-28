using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using Stajirovka2.Module.BusinessObjects.Stajirovka;

using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using System.Drawing;
using DevExpress.Xpo;
using DevExpress.ExpressApp.SystemModule;

namespace Stajirovka2.Module.Controllers
{
    public class OrderMaterialController : ViewController
    {
        private System.ComponentModel.IContainer components;

        public OrderMaterialController()
        {
            TargetObjectType = typeof(OrderMaterial);
            TargetViewType = ViewType.Any;
                decimal Qty=3;
            SimpleAction markSumEditAction = new SimpleAction(
                 this, "markSumEditAction",
                 DevExpress.Persistent.Base.PredefinedCategory.RecordEdit)
            {

                TargetObjectsCriteria =
                        (CriteriaOperator.Parse("Qty > ?", Qty)).ToString()
                    ,
                ConfirmationMessage = "В материале нет цены или количества"
                 ,ImageName = "BO_Country"
            };
            markSumEditAction.Caption = "Расчет суммы";
            markSumEditAction.Execute += (s, e) => {

                /**/
                foreach (OrderMaterial m in e.SelectedObjects)
                {
                    m.Sum = m.Price * m.Qty;
                    View.ObjectSpace.SetModified(m);
                }
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            };
            SimpleAction DeleteOrderMaterial = new SimpleAction(this, "delete", DevExpress.Persistent.Base.PredefinedCategory.RecordEdit)
            {

                TargetObjectsCriteria =
                      (CriteriaOperator.Parse("Qty > ?", Qty)).ToString()
                  ,
                ConfirmationMessage = "В материале нет цены или количества"
               ,
                ImageName = "BO_Country"
            };
            DeleteOrderMaterial.Caption = "Удалить";
            DeleteOrderMaterial.Execute += (s, e) => {

                /**/
                foreach (OrderMaterial m in e.SelectedObjects)
                {
                    m.Sum = m.Price * m.Qty;
                    View.ObjectSpace.SetModified(m);
                }
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            };
            SimpleAction markSumViewAction = new SimpleAction(
                 this, "AMMOrderMaterial",
                 DevExpress.Persistent.Base.PredefinedCategory.Appearance)
            {

                TargetObjectsCriteria =
                        (CriteriaOperator.Parse("Qty > 50", Qty)).ToString()
                    ,
                ConfirmationMessage = "В материале нет цены или количества"
                 //,ImageName = "BO_Country_v92"
               
            };

            markSumViewAction.Execute += (s, e) => {
              
                   /**/ foreach (OrderMaterial m in e.SelectedObjects)
                    {
                    m.Sum = m.Price * m.Qty;                      
                        View.ObjectSpace.SetModified(m);
                    }
                    View.ObjectSpace.CommitChanges();
                    View.ObjectSpace.Refresh();
                };/**/
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // 
            // OrderMaterialController
            // 

        }
        protected override void OnActivated()
        {
            base.OnActivated();
            DeleteObjectsViewController controller =
            Frame.GetController<DeleteObjectsViewController>();
            var actions = controller.Actions;
            ActionBase delete = controller.Actions["Delete"];
           // delete.Executing += Delete_Execute;
        }
        protected override void OnDeactivated()
        {
            DeleteObjectsViewController controller =
            Frame.GetController<DeleteObjectsViewController>();
            ActionBase delete = controller.Actions["Delete"];
           // delete.Executing -= Delete_Execute;
            base.OnDeactivated();
        }
        void delete_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // if ((View.CurrentObject as OrderMaterial).Статус ==
          // СтатусНоменклатуры.Архивный)
                e.Cancel = true;
        }

        private void Delete_Execute(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
