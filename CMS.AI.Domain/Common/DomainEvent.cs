﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Domain.Common
{
    public abstract class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; }
    }
}
