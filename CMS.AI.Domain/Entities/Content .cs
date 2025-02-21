using CMS.AI.Domain.Common;
using CMS.AI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Entities
{
    public class Content : BaseEntity
    {
        private readonly List<ContentMeta> _metaData;
        private readonly List<ContentVersion> _versions;

        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Body { get; private set; }
        public ContentStatus Status { get; private set; }
        public int Version { get; private set; }

        public IReadOnlyCollection<ContentMeta> MetaData => _metaData.AsReadOnly();
        public IReadOnlyCollection<ContentVersion> Versions => _versions.AsReadOnly();

        // Private constructor for EF Core
        private Content()
        {
            _metaData = new List<ContentMeta>();
            _versions = new List<ContentVersion>();
            Title = string.Empty;
            Slug = string.Empty;
            Body = string.Empty;
        }

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

        public void AddMetaData(string language, string key, string value, string createdBy)
        {
            var meta = new ContentMeta(Id, language, key, value, createdBy);
            _metaData.Add(meta);
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