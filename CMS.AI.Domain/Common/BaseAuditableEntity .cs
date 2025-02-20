using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
    }

}
