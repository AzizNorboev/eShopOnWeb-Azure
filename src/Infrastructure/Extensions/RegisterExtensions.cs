using System.Text;
using Infrastructure.BlobStorage.Configuration;
using Infrastructure.BlobStorage.Options;
using Infrastructure.BlobStorage.Services;
using Infrastructure.BlobStorage.Services.Azure;
using Microsoft.eShopWeb.Infrastructure.CosmosDb;
using Microsoft.eShopWeb.Infrastructure.CosmosDb.Azure;
using Microsoft.eShopWeb.Infrastructure.CosmosDb.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class RegisterExtensions {

    public static IServiceCollection AddStorageServices(this IServiceCollection serviceCollection, IConfigurationSection configurationSection) {
        void BindEncoding(BlobContainerOptions opts) {
            var encodingString = configurationSection["Encoding"];
            if (!string.IsNullOrWhiteSpace(encodingString)) {
                opts.Encoding = Encoding.GetEncoding(encodingString);
            }
        }

        return serviceCollection.AddSingleton<IFileStorageService, FileStorageService>()
            .AddSingleton<IBlobContainerClient, AzureBlobContainerClient>()
            .Configure<BlobContainerOptions>(BindEncoding)
            .Configure<AzureBlobContainerOptions>(opts => {
                configurationSection.Bind(opts);
                BindEncoding(opts);
            });
    }

    public static IServiceCollection AddCosmosServices(this IServiceCollection serviceCollection, IConfigurationSection configurationSection)
    {
        return serviceCollection.AddSingleton<ICosmosService, CosmosService>()
            .AddSingleton<ICosmosClient, AzureCosmosClient>()
            .Configure<AzureCosmosClientOptions>(opts =>
            {
                configurationSection.Bind(opts);
            });
    }
}
