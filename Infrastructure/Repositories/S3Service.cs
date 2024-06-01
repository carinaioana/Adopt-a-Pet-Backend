using AdoptPets.Application.Persistence;
using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Amazon.Runtime;
using Amazon;

namespace AdoptPets.Infrastructure.Repositories
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly ILogger<S3Service> _logger;
        private readonly string _bucketName;

        public S3Service(IAmazonS3 s3Client, IConfiguration configuration, ILogger<S3Service> logger)
        {
            _s3Client = _s3Client = new AmazonS3Client(
                new BasicAWSCredentials(
                    configuration["AWS:AccessKey"], 
                    configuration["AWS:SecretKey"]),
                RegionEndpoint.GetBySystemName(configuration["AWS:Region"]));
            _logger = logger;
            _bucketName = configuration["AWS:BucketName"];
        }

        public async Task<(bool Success, string Url)> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogError("Invalid file input");
                return (false, null);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            try
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new PutObjectRequest
                    {
                        InputStream = newMemoryStream,
                        BucketName = _bucketName,
                        Key = fileName,
                        ContentType = file.ContentType,
                        //CannedACL = S3CannedACL.PublicRead
                    };

                    var response = await _s3Client.PutObjectAsync(uploadRequest);

                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var fileUrl = $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
                        return (true, fileUrl);
                    }
                    else
                    {
                        _logger.LogError($"S3 upload failed with status code {response.HttpStatusCode}");
                        return (false, null);
                    }
                }
            }
            catch (AmazonS3Exception e)
            {
                _logger.LogError($"Error encountered on server. Message:'{e.Message}' when writing an object");
                return (false, null);
            }
            catch (Exception e)
            {
                _logger.LogError($"Unknown error encountered on server. Message:'{e.Message}'");
                return (false, null);
            }
        }
    }
}
