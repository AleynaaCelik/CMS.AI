using CMS.AI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Entities
{
    public class ContentMeta : BaseEntity
    {
        public Guid ContentId { get; private set; }
        public string Language { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public Content Content { get; private set; }

        private ContentMeta()
        {
            Language = string.Empty;
            Key = string.Empty;
            Value = string.Empty;
        }

        public ContentMeta(Guid contentId, string language, string key, string value, string createdBy)
        {
            Id = Guid.NewGuid();
            ContentId = contentId;
            Language = language;
            Key = key;
            Value = value;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }
    }

}
