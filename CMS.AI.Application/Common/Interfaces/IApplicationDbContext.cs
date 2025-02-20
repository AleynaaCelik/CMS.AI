using CMS.AI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Content> Contents { get; }
        DbSet<ContentMeta> ContentMetas { get; }
        DbSet<ContentVersion> ContentVersions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
