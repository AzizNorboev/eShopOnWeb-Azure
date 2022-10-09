using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.BlobStorage.Services;

public interface IBlobContainerClient {
    Task<IBlobClient> GetBlobClient(string fileName, CancellationToken cancellationToken);
}