
using MySql.Data.Entity;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CoinsAlert.Model
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {
        }

        // Constructor to use on a DbConnection that is already opened
        public AppDbContext(DbConnection existingConnection, bool contextOwnsConnection)
          : base(existingConnection, contextOwnsConnection)
        {

        }

        public virtual DbSet<KuCoin> KuCoin { get; set; }

        public virtual DbSet<binance> Binance { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }



}
}
