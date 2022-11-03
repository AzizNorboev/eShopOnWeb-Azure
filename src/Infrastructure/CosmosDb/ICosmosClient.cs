using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Microsoft.eShopWeb.Infrastructure.CosmosDb;
internal interface ICosmosClient
{
    Task<Container> GetContainerAsync(CancellationToken cancellationToken);
}
