using CMS.AI.Domain.Common;
using System;

namespace CMS.AI.Domain.Entities
{
    public class ContentMeta : BaseEntity
    {
        // Private constructor for EF Core
        private ContentMeta()
        {
            Key = string.Empty;
            Value = string.Empty;
            Language = "en-US";
        }

        public ContentMeta(Guid contentId, string language, string key, string value, string createdBy)
        {
            Id = Guid.NewGuid();
            ContentId = contentId;
            Language = language;
            Key = key;
            Value = value;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
            LastModifiedBy = createdBy;
            LastModifiedAt = DateTime.UtcNow;
        }

        public Guid ContentId { get; private set; }
        public Content? Content { get; private set; } // Navigation property, nullable
        public string Key { get; private set; }
        public string Value { get; private set; }
        public string Language { get; private set; }
    }
}