using Domain.Documents.Files;
using Domain.Documents.Folders;
using Domain.DocumentSharing.ShareRule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Files> Files { get;}
        DbSet<Folders> Folders { get;}
        DbSet<ShareRules> ShareRules { get;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
