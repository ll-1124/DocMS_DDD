using Domain.Common.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Files
{
    public class FileShareRules
    {
        public int ID { get; set; }        
        public required FileId FileId { get; init; }
        public required ShareType Type { get; set; }
        public required AccessScope Scope { get; set; }
        public required string Value { get; set; }
    }
}
