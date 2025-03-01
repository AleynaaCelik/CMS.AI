using AutoMapper;
using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Application.Contents.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Contents.Queries.GetContents
{
    public class GetContentsQueryHandler : IRequestHandler<GetContentsQuery, IEnumerable<ContentDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetContentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContentDto>> Handle(GetContentsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Contents
                .Include(c => c.MetaData)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x => x.Title.Contains(request.SearchTerm) ||
                                       x.Body.Contains(request.SearchTerm));
            }

            if (request.Status.HasValue)
            {
                query = query.Where(x => x.Status == request.Status.Value);
            }

            var contents = await query.ToListAsync(cancellationToken);

            return contents.Select(c => new ContentDto
            {
                Id = c.Id,
                Title = c.Title,
                Slug = c.Slug,
                Body = c.Body,
                Status = c.Status,
                Version = c.Version,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                LastModifiedAt = c.LastModifiedAt,
                LastModifiedBy = c.LastModifiedBy,
                MetaData = c.MetaData.Select(m => new ContentMetaDto
                {
                    Id = m.Id,
                    ContentId = m.ContentId,
                    Language = m.Language,
                    Key = m.Key,
                    Value = m.Value
                }).ToList()
            });
        }
    }
}


