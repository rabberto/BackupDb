using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace BackupDb.Configuration
{
    internal static class AppConfig
    {
        public static string Env { get { return GetAppSettings().GetValues(nameof(Env)).FirstOrDefault(); } }
        public static string Folder { get { return GetAppSettings().GetValues(nameof(Folder)).FirstOrDefault(); } }
        public static string Address { get { return GetAppSettings().GetValues(nameof(Address)).FirstOrDefault(); } }
        public static string AccessKey { get { return GetAppSettings().GetValues(nameof(AccessKey)).FirstOrDefault(); } }
        public static string SecretKey { get { return GetAppSettings().GetValues(nameof(SecretKey)).FirstOrDefault(); } }
        public static string BucketName { get { return GetAppSettings().GetValues(nameof(BucketName)).FirstOrDefault(); } }
        public static string DbServer { get { return GetAppSettings().GetValues(nameof(DbServer)).FirstOrDefault(); } }
        public static string DbUser { get { return GetAppSettings().GetValues(nameof(DbUser)).FirstOrDefault(); } }
        public static string DbPassword { get { return GetAppSettings().GetValues(nameof(DbPassword)).FirstOrDefault(); } }
        public static string DbName { get { return GetAppSettings().GetValues(nameof(DbName)).FirstOrDefault(); } }

        private static NameValueCollection GetAppSettings()
            => ConfigurationManager.AppSettings;

        public static string GetConnectionString()
            => $"server={DbServer};user={DbUser};pwd={DbPassword};database={DbName};charset=utf8;convertzerodatetime=true;";

        public static string GetFolderSql()
            => $"{Folder}sql\\";
        public static string GetFolderLog()
            => $"{Folder}logs\\";
        public static string GetFolderSubmit()
            => $"{Folder}Submit\\";

    }
}
