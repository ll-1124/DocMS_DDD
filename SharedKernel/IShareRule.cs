using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public interface IShareRule
    {
        bool IsEligible(ClaimsPrincipal principal);
    }
}
