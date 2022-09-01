using BackupDb.Configuration;
using BackupDb.Flows;
using BackupDb.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BackupDb
{
    internal class Program
    {
        static void Main()
        {
            var services = new ServiceCollection();
            ConfigureService.Configure(services);

            var serviceProvider = services.BuildServiceProvider();

            var bucketFlow = new BucketFlow(serviceProvider.GetService<IBucketService>());

            var backupFlow = new BackupFlow(serviceProvider.GetService<IPhysicalFileService>(),
                                            serviceProvider.GetService<IUploadService>(),
                                            serviceProvider.GetService<IBackupService>());

            backupFlow.FileUploadFlow();
            //bucketFlow.List();

        }
    }
}
