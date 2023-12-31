using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using DevStore.Core.Data;
using DevStore.Orders.Domain.Orders;
using DevStore.Orders.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace DevStore.Orders.Infra.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrdersContext _context;

    public OrderRepository(OrdersContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public DbConnection GetConnection()
    {
        return _context.Database.GetDbConnection();
    }

    public async Task<Order> GetById(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<IEnumerable<Order>> GetCustomersById(Guid customerId)
    {
        return await _context.Orders
            .Include(p => p.OrderItems)
            .AsNoTracking()
            .Where(p => p.CustomerId == customerId)
            .ToListAsync();
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }


    public async Task<OrderItem> GetItemById(Guid id)
    {
        return await _context.OrderItems.FindAsync(id);
    }

    public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
    {
        return await _context.OrderItems
            .FirstOrDefaultAsync(p => p.ProductId == productId && p.OrderId == orderId);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}