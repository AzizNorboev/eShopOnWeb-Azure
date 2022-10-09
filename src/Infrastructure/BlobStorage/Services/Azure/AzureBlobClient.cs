using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Infrastructure.BlobStorage.Data;

namespace Infrastructure.BlobStorage.Services.Azure;

public class AzureBlobClient : IBlobClient {
    private readonly BlobClient _blobClient;
    public AzureBlobClient(BlobClient blobClient) {
        _blobClient = blobClient;
    }
    public async Task<bool> ExistsAsync(CancellationToken cancellationToken) {
        return await _blobClient.ExistsAsync(cancellationToken).ConfigureAwait(false);
    }
    public async Task UploadAsync(byte[] data, CancellationToken cancellationToken) {
        await _blobClient.UploadAsync(new BinaryData(data), cancellationToken).ConfigureAwait(false);
    }
    public async Task UploadAsync(Stream data, CancellationToken cancellationToken) {
        await _blobClient.UploadAsync(data, cancellationToken).ConfigureAwait(false);
    }
    public async Task<byte[]?> DownloadAsync(CancellationToken cancellationToken) {
        var content = await _blobClient.DownloadContentAsync(cancellationToken).ConfigureAwait(false);
        return content?.Value?.Content?.ToArray();
    }
    public async Task<StreamBlob?> DownloadStreamAsync(CancellationToken cancellationToken) {
        var response = await _blobClient.DownloadStreamingAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return new StreamBlob(response.Value.Content, response.Value.Details.ContentType);
    } 
}
