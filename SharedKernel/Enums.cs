using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{

    public enum ResourceType
    {
        File = 1 << 0,
        Folder = 1 << 1
    }

    public enum ShareType
    {
        Role = 1 << 0,
        Account = 1 << 1,
        Email = 1 << 2,
        Link = 1 << 3,
        Department = 1 << 4,
        Company = 1 << 5,
        Public = 1 << 6
    }

    public enum AccessScope
    {
        Viewer = 1 << 0,
        Downloader = 1 << 1,
        Editor = 1 << 2,
        Owner = 1 << 3
    }

    public enum ShareRequestStatus
    {
        Pending = 1 << 0,
        Accepted = 1 << 1,
        Rejected = 1 << 2,
    }
}
