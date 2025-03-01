// CMS.AI.Infrastructure/Persistence/ApplicationDbContextFactory.cs
using CMS.AI.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CMS.AI.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Docker �zerindeki SQL Server ba�lant�s� - sizin docker-compose'da belirtti�iniz bilgiler
            var connectionString = "Server=localhost,1433;Database=CMS.AI;User Id=sa;Password=Sql.12345678;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}