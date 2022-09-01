using BackupDb.Configuration;
using BackupDb.Helpers;
using BackupDb.Models;
using BackupDb.Models.Bucket;
using BackupDb.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BackupDb.Services
{
    internal class BucketService : IBucketService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _accessKey = AppConfig.AccessKey;
        private readonly string _secretKy = AppConfig.SecretKey;
        private readonly string _address = AppConfig.Address;

        public async Task<BucketResponse> ListBucket()
        {
            var url = _address + "bucket/v1/list-bucket";
            var request = new BucketRequest(_accessKey, _secretKy);

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync(url, data);

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                return await GetContentObjectAsync(response);
            }
            catch (Exception ex)
            {
                LogService.Write($"ERROR {nameof(BucketService)}.{nameof(ListBucket)} - {ex.Message}");
                return null;
            }
        }

        public async Task<BucketResponse> GetContentObjectAsync(HttpResponseMessage response)
        {
            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var baseResponse = JsonConvert.DeserializeObject<BaseResponse>(content);

                if (baseResponse.Data == null)
                {
                    for (int i = 0; i < baseResponse.Errors.Count; i++)
                    {
                        throw new Exception(baseResponse.Errors[i]);
                    };
                }

                return JsonConvert.DeserializeObject<BucketResponse>(baseResponse.Data.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
