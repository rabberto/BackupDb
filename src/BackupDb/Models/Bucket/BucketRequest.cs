using Newtonsoft.Json;

namespace BackupDb.Models.Bucket
{
    internal class BucketRequest : BaseRequest
    {
        public BucketRequest(string accessKey, string secretKey)
            : base(accessKey, secretKey)
        {
        }
    }
}
