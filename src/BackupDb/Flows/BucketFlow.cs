using BackupDb.Helpers;
using BackupDb.Services.Contracts;
using System;

namespace BackupDb.Flows
{
    internal class BucketFlow
    {
        private readonly IBucketService _bucketService;
        public BucketFlow(IBucketService bucketService)
        {
            _bucketService = bucketService; 
        }

        public void List()
        {
            var result = _bucketService.ListBucket();

            try
            {
                foreach (var bucket in result.Result.Buckets)
                    Console.WriteLine(bucket.BucketName);
            }
            catch (AggregateException ArEx)
            {
                LogService.Write($"ERROR {nameof(BucketFlow)}.{nameof(List)} - {ArEx.Message}");
            }
        }
    }
}
