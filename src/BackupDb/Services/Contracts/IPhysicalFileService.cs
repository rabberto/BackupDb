using BackupDb.Models.File;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackupDb.Services.Contracts
{
    internal interface IPhysicalFileService
    {
        Task<List<PhysicalFile>> GetFiles();
        void DeleteFiles(List<PhysicalFile> files);
        void DeleteFile(string files);
        void CompressFile(string path);
        void DeleteFilesFromPath(string path);
    }
}
