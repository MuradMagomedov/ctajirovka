﻿using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using System.Data.Entity;
using Stajirovka2.Module.BusinessObjects;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.ReportsV2;
using Stajirovka2.Module.BusinessObjects.Stajirovka;
using Stajirovka2.Module.Reports;

namespace Stajirovka2.Module {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
    public sealed partial class Stajirovka2Module : ModuleBase {
        static Stajirovka2Module() {
            DevExpress.Data.Linq.CriteriaToEFExpressionConverter.SqlFunctionsType = typeof(System.Data.Entity.SqlServer.SqlFunctions);
			DevExpress.Data.Linq.CriteriaToEFExpressionConverter.EntityFunctionsType = typeof(System.Data.Entity.DbFunctions);
			DevExpress.ExpressApp.SystemModule.ResetViewSettingsController.DefaultAllowRecreateView = false;
         
            // Uncomment this code to delete and recreate the database each time the data model has changed.
            // Do not use this code in a production environment to avoid data loss.
            #if DEBUG
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Stajirovka2DbContext>());
             #endif 
        }
        public Stajirovka2Module() {
            InitializeComponent();
			BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
           //this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule));
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);

			//Регистрация репорта заказов
			PredefinedReportsUpdater ExternalOrderReportsUpdater = new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);
			ExternalOrderReportsUpdater.AddPredefinedReport<XtraReport2>("Закзы", typeof(ExternalOrder));
			
			//Регистрация репорта материала заказов
			PredefinedReportsUpdater reportsUpdater = new PredefinedReportsUpdater(Application, objectSpace, versionFromDB);		
			reportsUpdater.AddPredefinedReport<XtraReport1>("Матерьялы заказа", typeof(OrderMaterial));

            return new ModuleUpdater[] { updater, reportsUpdater , ExternalOrderReportsUpdater };
             /*  return new ModuleUpdater[] { updater };*/
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo) {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
        }
    }
}