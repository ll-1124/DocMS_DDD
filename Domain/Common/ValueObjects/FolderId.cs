using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.ValueObjects
{
    public record FolderId(Guid Value)
    {
        public static FolderId New() => new(Guid.NewGuid());
    }
}
