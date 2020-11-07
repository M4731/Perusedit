using System.Data.Entity;
namespace Perusedit.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DBConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Perusedit.Migrations.Configuration>("DBConnectionString"));

        }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}
