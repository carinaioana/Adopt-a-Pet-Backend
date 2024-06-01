using AdoptPets.Application.Persistence;
using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AdoptPets.Infrastructure.Repositories
{
    public class AnnouncementRepository : BaseRepository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(AdoptPetsContext context) : base(context)
        {

        }
        public Task<bool> IsAnnouncementTitleAndDateUnique(string announcementTitle, DateTime announcementDate)
        {
            var matches = context.Announcements.Any(a => a.AnnouncementTitle.Equals(announcementTitle)
            && a.AnnouncementDate.Date.Equals(announcementDate.Date));
            return Task.FromResult(matches);
        }

        public async Task<Result<Announcement>> FindByTitleAsync(string title)
        {
            try
            {
                var announcement = await context.Announcements
                    .FirstOrDefaultAsync(a => a.AnnouncementTitle.Equals(title, StringComparison.OrdinalIgnoreCase));

                if (announcement == null)
                {
                    return Result<Announcement>.Failure($"Announcement with title '{title}' not found");
                }

                return Result<Announcement>.Success(announcement);
            }
            catch (Exception ex)
            {
                return Result<Announcement>.Failure($"An error occurred while retrieving the announcement: {ex.Message}");
            }
        }

        public Task<List<Announcement>> GetAnnouncementsByUserAsync(string userId)
        {
            return context.Announcements
            .Where(a => a.CreatedBy != null && a.CreatedBy == userId)
            .ToListAsync();
        }
        /* public async Task<(bool Success, string Url)> UploadFileToS3(IFormFile file)
         {
             try
             {
                 var client = new AmazonS3Client(RegionEndpoint.USEast1);
                 var bucketName = "adoptpets";
                 var keyName = Guid.NewGuid().ToString();
                 var fileTransferUtility = new TransferUtility(client);
                 await using var fileToUpload = file.OpenReadStream();
                 await fileTransferUtility.UploadAsync(fileToUpload, bucketName, keyName);
                 return (true, $"https://{bucketName}.s3.amazonaws.com/{keyName}");
             }
             catch (Exception ex)
             {
                 return (false, ex.Message);
             }
         }*/
       /* public async Task<(bool Success, string Url)> UploadFileToS3(IFormFile file)
        {
            var s3Client = new AmazonS3Client();
            var bucketName = "my-images-bucket38";

            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);

                var uploadRequest = new PutObjectRequest
                {
                    InputStream = newMemoryStream,
                    BucketName = bucketName,
                    Key = file.FileName,
                    ContentType = file.ContentType
                };

                try
                {
                    var response = await s3Client.PutObjectAsync(uploadRequest);
                    var fileUrl = $"https://{bucketName}.s3.amazonaws.com/{file.FileName}";
                    return (true, fileUrl);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return (false, string.Empty);
                }
            }*/

        }
    }
}
