using CMS.AI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Contents.DTOs
{
    public class ContentDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public ContentStatus Status { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public ICollection<ContentMetaDto> MetaData { get; set; } = new List<ContentMetaDto>(); 
    }
}
