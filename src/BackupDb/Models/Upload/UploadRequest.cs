using Newtonsoft.Json;

namespace BackupDb.Models.Upload
{
    public sealed class UploadRequest
    {
        public UploadRequest(BaseRequest baseRequest, string bucketName, string keyAmazon, string fileName, string fileBase64)
        {
            BaseRequest = baseRequest;
            BucketName = bucketName;
            KeyAmazon = keyAmazon;
            FileName = fileName;
            FileBase64 = fileBase64;
        }

        [JsonProperty("authenticationViewModel")]
        public BaseRequest BaseRequest { get; private set; }

        [JsonProperty("bucketName")]
        public string BucketName { get; private set; }

        [JsonProperty("keyAmazon")]
        public string KeyAmazon { get; private set; }

        [JsonProperty("fileName")]
        public string FileName { get; private set; }

        [JsonProperty("fileBase64")]
        public string FileBase64 { get; private set; }
    }
}
