using System;
namespace Microsoft.eShopWeb.Infrastructure.Messaging.Messages
{
    public interface IMessage
    {
        string Id { get; set; }
    }
}

