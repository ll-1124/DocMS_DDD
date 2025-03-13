using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? AccountID(this ClaimsPrincipal principal) =>
       Guid.TryParse(principal.Claims.SingleOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier)?.Value, out var id) ? id : null;

        public static string? Email(this ClaimsPrincipal principal) =>
            principal.Claims.SingleOrDefault(cl => cl.Type == "EMAIL")?.Value;

        public static string? Department(this ClaimsPrincipal principal) =>
            principal.Claims.SingleOrDefault(cl => cl.Type == "DEPARTMENT")?.Value;
        public static string? Company(this ClaimsPrincipal principal) =>
            principal.Claims.SingleOrDefault(cl => cl.Type == "COMPANY")?.Value;
    }
}
