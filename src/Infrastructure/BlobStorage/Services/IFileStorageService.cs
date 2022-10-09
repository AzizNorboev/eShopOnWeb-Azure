using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.BlobStorage.Data;

namespace Infrastructure.BlobStorage.Services
{
    public interface IFileStorageService
    {
        Task WriteFileAsync(string fileName, string data, CancellationToken cancellationToken = default);
        Task<string?> ReadFileAsync(string filename, CancellationToken cancellationToken = default);
        Task UploadAsync(string filename, Stream data, bool closeStream = false, CancellationToken cancellationToken = default);
        Task<StreamBlob?> DownloadAsync(string fileName, CancellationToken cancellationToken = default);
    }
}
