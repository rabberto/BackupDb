using Newtonsoft.Json;
using System.Collections.Generic;

namespace BackupDb.Models.Bucket
{
    internal class BucketResponse
    {
        [JsonProperty("buckets")]
        public List<BucketData> Buckets { get; set; }

        [JsonProperty("contentLength")]
        public int ContentLength { get; set; }

        [JsonProperty("httpStatusCode")]
        public int HttpStatusCode { get; set; }
    }
}
