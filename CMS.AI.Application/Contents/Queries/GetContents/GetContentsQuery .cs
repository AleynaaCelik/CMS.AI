using CMS.AI.Application.Contents.DTOs;
using CMS.AI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Contents.Queries.GetContents
{
    public class GetContentsQuery : IRequest<IEnumerable<ContentDto>>
    {

        public string? SearchTerm { get; init; }
        public ContentStatus? Status { get; init; }
    }
}
