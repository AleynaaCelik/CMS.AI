using CMS.AI.Domain.Common;
using CMS.AI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMS.AI.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ILogger<ApplicationDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        public DbSet<Content> Contents { get; set; }
        public DbSet<ContentMeta> ContentMetas { get; set; }
        public DbSet<ContentVersion> ContentVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Modeller için özel yapılandırmalar eklenebilir
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = now;
                        entry.Entity.CreatedBy = entry.Entity.CreatedBy ?? "System"; // Varsayılan değer
                        entry.Entity.LastModifiedAt = now;
                        entry.Entity.LastModifiedBy = entry.Entity.LastModifiedBy ?? "System"; // Varsayılan değer
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = now;
                        entry.Entity.LastModifiedBy = entry.Entity.LastModifiedBy ?? "System"; // Varsayılan değer
                        break;
                }
            }

            _logger.LogInformation($"Database operation executed at {DateTime.Now}");
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}