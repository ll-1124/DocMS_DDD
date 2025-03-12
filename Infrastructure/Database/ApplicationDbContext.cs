using Application.Abstractions.Data;
using Domain.Common.ValueObjects;
using Domain.Documents.Files;
using Domain.Documents.Folders;
using Domain.DocumentSharing.ShareRule;
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
        public DbSet<Files> Files { get ; set ; }
        public DbSet<Folders> Folders { get ; set ; }
        public DbSet<ShareRules> ShareRules { get ; set ; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // Cấu hình Folder
            modelBuilder.Entity<Folders>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Folders>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<Folders>()
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
                .HasOne<Folders>()
                .WithMany()
                .HasForeignKey(f => f.ParentFolderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình ShareRules
            modelBuilder.Entity<ShareRules>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<ShareRules>()
                .Property(s => s.Type)
                .HasConversion<int>(); // Lưu enum dưới dạng int

            modelBuilder.Entity<ShareRules>()
                .Property(s => s.Scope)
                .HasConversion<int>(); // Lưu enum dưới dạng int

            // Cấu hình FileVersions
            modelBuilder.Entity<FileVersions>()
                .HasKey(fv => fv.Id);

            modelBuilder.Entity<FileVersions>()
                .Property(fv => fv.CreatedAt)
                .HasDefaultValueSql("NOW()");

            modelBuilder.Entity<FileVersions>()
                .Property(fv => fv.UpdatedAt)
                .HasDefaultValueSql("NOW()");

            // Cấu hình Metadata là Owned Type
            modelBuilder.Entity<FileVersions>()
                .OwnsOne(fv => fv.Metadata, metadata =>
                {
                    metadata.Property(m => m.Size).IsRequired();
                    metadata.Property(m => m.ContentType).IsRequired().HasMaxLength(255);
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
