using BackupDb.Configuration;
using BackupDb.Helpers;
using BackupDb.Models.File;
using BackupDb.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace BackupDb.Services
{
    internal class PhysicalFileService : IPhysicalFileService
    {
        private string _folderSubmit = AppConfig.GetFolderSubmit();
        private string _folderSql = AppConfig.GetFolderSql();

        public async Task<List<PhysicalFile>> GetFiles()
        {
            var files = new List<PhysicalFile>();
            try
            {
                foreach (string path in Directory.GetFiles(_folderSubmit))
                    files.Add(new PhysicalFile(await GetName(path), path, Convert.ToBase64String(File.ReadAllBytes(path))));

                LogService.Write($"SUCCESS: {nameof(PhysicalFileService)}.{nameof(GetFiles)} - total files: {files.Count}");
                return files;
            }
            catch (Exception ex)
            {
                LogService.Write($"ERROR: {nameof(PhysicalFileService)}.{nameof(GetFiles)} - {ex.Message}");
                return null;
            }
        }

        public void DeleteFilesFromPath(string path)
        {
            try
            {
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                    DeleteFile(file);
            }
            catch (Exception ex)
            {
                LogService.Write($"ERROR: {nameof(PhysicalFileService)}.{nameof(DeleteFilesFromPath)} - {ex.Message}");
            }
        }

        public void DeleteFiles(List<PhysicalFile> files)
        {
            try
            {
                foreach (var file in files)
                    DeleteFile(file.Path);
            }
            catch (Exception ex)
            {
                LogService.Write($"ERROR: {nameof(PhysicalFileService)}.{nameof(DeleteFiles)} - {ex.Message}");
            }
        }

        public void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                LogService.Write($"SUCCESS: {nameof(PhysicalFileService)}.{nameof(DeleteFile)} - file: {file} deleted");
            }
            else
                LogService.Write($"ERROR: {nameof(PhysicalFileService)}.{nameof(DeleteFile)} - file: {file} not found");
        }

        public void CompressFile(string fileName)
        {
            try
            {
                if (!Directory.Exists(_folderSubmit))
                    Directory.CreateDirectory(_folderSubmit);

                ZipFile.CreateFromDirectory(_folderSql, $"{_folderSubmit}{fileName}.zip");

                LogService.Write($"SUCCESS: {nameof(PhysicalFileService)}.{nameof(CompressFile)} - successfully zipped file");
            }
            catch (IOException exIO)
            {
                LogService.Write($"ERROR: {nameof(PhysicalFileService)}.{nameof(CompressFile)} - {exIO.Message}");
            }
            catch (Exception ex)
            {
                LogService.Write($"ERROR: {nameof(PhysicalFileService)}.{nameof(CompressFile)} - {ex.Message}");
            }
        }
  
        private Task<string> GetName(string path)
        {
            var split = path.Split('\\');
            return Task.FromResult(split[split.Length - 1].ToString());
        }
    }
}
