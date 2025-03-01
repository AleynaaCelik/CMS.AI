using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.AI.Domain.Entities;

namespace CMS.AI.Infrastructure.Persistance.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Slug)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Body)
                .IsRequired();

            builder.HasMany(x => x.MetaData)
                .WithOne(x => x.Content)
                .HasForeignKey(x => x.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Versions)
                .WithOne(x => x.BaseContent)
                .HasForeignKey(x => x.ContentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
