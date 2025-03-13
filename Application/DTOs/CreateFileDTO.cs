using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateFileDTO
    {
        public required string Name { get; set; }
        public Guid? ParentFolderId { get; set; }
        public IFormFile File { get; set; }
    }
}
