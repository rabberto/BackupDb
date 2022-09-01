using BackupDb.Models.Bucket;
using System.Net.Http;
using System.Threading.Tasks;

namespace BackupDb.Services.Contracts
{
    internal interface IBucketService
    {
        Task<BucketResponse> ListBucket();
        Task<BucketResponse> GetContentObjectAsync(HttpResponseMessage response);
    }
}
