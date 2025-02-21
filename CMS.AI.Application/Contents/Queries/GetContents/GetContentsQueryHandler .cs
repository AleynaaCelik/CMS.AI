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
        private readonly IMapper _mapper;

        public GetContentsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            return _mapper.Map<IEnumerable<ContentDto>>(contents);
        }
    }
}


