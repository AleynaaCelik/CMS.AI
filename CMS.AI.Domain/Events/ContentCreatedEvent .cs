using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Events
{
    public class ContentCreatedEvent : DomainEvent
    {
        public Guid ContentId { get; }

        public ContentCreatedEvent(Guid contentId)
        {
            ContentId = contentId;
        }
    }
}
