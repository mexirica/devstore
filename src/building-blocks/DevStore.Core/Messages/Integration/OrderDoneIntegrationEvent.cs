using System;

namespace DevStore.Core.Messages.Integration;

public class OrderDoneIntegrationEvent : IntegrationEvent
{
    public OrderDoneIntegrationEvent(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; private set; }
}