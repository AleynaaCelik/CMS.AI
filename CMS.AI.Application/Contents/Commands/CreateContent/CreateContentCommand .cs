using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Contents.Commands.CreateContent
{
    public class CreateContentCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
