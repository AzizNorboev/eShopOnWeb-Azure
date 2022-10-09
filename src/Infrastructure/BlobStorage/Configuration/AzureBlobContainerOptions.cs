using Infrastructure.BlobStorage.Options;

namespace Infrastructure.BlobStorage.Configuration;

public class AzureBlobContainerOptions : BlobContainerOptions
{
    public string? ConnectionString { get; set; }
    public string? ContainerName { get; set; }
}