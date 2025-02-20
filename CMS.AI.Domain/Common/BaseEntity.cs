using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }

}
