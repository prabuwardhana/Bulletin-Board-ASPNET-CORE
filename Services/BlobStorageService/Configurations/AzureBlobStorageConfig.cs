namespace Services.BlobStorageService.Configurations
{
    public class AzureBlobStorageConfig
    {
        public const string Name = "AzureBlobOption";

        public string ConnectionString { get; set; }
        public string Container { get; set; }
        public long FileSizeLimit { get; set; }
    }
}