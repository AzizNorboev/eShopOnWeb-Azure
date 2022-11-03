
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Infrastructure.CosmosDb;
public interface ICosmosService
{
    public Task AddAsync(Order order, CancellationToken cancellationToken);
    public Task<IEnumerable<Order>> ReadAsync(CancellationToken cancellationToken);
}
