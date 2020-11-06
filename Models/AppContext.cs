using System.Data.Entity;

namespace Perusedit.Models
{
    public class AppContext : DbContext
    {
        public AppContext() : base("DBConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppContext,
            Perusedit.Migrations.Configuration>("DBConnectionString"));

        }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}
