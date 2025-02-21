using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Contents.DTOs
{
    public class ContentMetaDto
    {
        public Guid Id { get; set; }
        public Guid ContentId { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
