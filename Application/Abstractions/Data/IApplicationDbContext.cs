using Domain.DocumentManagements.Folders;
using Domain.DocumentManagements.Files;
using Domain.DocumentSharing.ShareRules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.DocumentManagements.Files.File;

namespace Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<File> Files { get;}
        DbSet<Folder> Folders { get;}
        DbSet<ShareRule> ShareRules { get;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
