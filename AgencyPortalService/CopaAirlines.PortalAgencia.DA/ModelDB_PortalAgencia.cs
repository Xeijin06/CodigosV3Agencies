namespace CopaAirlines.PortalAgencia.DA
{
    using CopaAirlines.PortalAgencia.DA.DbModels;
    using CopaAirlines.PortalAgencia.DA;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    //using CopaAirlines.PortalAgencia.DA.Migrations;

    public class ModelDB_PortalAgencia : DbContext, IContext_PortalAgencia
    {

        private const string connectionStringName = "name=DB_PortalAgencia"; 

        public ModelDB_PortalAgencia()
            : base(connectionStringName)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Add(
            //    new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
            //        "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
            Database.SetInitializer<ModelDB_PortalAgencia>(null);
            //Database.SetInitializer<ModelDB_PortalAgencia>(new MigrateDatabaseToLatestVersion<ModelDB_PortalAgencia, Configuration>());
            base.OnModelCreating(modelBuilder);
            
        }
        
        public System.Data.Entity.DbSet<NameCorrectionRequest> NameCorrectionRequest { get; set; }
        public System.Data.Entity.DbSet<Agency> Agencies { get; set; }
        public System.Data.Entity.DbSet<SecurityAttempt> SecurityAttempts { get; set; }


    }
}