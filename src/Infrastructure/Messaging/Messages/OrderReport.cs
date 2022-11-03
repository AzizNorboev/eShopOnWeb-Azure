using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Infrastructure.Messaging.Messages;
public class OrderReport : IMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string BuyerId { get; set; }
    public string ReportName { get; set; }
    public Address ShippingAddress { get; set; }
    public List<BasketItem> Items { get; set; }

    public override string ToString()
    {
        return $"Name: {typeof(OrderReport)}, BuyerId: {BuyerId}, ShippingAddress: {ShippingAddress}, ItemName: {Items[0]}";
    }
}
