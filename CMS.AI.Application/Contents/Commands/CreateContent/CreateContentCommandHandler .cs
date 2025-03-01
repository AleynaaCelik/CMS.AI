using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Domain.Entities;
using MediatR;
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

        public CreateContentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Content(
                request.Title,
                request.Body,
                "admin" // Kullanıcı kimliği için sabit değer kullanıyoruz
            );

            _context.Contents.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

    }

}
