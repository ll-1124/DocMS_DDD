using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DocumentSharing.ShareRequests
{
    public class ShareRequest : Entity<Guid>
    {
        public Guid ResourceId { get; private set; }
        public Guid Requester { get; private set; }
        public DateTime RequestAt { get; private set; }
        public string? RequestMessage { get; private set; }
        public string? OwnerMessage { get; private set; }

        public ResourceType ResourceType { get; private set; }
        public AccessScope RequestScope { get; private set; }
        public ShareType RequestShareType { get; private set; }
        public ShareRequestStatus Status { get; private set; }

        private ShareRequest()
        {
        }

    }
}
