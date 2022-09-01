using BackupDb.Models.File;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackupDb.Services.Contracts
{
    internal interface IUploadService
    {
        Task UploadFile(List<PhysicalFile> files);
    }
}
