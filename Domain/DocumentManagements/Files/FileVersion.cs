using Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DocumentManagements.Files
{
    public class FileVersion
    {
        public Guid Id { get; private set; }
        public Guid FileId { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public Guid CreatedBy { get; private set; }
        public Guid UpdatedBy { get; private set; }
        public Guid? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }
        public Metadata Metadata { get; private set; }

        private FileVersion()
        {
        }

        public FileVersion(Guid fileId, string name, string path, Metadata metadata, Guid createdBy)
        {
            Id = Guid.NewGuid();
            FileId = fileId;
            Name = name;
            Path = path;
            Metadata = metadata;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            CreatedBy = createdBy;
            UpdatedBy = createdBy;
            IsDeleted = false;
        }
    }
}
