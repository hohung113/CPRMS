using Amazon.S3;
using Amazon.S3.Model;
using Core.Application.ExternalServices;
using Core.Application.ServiceModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.ExternalServices
{
    public class S3StorageService : IS3StorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly AwsS3Options _options;
        public S3StorageService(IAmazonS3 s3Client, IOptions<AwsS3Options> options)
        {
            _s3Client = s3Client;
            _options = options.Value;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folder = "uploads")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            var fileKey = $"{folder}/{Guid.NewGuid()}_{file.FileName}";

            var request = new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = fileKey,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
                CannedACL = S3CannedACL.PublicRead
            };

            await _s3Client.PutObjectAsync(request);

            return $"https://{_options.BucketName}.s3.{_options.Region}.amazonaws.com/{fileKey}";
        }

        public async Task<bool> DeleteFileAsync(string fileKey)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _options.BucketName,
                Key = fileKey
            };

            var response = await _s3Client.DeleteObjectAsync(deleteRequest);
            return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
        }
    }
}
