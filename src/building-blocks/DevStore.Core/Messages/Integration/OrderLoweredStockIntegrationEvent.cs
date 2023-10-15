using System;

namespace DevStore.Core.Messages.Integration;

public class OrderLoweredStockIntegrationEvent : IntegrationEvent
{
    public OrderLoweredStockIntegrationEvent(Guid customerId, Guid orderId)
    {
        CustomerId = customerId;
        OrderId = orderId;
    }

    public Guid CustomerId { get; private set; }
    public Guid OrderId { get; private set; }
}