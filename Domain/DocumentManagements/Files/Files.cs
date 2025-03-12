using Domain.Common.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Files
{
    public class Files : AggregateRoot<FileId>
    {

        public string FileName { get; set; }
        public FolderId ParentFolderId { get; set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime LastModify { get; private set; }

        private readonly List<FileVersions> _fileVersions = new List<FileVersions>();

        public IReadOnlyCollection<FileVersions> FileVersions => _fileVersions.AsReadOnly();

        private Files(string fileName, FolderId folderId, DateTime createdAt, DateTime lastModify)
        {
            FileName = fileName;
            ParentFolderId = folderId;
            CreatedAt = createdAt;
            LastModify = lastModify;
        }

        public static Files Create(string fileName, FolderId folderId, DateTime createdAt, DateTime lastModify)
        {
            return new Files(fileName, folderId, createdAt, lastModify);
        }

        public void AddFileVersion(FileVersions fileVersions)
        {
            _fileVersions.Add(fileVersions);
            LastModify = DateTime.Now;
        }

        public void RemoveFileVersion(FileVersions fileVersions)
        {
            _fileVersions.Remove(fileVersions);
            LastModify = DateTime.Now;
        }

        public void ClearFileVersions()
        {
            _fileVersions.Clear();
            LastModify = DateTime.Now;
        }

        public void Rename(string fileName)
        {
            FileName = fileName;
            LastModify = DateTime.Now;
        }

        public void MoveToFolder(FolderId folderId)
        {
            ParentFolderId = folderId;
            LastModify = DateTime.Now;
        }
    }
}
