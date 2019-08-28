namespace Stajirovka2.Module.Controllers
{
    partial class OrderOperationController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
            simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            simpleAction1.Caption = "simple Action 1";
            simpleAction1.Category = "RecordEdit";
            simpleAction1.ConfirmationMessage = null;
            simpleAction1.Id = "simpleAction1";
            simpleAction1.ToolTip = null;
            simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // OrderOperationController
            // 
            this.Actions.Add(simpleAction1);
            this.TargetObjectType = typeof(Stajirovka2.Module.BusinessObjects.Stajirovka.OrderOperation);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion
    }
}
