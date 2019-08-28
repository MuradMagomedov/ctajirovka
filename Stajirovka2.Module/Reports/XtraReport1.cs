using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.Xpo;
using Stajirovka2.Module.BusinessObjects.Stajirovka;
using DevExpress.XtraRichEdit.Model;

namespace Stajirovka2.Module.Reports
{
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport1()
        {
            InitializeComponent();
        }
	/*	private void report_DataSourceDemanded(object sender, EventArgs e)
		{
			XPCollection<ExternalOrder> ExternalOrders = new XPCollection<ExternalOrder>();

			DataSource = ExternalOrders; }*/

	}
}
