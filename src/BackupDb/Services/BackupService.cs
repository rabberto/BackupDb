using BackupDb.Configuration;
using BackupDb.Helpers;
using BackupDb.Services.Contracts;
using MySql.Data.MySqlClient;
using System;

namespace BackupDb.Services
{
    internal class BackupService : IBackupService
    {
        private readonly string _FolderSql = AppConfig.GetFolderSql();
        
        public string CreatedBackup()
        {
            LogService.Write($"START: {nameof(BackupService)}.{nameof(CreatedBackup)}");
            var fileName = CreatedFileName();
            var path = $"{_FolderSql}{fileName}.sql";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(AppConfig.GetConnectionString()))
                {
                    conn.Close();
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            LogService.Write($"SUCCESS: {nameof(BackupService)}.{nameof(CreatedBackup)} start backup");
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(path);
                            conn.Close();
                        }
                    }
                }
                return fileName;
            }
            catch (MySqlException exMySql)
            {
                LogService.Write($"ERROR: {nameof(BackupService)}.{nameof(CreatedBackup)} {exMySql.Message}");
                return null;
            }
            catch (Exception ex)
            {
                LogService.Write($"ERROR: {nameof(BackupService)}.{nameof(CreatedBackup)} {ex.Message}");
                return null;
            }  
        }

        private string CreatedFileName()
            => $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}_{DateTime.Now.ToLongTimeString().Replace(":", "")}";
    }
}
