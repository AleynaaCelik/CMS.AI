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
        public Guid ContentId { get; set; }
        public string Language { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Content Content { get; set; }
    }

}
