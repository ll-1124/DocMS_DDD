using Domain.Common.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DocumentSharing.ShareRule
{
    public class ShareRules : Entity<Guid>
    {
        public Guid ResourceId { get; private set; }
        public ShareType Type { get; private set; }
        public AccessScope Scope { get; private set; }
        public string Value { get; private set; }

        private ShareRules()
        {
        }

        private ShareRules(Guid resourceId, ShareType type, AccessScope scope, string value)
        {
            ResourceId = resourceId;
            Type = type;
            Scope = scope;
            Value = value;
        }

        public static ShareRules Create(Guid resourceId, ShareType type, AccessScope scope, string value)
        {
            return new ShareRules(resourceId, type, scope, value);
        }

        public void Update(ShareType type, AccessScope scope, string value)
        {
            Type = type;
            Scope = scope;
            Value = value;
        }
    }
}
