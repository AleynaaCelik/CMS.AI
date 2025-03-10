using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Varsayılan değer atama
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Varsayılan değer
        public string CreatedBy { get; set; } = "System"; // Varsayılan değer
        public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow; // Varsayılan değer
        public string LastModifiedBy { get; set; } = "System"; // Varsayılan değer
    }

}
