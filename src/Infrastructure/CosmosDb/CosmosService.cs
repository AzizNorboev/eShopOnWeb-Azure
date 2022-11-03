using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Infrastructure.CosmosDb.Azure;

namespace Microsoft.eShopWeb.Infrastructure.CosmosDb;
internal class CosmosService : ICosmosService
{
    private readonly ICosmosClient _azureCosmosClient;

    public CosmosService(ICosmosClient azureCosmosClient)
    {
        _azureCosmosClient = azureCosmosClient;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        var container = await _azureCosmosClient.GetContainerAsync(cancellationToken);
        await container.CreateItemAsync(order, new PartitionKey("/OrderId"));

    }
    public async Task<IEnumerable<Order>> ReadAsync(CancellationToken cancellationToken)
    {
        //query = "SELECT * FROM Orders";
        //var container = await _azureCosmosClient.GetContainerAsync(cancellationToken);
        //QueryDefinition queryDefinition = new(query);
        //FeedIterator<Order> queryResultIterator = container.GetItemQueryIterator<Order>(queryDefinition);

        //List<Order> orders = new();
        //while (queryResultIterator.HasMoreResults)
        //{
        //    FeedResponse<Order> currentResultSet = await queryResultIterator.ReadNextAsync();
        //    foreach(var item in currentResultSet)
        //    {
        //        orders.Add(item);
        //    }
        //}
        //return orders;
        return null;
    }
}
