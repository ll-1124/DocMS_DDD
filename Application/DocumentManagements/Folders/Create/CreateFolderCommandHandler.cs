using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DocumentManagements.Folders;
using Folder = Domain.DocumentManagements.Folders.Folder;


namespace Application.DocumentManagements.Folders.Create
{

    internal sealed class CreateFolderCommandHandler(IApplicationDbContext _context) : ICommandHandler<CreateFolderCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            var folder = Folder.Create(request.createFolderDTO.FolderName, request.createFolderDTO.ParentFolderId);

            folder.AddDomainEvent(new FolderCreatedEvent(folder.Id));

            _context.Folders.Add(folder);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(folder.Id);
        }
    }

}
