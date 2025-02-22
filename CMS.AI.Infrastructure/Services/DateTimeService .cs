using CMS.AI.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Infrastructure.Services
{
    public sealed class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }

}
