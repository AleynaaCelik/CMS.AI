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
        public Guid ContentId { get; set; }
        public int Version { get; set; }
        public string Content { get; set; }
        public Content BaseContent { get; set; }
    }
}
