using Application.Abstractions;
using Application.Abstractions.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DocumentManagements.Folders
{
    public class CreateFolderCommandHandler : ICommandHandler<CreateFolderCommand, Guid>
    {

        private readonly IApplicationDbContext _context;

        public CreateFolderCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Guid> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            var folder = Domain.Documents.Folders.Folders.Create(request.createFolderDTO.FolderName, request.createFolderDTO.ParentFolderId);

            _context.Folders.Add(folder);

            _context.SaveChangesAsync(cancellationToken);

            return Task.FromResult(folder.Id);
        }
    }
}
