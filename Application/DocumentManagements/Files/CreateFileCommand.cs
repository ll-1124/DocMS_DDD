using Application.Abstractions;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DocumentManagements.Files
{
    public record CreateFileCommand(CreateFileDTO createFileDTO) : ICommand<Guid>
    {
    }
}
