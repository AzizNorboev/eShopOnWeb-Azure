namespace Microsoft.eShopWeb.Infrastructure.Messaging.Configuration
{
    public class ServiceBusConfiguration
    {
        public string? ConnectionStringToOrderQueue { get; set; }
        public string? OrderQueue { get; set; }
    }
}
