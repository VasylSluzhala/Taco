namespace Taco.Core.Configuration.Models
{
    public class AmazonS3Settings
    {
        public string BucketName { get; set; }

        public string AwsAccessKeyId { get; set; }

        public string AwsSecretAccessKey { get; set; }
    }
}
