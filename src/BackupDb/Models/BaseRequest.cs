using Newtonsoft.Json;

namespace BackupDb.Models
{
    public class BaseRequest
    {
        public BaseRequest(string accessKey, string secretKey)
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
        }

        [JsonProperty("accessKey")]
        public string AccessKey { get; set; }
        [JsonProperty("secretKey")]
        public string SecretKey { get; set; }
    }
}
