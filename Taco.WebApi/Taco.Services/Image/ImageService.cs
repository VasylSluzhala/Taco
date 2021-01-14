using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Threading.Tasks;
using Taco.Core;
using Taco.Core.DTO;

namespace Taco.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly RegionEndpoint bucketRegion = RegionEndpoint.EUWest2;
        private readonly IAmazonS3 _s3Client;
        private readonly ConfigurationFactory _configuration;

        public ImageService(ConfigurationFactory configuration)
        {
            _s3Client = new AmazonS3Client(configuration.AmazonS3.AwsAccessKeyId, configuration.AmazonS3.AwsSecretAccessKey, bucketRegion);
            _configuration = configuration;
        }

        public async Task<ImageDto> GetImage(string folder, string imageId)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = _configuration.AmazonS3.BucketName,
                Key = $"{folder}/{imageId}"
            };

            var result = new ImageDto();
            MemoryStream memoryStream = new MemoryStream();
            using (GetObjectResponse response = await _s3Client.GetObjectAsync(request))
            {
                using (Stream responseStream = response.ResponseStream)
                {

                    await responseStream.CopyToAsync(memoryStream);
                    result.ContentType = response.Headers["Content-Type"];
                }
            }

            result.File = memoryStream.ToArray();
            return result;
        }
    }
}
