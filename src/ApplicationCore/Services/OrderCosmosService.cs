using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Services;
public class OrderCosmosService : IAzureCosmosService<Order>
{
    public Task<bool> CreateAsync(Order entity)
    {
        throw new NotImplementedException();
    }
}
