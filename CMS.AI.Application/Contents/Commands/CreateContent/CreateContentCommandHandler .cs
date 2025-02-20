using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Contents.Commands.CreateContent
{
    public class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateContentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            var entity = Content.Create(
                request.Title,
                request.Body,
                _currentUserService.UserId
            );

            _context.Contents.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}
