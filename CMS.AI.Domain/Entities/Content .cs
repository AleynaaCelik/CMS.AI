using CMS.AI.Domain.Common;
using CMS.AI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Entities
{
    public class Content : BaseEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Body { get; private set; }
        public ContentStatus Status { get; private set; }
        public int Version { get; private set; }
        private readonly List<ContentMeta> _metaData;
        private readonly List<ContentVersion> _versions;

        public IReadOnlyCollection<ContentMeta> MetaData => _metaData.AsReadOnly();
        public IReadOnlyCollection<ContentVersion> Versions => _versions.AsReadOnly();

        public Content(string title, string body, string createdBy)
        {
            Id = Guid.NewGuid();
            Title = title;
            Body = body;
            Slug = GenerateSlug(title);
            Status = ContentStatus.Draft;
            Version = 1;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            _metaData = new List<ContentMeta>();
            _versions = new List<ContentVersion>();

            AddVersion(Version, body, createdBy);
        }

        // For EF Core
        private Content()
        {
            _metaData = new List<ContentMeta>();
            _versions = new List<ContentVersion>();
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
            var contentVersion = new ContentVersion(Id, version, content, createdBy);
            _versions.Add(contentVersion);
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