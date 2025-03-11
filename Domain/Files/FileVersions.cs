using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Files
{
    public class FileVersions
    {
        public Guid Id { get; private set; }
        public Guid FileId { get; private set; }
        [Required] public string? Name { get; private set; }
        [Required] public string? Path { get; private set; }
        public long Size { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public Guid CreatedBy { get; private set; }
        public Guid UpdatedBy { get; private set; }
        public Guid? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }
    }
}
