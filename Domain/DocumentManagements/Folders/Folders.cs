using Domain.Common.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Folders
{
    public class Folders : AggregateRoot<FolderId>
    {
        public string FolderName { get; set; }
        public FolderId ParentFolderId { get; set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime LastModify { get; private set; }

        private readonly HashSet<FolderId> _subFolderIds = [];

        public IReadOnlyCollection<FolderId> SubFolderIds => _subFolderIds.ToList().AsReadOnly();

        private Folders(string folderName, FolderId parentFolderId)
        {
            FolderName = folderName;
            ParentFolderId = parentFolderId;
        }

        public static Folders Create(string folderName, FolderId parentFolderId)
        {
            return new Folders(folderName, parentFolderId);
        }

        public void AddSubFolder(FolderId folderId)
        {
            _subFolderIds.Add(folderId);
            LastModify = DateTime.Now;
        }

        public void MoveToFolder(FolderId parentFolderId)
        {
            ParentFolderId = parentFolderId;
            LastModify = DateTime.Now;
        }

        public void Rename(string folderName)
        {
            FolderName = folderName;
            LastModify = DateTime.Now;
        }

        public void RemoveSubFolder(FolderId folderId)
        {
            _subFolderIds.Remove(folderId);
            LastModify = DateTime.Now;
        }

    }
}
