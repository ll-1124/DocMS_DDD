using Domain.Common.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DocumentManagements.Folders
{
    public class Folder : AggregateRoot<Guid>
    {
        public string FolderName { get; set; }
        public Guid? ParentFolderId { get; set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime LastModify { get; private set; }

        private readonly HashSet<Guid> _subFolderIds = [];

        public IReadOnlyCollection<Guid> SubFolderIds => _subFolderIds.ToList().AsReadOnly();

        private Folder()
        {
        }

        private Folder(string folderName, Guid? parentFolderId)
        {
            FolderName = folderName;
            ParentFolderId = parentFolderId;
            CreatedAt = DateTime.Now;
            LastModify = DateTime.Now;
        }

        public static Folder Create(string folderName, Guid? parentFolderId)
        {
            return new Folder(folderName, parentFolderId);
        }

        public void AddSubFolder(Guid folderId)
        {
            _subFolderIds.Add(folderId);
            LastModify = DateTime.Now;
        }

        public void MoveToFolder(Guid parentFolderId)
        {
            ParentFolderId = parentFolderId;
            LastModify = DateTime.Now;
        }

        public void Rename(string folderName)
        {
            FolderName = folderName;
            LastModify = DateTime.Now;
        }

        public void RemoveSubFolder(Guid folderId)
        {
            _subFolderIds.Remove(folderId);
            LastModify = DateTime.Now;
        }

    }
}
