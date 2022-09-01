using BackupDb.Configuration;
using BackupDb.Helpers;
using BackupDb.Services.Contracts;
using System;
using System.Threading;

namespace BackupDb.Flows
{

    internal class BackupFlow
    {
        private readonly IPhysicalFileService _physicalFileService;
        private readonly IUploadService _uploadService;
        private readonly IBackupService _backupService;
        private string _folderSql = AppConfig.GetFolderSql();

        public BackupFlow(IPhysicalFileService physicalFileService, IUploadService uploadService, IBackupService backupService)
        {
            _physicalFileService = physicalFileService;
            _uploadService = uploadService;
            _backupService = backupService; 
        }
        public async void FileUploadFlow()
        {
            var isContinue = true;
            while (isContinue)
            {
                LogService.Write("FLOW STARTED");

                var fileName = _backupService.CreatedBackup();

                if (!string.IsNullOrEmpty(fileName))
                {
                    _physicalFileService.CompressFile(fileName);

                    _physicalFileService.DeleteFilesFromPath(_folderSql);

                    var files = await _physicalFileService.GetFiles();

                    if (files.Count >= 1)
                    {
                        _uploadService.UploadFile(files);
                        _physicalFileService.DeleteFiles(files);
                    }
                    else
                        LogService.Write($"ERROR: {nameof(FileUploadFlow)} - folder is empty");
                }

                LogService.Write("FLOW FINISHED");

                Console.ReadKey();

                var sleep = 3600000 * 24;
                Thread.Sleep(sleep);
            }
        }
    }
}
