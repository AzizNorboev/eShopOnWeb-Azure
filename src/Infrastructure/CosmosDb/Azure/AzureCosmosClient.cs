using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Helpers;
using Microsoft.Azure.Cosmos;
using Microsoft.eShopWeb.Infrastructure.CosmosDb.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.eShopWeb.Infrastructure.CosmosDb.Azure;
public class AzureCosmosClient : ICosmosClient
{
    private readonly LazyAsync<CosmosClient> _blobContainerClient;
    private readonly IOptions<AzureCosmosClientOptions> _options;

    public AzureCosmosClient(IOptions<AzureCosmosClientOptions> options)
    {
        _blobContainerClient = new LazyAsync<CosmosClient>(async cancellationToken => {
            var containerClient = CreateClient();
            await Initialize(containerClient, cancellationToken);
            return containerClient;
        }, LazyThreadSafetyMode.None);
        _options = options;
    }
    protected CosmosClient CreateClient()
    {
        return new CosmosClient(_options.Value.Url, _options.Value.Key);
    }

    protected async Task Initialize(CosmosClient client, CancellationToken cancellationToken)
    {
        await client.CreateDatabaseIfNotExistsAsync(_options.Value.Db);
    }

    protected Task<CosmosClient> CreateClient(CancellationToken cancellationToken)
    {
        return Task.FromResult(new CosmosClient(_options.Value.Url, _options.Value.Key));
    }

    public async Task<Container> GetContainerAsync(CancellationToken cancellationToken)
    {
        var client = await _blobContainerClient.GetValue();
        var db = client.GetDatabase(_options.Value.Db);
        return await db.CreateContainerIfNotExistsAsync("Orders", "/OrderId");
    }
}
