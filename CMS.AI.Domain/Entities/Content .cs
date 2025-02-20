using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Entities
{
    public class Content : BaseAuditableEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Body { get; private set; }
        public ContentStatus Status { get; private set; }
        public int Version { get; private set; }
        public List<ContentMeta> MetaData { get; private set; }
        public List<ContentVersion> Versions { get; private set; }

        private Content()
        {
            MetaData = new List<ContentMeta>();
            Versions = new List<ContentVersion>();
        }

        public static Content Create(string title, string body, string createdBy)
        {
            var content = new Content
            {
                Id = Guid.NewGuid(),
                Title = title,
                Slug = GenerateSlug(title),
                Body = body,
                Status = ContentStatus.Draft,
                Version = 1,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            content.AddVersion(content.Version, body, createdBy);
            return content;
        }

        public void Update(string title, string body, string modifiedBy)
        {
            Title = title;
            Body = body;
            Slug = GenerateSlug(title);
            Version++;
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = modifiedBy;

            AddVersion(Version, body, modifiedBy);
        }

        private void AddVersion(int version, string content, string createdBy)
        {
            var contentVersion = new ContentVersion
            {
                Id = Guid.NewGuid(),
                Version = version,
                Content = content,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            Versions.Add(contentVersion);
        }

        private static string GenerateSlug(string title)
        {
            return title.ToLower()
                       .Replace(" ", "-")
                       .Replace(System.Text.RegularExpressions.Regex.Replace(title, "[^a-zA-Z0-9 ]", ""), "");
        }
    }

    public enum ContentStatus
    {
        Draft,
        Published,
        Archived,
        UnderReview
    }
}