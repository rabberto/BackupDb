using Newtonsoft.Json;

namespace BackupDb.Models.Bucket
{
    internal class BucketData
    {
        public BucketData(string creationDate, string bucketName)
        {
            CreationDate = creationDate;
            BucketName = bucketName;
        }

        [JsonProperty("creationDate")]
        public string CreationDate { get; private set; }

        [JsonProperty("bucketName")]
        public string BucketName { get; private set; }
    }
}
