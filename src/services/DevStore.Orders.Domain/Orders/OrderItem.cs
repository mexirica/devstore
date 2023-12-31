using System;
using DevStore.Core.DomainObjects;

namespace DevStore.Orders.Domain.Orders;

public class OrderItem : Entity
{
    public OrderItem(Guid productId, string productName, int quantity,
        decimal price, string productImage = null)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        ProductImage = productImage;
    }

    // EF ctor
    protected OrderItem()
    {
    }

    public Guid OrderId { get; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; }
    public decimal Price { get; }
    public string ProductImage { get; set; }

    // EF Rel.
    public Order Order { get; set; }

    internal decimal CalculateAmount()
    {
        return Quantity * Price;
    }
}