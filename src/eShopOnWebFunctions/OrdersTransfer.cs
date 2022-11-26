using System;
using System.Threading.Tasks;
using Infrastructure.BlobStorage.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.eShopWeb.Infrastructure.Messaging.Messages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OrderItemsReserver
{
    public class OrdersTransfer
    {
        private readonly IFileStorageService _fileStorageService;
        public OrdersTransfer(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        [FunctionName("OrdersTransfer")]
        public async Task Run([ServiceBusTrigger("%ServiceBusConfiguration:OrderReportQueue%", Connection = "ServiceBusConfiguration:ConnectionStringToOrderReportQueue")]
        OrderReport message, ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
                if (message.Id != null)
                {

                    var orderDate = DateTime.UtcNow;
                    var orderDateString = orderDate.ToString("yyyyMMdd hh:mm:ss.fff tt");
                    var orderName = $"/Report_Order_{orderDateString}.json";
                    var orderJson = JsonConvert.SerializeObject(message);

                    await _fileStorageService.WriteFileAsync(orderName, orderJson);
                }
            }

            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
