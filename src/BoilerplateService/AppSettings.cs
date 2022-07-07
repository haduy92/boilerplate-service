namespace BoilerplateService
{
    public class AppSettings
    {
        public AwsOptions AWS { get; set; }
        public DynamoDbOptions DynamoDb { get; set; }
    }

    public class AwsOptions
    {
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }

    public class DynamoDbOptions
    {
        public bool LocalMode { get; set; }
        public string LocalServiceUrl { get; set; }
    }
}