using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Infrastructure.BlobStorage.Configuration;
using Infrastructure.BlobStorage.Services.Base;
using Microsoft.Extensions.Options;

namespace Infrastructure.BlobStorage.Services.Azure;

public class AzureBlobContainerClient: BaseBlobContainerClient<BlobContainerClient> {
    private readonly IOptions<AzureBlobContainerOptions> _options;
    public AzureBlobContainerClient(IOptions<AzureBlobContainerOptions> options) {
        _options = options;
    }

    protected override BlobContainerClient CreateContainer() {
        return new BlobContainerClient(_options.Value.ConnectionString, _options.Value.ContainerName);
    }
    
    protected override async Task Initialize(BlobContainerClient container, CancellationToken cancellationToken) {
        await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
    
    protected override Task<IBlobClient> CreateClient(BlobContainerClient container, string fileName, CancellationToken cancellationToken) {
        return Task.FromResult<IBlobClient>(new AzureBlobClient(container.GetBlobClient(fileName)));
    }
}