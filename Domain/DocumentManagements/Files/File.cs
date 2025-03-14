using Domain.Common.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DocumentManagements.Files
{
    public class File : AggregateRoot<Guid>
    {

        public string FileName { get; set; }
        public Guid ParentFolderId { get; set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime LastModify { get; private set; }

        private readonly List<FileVersion> _FileVersion = new List<FileVersion>();

        public IReadOnlyCollection<FileVersion> FileVersion => _FileVersion.AsReadOnly();

        private File()
        {
        }

        private File(string fileName, Guid parentFolderId, DateTime createdAt, DateTime lastModify)
        {
            FileName = fileName;
            ParentFolderId = parentFolderId;
            CreatedAt = createdAt;
            LastModify = lastModify;
        }

        public static File Create(string fileName, Guid parentFolderId, DateTime createdAt, DateTime lastModify)
        {
            return new File(fileName, parentFolderId, createdAt, lastModify);
        }

        public void AddFileVersion(FileVersion FileVersion)
        {
            _FileVersion.Add(FileVersion);
            LastModify = DateTime.Now;
        }

        public void RemoveFileVersion(FileVersion FileVersion)
        {
            _FileVersion.Remove(FileVersion);
            LastModify = DateTime.Now;
        }

        public void ClearFileVersion()
        {
            _FileVersion.Clear();
            LastModify = DateTime.Now;
        }

        public void Rename(string fileName)
        {
            FileName = fileName;
            LastModify = DateTime.Now;
        }

        public void MoveToFolder(Guid folderId)
        {
            ParentFolderId = folderId;
            LastModify = DateTime.Now;
        }
    }
}
