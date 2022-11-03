using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.Infrastructure.Messaging.Messages;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Messaging;

public class ServiceBusPublisher<T> : IPublisher<T> where T : IMessage
{
    private readonly ServiceBusSender _serviceBusSender;

    public ServiceBusPublisher(ServiceBusSender serviceBusSender)
    {
        _serviceBusSender = serviceBusSender;
    }

    public async Task PublishAsync(T objectToPublish)
    {
        var jsonFormOfObjectToPublish = JsonSerializer.Serialize(objectToPublish);

        var message = new ServiceBusMessage(jsonFormOfObjectToPublish);

        await _serviceBusSender.SendMessageAsync(message);
    }
}
