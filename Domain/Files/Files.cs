using Domain.Common.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Files
{
    public class Files : AggregateRoot<FileId>
    {

        public required string FileName { get; set; }
        public required FolderId FolderId { get; set; }
        public required Metadata Metadata { get; set; }

        private readonly List<FileVersions> _fileVersions = new List<FileVersions>();
        private readonly List<FileShareRules> _fileShareRules = new List<FileShareRules>();


    }
}
