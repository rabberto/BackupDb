using BackupDb.Services;
using BackupDb.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BackupDb.Configuration
{
    internal static class ConfigureService
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IPhysicalFileService, PhysicalFileService>();
            services.AddTransient<IUploadService, UploadService>();
            services.AddTransient<IBucketService, BucketService>();
            services.AddTransient<IBackupService, BackupService>();
        }
    }
}
