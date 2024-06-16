using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using System.Drawing;

namespace AdoptPets.Application
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"];
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string animalType, string animalBreed)
        {
            string folderPath = $"{animalType.ToLower()}s/{animalBreed.ToLower()}/";
            string key = $"{folderPath}{fileName}";

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = key,
                BucketName = _bucketName,
                CannedACL = S3CannedACL.PublicRead
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            return $"https://{_bucketName}.s3.amazonaws.com/{key}";
        }

        public async Task<Bitmap> DownloadImageAsync(string key)
        {
            using (var response = await _s3Client.GetObjectAsync(_bucketName, key))
            using (var responseStream = response.ResponseStream)
            {
                return new Bitmap(responseStream);
            }
        }
    }

}
