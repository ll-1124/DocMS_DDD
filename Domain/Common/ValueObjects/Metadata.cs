using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.ValueObjects
{
    public record Metadata : ValueObject
    {
        public long Size { get; }
        public string ContentType { get; }
        public DateTime CreatedAt { get; }

        public Metadata(long size, string contentType, DateTime createdAt)
        {
            Size = size;
            ContentType = contentType;
            CreatedAt = createdAt;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Size;
            yield return ContentType;
            yield return CreatedAt;
        }
    }
}
