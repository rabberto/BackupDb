using BackupDb.Configuration;
using BackupDb.Helpers;
using BackupDb.Models;
using BackupDb.Models.File;
using BackupDb.Models.Upload;
using BackupDb.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BackupDb.Services
{
    public class UploadService : IUploadService
    {
        private readonly string _env = AppConfig.Env;
        private readonly string _accessKey = AppConfig.AccessKey;
        private readonly string _secretKy = AppConfig.SecretKey;
        private readonly string _address = AppConfig.Address;
        private readonly string _bucketName = AppConfig.BucketName;

        public async Task UploadFile(List<PhysicalFile> files)
        {
            foreach (var file in files)
            {
                var url = _address + "file/v1/upload";
                var data = BuilderContent(file);

                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsync(url, data);

                        if (!response.IsSuccessStatusCode)
                            LogService.Write($"ERROR {nameof(UploadService)}.{nameof(UploadFile)} - {response.RequestMessage}");

                        LogService.Write($"SUCESSO {nameof(UploadService)}.{nameof(UploadFile)} - {file.Name} file successfully uploaded");
                    };                        
                }
                catch (Exception ex)
                {
                    LogService.Write($"ERROR {nameof(UploadService)}.{nameof(UploadFile)} - {ex.Message}");
                }
            }
        }

        private StringContent BuilderContent(PhysicalFile file)
        {
            var keyAmazon = $"{_env}/{file.Name}";
            var request = new UploadRequest(new BaseRequest(_accessKey, _secretKy), _bucketName, keyAmazon, file.Name, file.Base64);
            var json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
