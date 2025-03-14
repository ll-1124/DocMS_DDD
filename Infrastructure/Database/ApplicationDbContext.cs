using Application.Abstractions.Data;
using Domain.Common.ValueObjects;
using Domain.DocumentManagements.Folders;
using File = Domain.DocumentManagements.Files.File;
using Domain.DocumentSharing.ShareRequests;
using Domain.DocumentSharing.ShareRules;
using Domain.UserManagements.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Infrastructure.Database
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : DbContext(options), IApplicationDbContext
    {
        public DbSet<File> Files { get ; set ; }
        public DbSet<Folder> Folders { get ; set ; }
        public DbSet<ShareRule> ShareRules { get ; set ; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // Cấu hình Folder
            modelBuilder.Entity<Folder>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Folder>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Folder>()
                .HasOne<Folder>()
                .WithMany()
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Folder>()
                .Property(f => f.LastModify)
                .HasDefaultValueSql("NOW()");

            // Cấu hình File
            modelBuilder.Entity<Files>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Files>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Files>()
                .Property(f => f.LastModify)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Files>()
                .HasOne<Folder>()
                .WithMany()
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình ShareRules
            modelBuilder.Entity<ShareRule>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<ShareRule>()
                .Property(s => s.ResourceId).IsRequired();

            modelBuilder.Entity<ShareRule>()
               .Property(s => s.Value).IsRequired()
               .HasMaxLength(50);

            modelBuilder.Entity<ShareRule>()
                .Property(s => s.Type)
                .HasConversion<int>(); // Lưu enum dưới dạng int

            modelBuilder.Entity<ShareRule>()
                .Property(s => s.Scope)
                .HasConversion<int>(); // Lưu enum dưới dạng int

            modelBuilder.Entity<ShareRule>()
                .Property(s => s.ResourceType)
                .HasConversion<int>(); // Lưu enum dưới dạng int

            modelBuilder.Entity<ShareRule>()
                .Property(fv => fv.Expiration);

            // Cấu hình ShareRules
            modelBuilder.Entity<ShareRequest>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<ShareRequest>()
                .Property(s => s.ResourceId).IsRequired();

            modelBuilder.Entity<ShareRequest>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(f => f.Requester)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShareRequest>()
                .Property(s => s.RequestMessage)
                .HasMaxLength(250); 

            modelBuilder.Entity<ShareRequest>()
                .Property(s => s.OwnerMessage)
                .HasMaxLength(250);  

            modelBuilder.Entity<ShareRequest>()
                .Property(s => s.ResourceType)
                .HasConversion<int>();

            modelBuilder.Entity<ShareRequest>()
                .Property(fv => fv.RequestScope)
                .HasConversion<int>();

            modelBuilder.Entity<ShareRequest>()
                .Property(fv => fv.RequestShareType)
                .HasConversion<int>();

            modelBuilder.Entity<ShareRequest>()
                .Property(fv => fv.Status)
                .HasConversion<int>();

            modelBuilder.Entity<ShareRequest>()
                .Property(fv => fv.RequestAt)
                .HasDefaultValueSql("NOW()");

            // Cấu hình FileVersions
            modelBuilder.Entity<FileVersions>()
                .HasKey(fv => fv.Id);

            modelBuilder.Entity<FileVersions>()
                .Property(fv => fv.CreatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<FileVersions>()
                .HasOne<Files>()
                .WithMany()
                .HasForeignKey(f => f.FileId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FileVersions>()
                .Property(fv => fv.UpdatedAt)
                .HasDefaultValueSql("NOW()");

            // Cấu hình Metadata là Owned Type
            modelBuilder.Entity<FileVersions>()
                .OwnsOne(fv => fv.Metadata, metadata =>
                {
                    metadata.Property(m => m.Size).IsRequired();
                    metadata.Property(m => m.ContentType).IsRequired().HasMaxLength(50);
                });

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await PublishEvents();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private async Task PublishEvents()
        {
            var events = ChangeTracker.Entries<Entity<Guid>>()
                .Where(x => x.Entity is Entity<Guid> && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted))
                .Select(x => x.Entity)
                .SelectMany(entity =>
                {
                    IReadOnlyCollection<IDomainEvent> domainEvents = entity.DomainEvents;

                    entity.ClearDomainEvents();

                    return domainEvents;

                }).ToList();

            foreach (var domainEvent in events)
            {
                await publisher.Publish(domainEvent);
            }
        }
    }
}
