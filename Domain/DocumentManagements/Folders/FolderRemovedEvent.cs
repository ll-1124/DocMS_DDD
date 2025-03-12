using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DocumentManagements.Folders
{
    public sealed record FolderRemovedEvent(Guid FolderId) : IDomainEvent
    {
    }
}
