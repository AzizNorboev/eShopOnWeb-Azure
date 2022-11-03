using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.CosmosDb.Configuration;
public class AzureCosmosClientOptions
{
    public string? Url { get; set; }
    public string? Db { get; set; }
    public string? Key { get; set; }
    public string? ConnectionString { get; set; }
}
