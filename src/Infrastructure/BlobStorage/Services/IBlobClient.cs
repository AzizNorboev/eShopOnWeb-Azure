using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.BlobStorage.Data;

namespace Infrastructure.BlobStorage.Services; 

public interface IBlobClient {
    Task UploadAsync(byte[] data, CancellationToken cancellationToken);
    Task UploadAsync(Stream data, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(CancellationToken cancellationToken);
    Task<byte[]?> DownloadAsync(CancellationToken cancellationToken);
    Task<StreamBlob?> DownloadStreamAsync(CancellationToken cancellationToken);
}