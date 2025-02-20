using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Events
{
    public class ContentUpdatedEvent : DomainEvent
    {
        public Guid ContentId { get; }
        public int Version { get; }

        public ContentUpdatedEvent(Guid contentId, int version)
        {
            ContentId = contentId;
            Version = version;
        }
    }
}
