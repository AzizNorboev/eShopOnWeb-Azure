using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.BlobStorage.Data;
using Infrastructure.BlobStorage.Errors;
using Infrastructure.BlobStorage.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.BlobStorage.Services;

public class FileStorageService : IFileStorageService {
    private readonly IBlobContainerClient _blobContainerClient;
    private readonly Encoding _encoding;
    public FileStorageService(IBlobContainerClient blobContainerClient, IOptions<BlobContainerOptions> options) {
        _blobContainerClient = blobContainerClient;
        _encoding = options.Value.Encoding ?? Encoding.UTF8;
    }

    public async Task WriteFileAsync(string fileName, string data, CancellationToken cancellationToken = default) {
        try {
            var blob = await _blobContainerClient.GetBlobClient(fileName, cancellationToken);
            await blob.UploadAsync(_encoding.GetBytes(data), cancellationToken).ConfigureAwait(false);
        } catch (Exception e) {
            //TODO: log and fail with app specific error
            throw;
        }
    }

    public async Task<string?> ReadFileAsync(string filename, CancellationToken cancellationToken = default) {
        var blob = await _blobContainerClient.GetBlobClient(filename, cancellationToken);

        if (!await blob.ExistsAsync(cancellationToken).ConfigureAwait(false))
            throw new BlobNotFoundException($"File: '{filename}' not found");

        try {
            var bytes = await blob.DownloadAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            if (bytes != null) {
                return _encoding.GetString(bytes);
            }
        } catch (Exception e) {
            //TODO: log and fail with app specific error
            throw;
        }
        return null;
    }

    public async Task UploadAsync(string filename, Stream data, bool closeStream = false, CancellationToken cancellationToken = default) {
        try {
            var blob = await _blobContainerClient.GetBlobClient(filename, cancellationToken);
            await blob.UploadAsync(data, cancellationToken).ConfigureAwait(false);
        } catch (Exception e) {
            //TODO: log and fail with app specific error
            throw;
        }
        finally {
            if (closeStream) {
                await data.DisposeAsync();
            }
        }
    }

    public async Task<StreamBlob?> DownloadAsync(string fileName, CancellationToken cancellationToken = default) {
        var blob = await _blobContainerClient.GetBlobClient(fileName, cancellationToken);
        try {
            return await blob.DownloadStreamAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        } catch (Exception e) {
            //TODO: log and fail with app specific error
            throw;
        }
        return null;
    }
}
