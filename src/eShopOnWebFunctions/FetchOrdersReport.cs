using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Infrastructure.BlobStorage.Services;
using System.Threading;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.ApplicationInsights;
using Microsoft.eShopWeb.Infrastructure.CosmosDb;
using System.Collections.Generic;

namespace eShopOnWebFunctions
{
    public class FetchOrdersReport
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IOrderService _orderService;
        public FetchOrdersReport(IFileStorageService fileStorageService, IOrderService orderService)
        {
            _fileStorageService = fileStorageService;
            _orderService = orderService;
        }

        [FunctionName("FetchOrdersReport")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            CancellationToken cancellationToken)
        {
            try
            {
                //gets last created order
                var order = _orderService.GetCreatedOrder();
                if (order == null)
                {
                    throw new NullReferenceException();
                }
                var orderDate = DateTime.UtcNow;
                var orderDateString = orderDate.ToString("yyyyMMdd hh:mm:ss.fff tt");
                var orderName = $"/Report_Order_{orderDateString}.json";
                var orderJson = JsonConvert.SerializeObject(order);

                await _fileStorageService.WriteFileAsync(orderName, orderJson, cancellationToken);

                return await Task.FromResult(new OkObjectResult($"FetchOrdersReport HTTP triggered function executed successfully for order with ID = {order.Id}"));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new OkObjectResult($"{ex.Message}"));
                //return await Task.FromResult(new StatusCodeResult(403));
            }
        }
    }
}
