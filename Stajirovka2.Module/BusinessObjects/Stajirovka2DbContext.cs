using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;

namespace Stajirovka2.Module.BusinessObjects {
	public class Stajirovka2DbContext : DbContext {
		public Stajirovka2DbContext(String connectionString)
			: base(connectionString) {
		}
		public Stajirovka2DbContext(DbConnection connection)
			: base(connection, false) {
		}
		public Stajirovka2DbContext()
			: base("name=ConnectionString") {
		}
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
	}
}