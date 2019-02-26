namespace TodayInHistory.Entity
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TodayInHistory.Model;

    public class HistoryContext : DbContext
    {
        public HistoryContext(): base("name=History")
        {
            //让EF不再生成数据库表（也不再生成 __MigrationHistory）
            Database.SetInitializer<HistoryContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<History> Historys { get; set; }
    }
}