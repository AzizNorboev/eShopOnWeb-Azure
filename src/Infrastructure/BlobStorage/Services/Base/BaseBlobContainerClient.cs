using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Helpers;

namespace Infrastructure.BlobStorage.Services.Base; 

public abstract class BaseBlobContainerClient<T>: IBlobContainerClient {
    
    private readonly LazyAsync<T> _blobContainerClient;

    protected BaseBlobContainerClient() {
        _blobContainerClient = new LazyAsync<T>(async cancellationToken => {
            var containerClient = CreateContainer();
            await Initialize(containerClient, cancellationToken);
            return containerClient;
        }, LazyThreadSafetyMode.None);
    }
    protected abstract T CreateContainer();
    protected abstract Task Initialize(T container, CancellationToken cancellationToken);
    protected abstract Task<IBlobClient> CreateClient(T container, string fileName, CancellationToken cancellationToken);

    public async Task<IBlobClient> GetBlobClient(string fileName, CancellationToken cancellationToken) {
        try
        {
            var client = await _blobContainerClient.GetValue(cancellationToken);
            return await CreateClient(client, fileName, cancellationToken);
        }
        catch (Exception)
        {
            var client = await _blobContainerClient.GetValue(cancellationToken);
            return await CreateClient(client, fileName, cancellationToken);
        }
        
    }
}
