using Application.Abstractions;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DocumentManagements.Folders
{
    public record CreateFolderCommand(CreateFolderDTO createFolderDTO) : ICommand<Guid> { }
}
