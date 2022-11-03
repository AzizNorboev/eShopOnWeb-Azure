using System.Threading.Tasks;
using Microsoft.eShopWeb.Infrastructure.Messaging.Messages;

namespace Microsoft.eShopWeb.Infrastructure.Messaging;

public interface IPublisher<T> where T : IMessage
{
    Task PublishAsync(T objectToPublish);
}
