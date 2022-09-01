using Newtonsoft.Json;
using System.Collections.Generic;

namespace BackupDb.Models
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new List<string>();
        }

        public BaseResponse(object data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("errors")]
        public List<string> Errors { get; set; }

    }
}
