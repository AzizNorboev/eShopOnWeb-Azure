using Azure.Storage.Blobs;
using Infrastructure.BlobStorage.Services;
using Infrastructure.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PearsonAdoIntegrationFunctions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace PearsonAdoIntegrationFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services
                .AddLogging()
                .AddStorageServices(configuration.GetSection("StorageAccount"))
                .AddCosmosServices(configuration.GetSection("CosmosDb"))
                .AddSingleton<IOrderService, OrderService>()
                .AddSingleton(x => new BlobServiceClient(configuration.GetValue<string>("ConnectionString")))
                .AddSingleton<IFileStorageService, FileStorageService>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                .AddUserSecrets<Startup>();
        }
    }
}
