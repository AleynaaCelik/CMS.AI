using CMS.AI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Entities
{
    public class ContentVersion : BaseEntity
    {
        public Guid ContentId { get; private set; }
        public int Version { get; private set; }
        public string Content { get; private set; }
        public Content BaseContent { get; private set; }

        private ContentVersion()
        {
            Content = string.Empty;
        }

        public ContentVersion(Guid contentId, int version, string content, string createdBy)
        {
            Id = Guid.NewGuid();
            ContentId = contentId;
            Version = version;
            Content = content;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }
    }
}
