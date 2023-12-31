using System;
using DevStore.Core.Messages;

namespace DevStore.Orders.API.Application.Events;

public class OrderDoneEvent : Event
{
    public OrderDoneEvent(Guid orderId, Guid customerId)
    {
        OrderId = orderId;
        CustomerId = customerId;
    }

    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
}