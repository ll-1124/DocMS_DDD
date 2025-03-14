using Domain.Common.Extensions;
using SharedKernel;
using System.Security.Claims;

namespace Domain.DocumentSharing.ShareRules
{
    public class ShareRule : Entity<Guid>, IShareRule
    {
        public Guid ResourceId { get; private set; }
        public ResourceType ResourceType { get; private set; }
        public ShareType Type { get; private set; }
        public AccessScope Scope { get; private set; }
        public string Value { get; private set; }
        public DateTime? Expiration {  get; private set; }

        private ShareRule()
        {
        }

        private ShareRule(Guid resourceId, ShareType type, AccessScope scope, string value, DateTime? expriration)
        {
            ResourceId = resourceId;
            Type = type;
            Scope = scope;
            Value = value;
            Expiration = expriration;
        }

        public static ShareRule Create(Guid resourceId, ShareType type, AccessScope scope, string value, DateTime? expriration)
        {
            return new ShareRule(resourceId, type, scope, value, expriration);
        }

        public void Update(ShareType type, AccessScope scope, string value, DateTime? expriration)
        {
            Type = type;
            Scope = scope;
            Value = value;
            Expiration = expriration;
        }

        public void Remove()
        {
            Expiration = DateTime.Now;
        }

        private bool IsExpired()
        {
            return Expiration.HasValue && Expiration <= DateTime.Now;
        }

        public bool IsEligible(ClaimsPrincipal principal)
        {
            return IsExpired() switch
            {
                true => false,
                false => Type switch
                {
                    ShareType.Link => true,
                    ShareType.Role => principal?.IsInRole(Value) ?? false,
                    ShareType.Account => principal?.AccountID()?.ToString() == Value,
                    ShareType.Email => principal?.Email() == Value,
                    ShareType.Department => principal?.Department() == Value,
                    ShareType.Company => principal?.Company() == Value,
                    ShareType.Public => "Share With Everyone" == Value,
                    _ => throw new ArgumentOutOfRangeException()
                }
            };
        }


    }
}
