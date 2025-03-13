using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateFolderDTO
    {
        public required string FolderName { get; set; }
        public Guid? ParentFolderId { get; set; }
    }
}
