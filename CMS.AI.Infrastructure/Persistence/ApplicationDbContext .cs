using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ILogger<ApplicationDbContext> logger)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<Content> Contents { get; set; } = null!;
        public DbSet<ContentMeta> ContentMetas { get; set; } = null!;
        public DbSet<ContentVersion> ContentVersions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Loglama eklenebilir
            _logger.LogInformation("Database operation executed at {time}", DateTime.UtcNow);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}