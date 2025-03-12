using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DocumentManagements.Files
{
    public sealed record FileCreatedEvent(Guid FileId) : IDomainEvent
    {

    }
}
