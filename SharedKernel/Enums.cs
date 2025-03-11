using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public enum ShareType
    {
        Role = 1 << 0,
        Account = 1 << 1,
        Email = 1 << 2,
        Link = 1 << 3,
        Department = 1 << 4,
        DepartmentEditor = 1 << 5,
        Company = 1 << 6,
        CompanyEditor = 1 << 7,
        CompanyOwner = 1 << 8,
        AccountPublic = 1 << 9,
    }

    public enum AccessScope
    {
        Viewer = 1 << 0,
        Download = 1 << 1,
        Editor = 1 << 2,
        Owner = 1 << 3
    }
}
