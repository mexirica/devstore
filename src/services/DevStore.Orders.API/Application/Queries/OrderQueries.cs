using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DevStore.Orders.API.Application.DTO;
using DevStore.Orders.Domain.Orders;

namespace DevStore.Orders.API.Application.Queries;

public interface IOrderQueries
{
    Task<OrderDTO> GetLastOrder(Guid customerId);
    Task<IEnumerable<OrderDTO>> GetByCustomerId(Guid customerId);
    Task<OrderDTO> GetAuthorizedOrders();
}

public class OrderQueries : IOrderQueries
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueries(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDTO> GetLastOrder(Guid customerId)
    {
        const string sql = @"SELECT
                                P.ID AS 'PRODUCTID', P.CODE, P.HASVOUCHER, P.DISCOUNT, P.AMOUNT,P.ORDERSTATUS,
                                P.STREETADDRESS,P.BUILDINGNUMBER, P.NEIGHBORHOOD, P.ZIPCODE, P.SECONDARYADDRESS, P.CITY, P.STATE,
                                PIT.ID AS 'PRODUCTITEMID',PIT.PRODUCTNAME, PIT.QUANTITY, PIT.PRODUCTIMAGE, PIT.PRICE 
                                FROM Orders P 
                                INNER JOIN OrderITEMS PIT ON P.ID = PIT.OrderID 
                                WHERE P.CUSTOMERID = @customerId 
                                AND P.DateAdded between DATEADD(minute, -5,  @Now) and @Now
                                ORDER BY P.DateAdded DESC";

        var order = await _orderRepository.GetConnection()
            .QueryAsync<dynamic>(sql, new { customerId, DateTime.Now });

        if (!order.Any())
            return null;

        return MapOrder(order);
    }

    public async Task<IEnumerable<OrderDTO>> GetByCustomerId(Guid customerId)
    {
        var orders = await _orderRepository.GetCustomersById(customerId);

        return orders.Select(OrderDTO.ToOrderDTO);
    }

    public async Task<OrderDTO> GetAuthorizedOrders()
    {
        const string sql = @"SELECT 
                                P.ID as 'OrderId', P.ID, P.CUSTOMERID, 
                                PI.ID as 'OrderItemId', PI.ID, PI.PRODUCTID, PI.QUANTITY 
                                FROM Orders P 
                                INNER JOIN ORDERITEMS PI ON P.ID = PI.ORDERID 
                                WHERE P.ORDERSTATUS = 1                                
                                ORDER BY P.DATEADDED";

        // Lookup to keep state at every cycle
        var lookup = new Dictionary<Guid, OrderDTO>();

        await _orderRepository.GetConnection().QueryAsync<OrderDTO, OrderItemDTO, OrderDTO>(sql,
            (o, oi) =>
            {
                if (!lookup.TryGetValue(o.Id, out var orderDTO))
                    lookup.Add(o.Id, orderDTO = o);

                orderDTO.OrderItems ??= new List<OrderItemDTO>();
                orderDTO.OrderItems.Add(oi);

                return orderDTO;
            }, splitOn: "OrderId,OrderItemId");

        // get lookup data
        return lookup.Values.OrderBy(p => p.Date).FirstOrDefault();
    }

    private OrderDTO MapOrder(dynamic result)
    {
        var order = new OrderDTO
        {
            Code = result[0].CODE,
            Status = result[0].ORDERSTATUS,
            Amount = result[0].AMOUNT,
            Discount = result[0].DISCOUNT,
            HasVoucher = result[0].HASVOUCHER,

            OrderItems = new List<OrderItemDTO>(),
            Address = new AddressDto
            {
                StreetAddress = result[0].STREETADDRESS,
                Neighborhood = result[0].NEIGHBORHOOD,
                ZipCode = result[0].ZIPCODE,
                City = result[0].CITY,
                SecondaryAddress = result[0].SECONDARYADDRESS,
                State = result[0].STATE,
                BuildingNumber = result[0].BUILDINGNUMBER
            }
        };

        foreach (var item in result)
        {
            var orderItem = new OrderItemDTO
            {
                Name = item.PRODUCTNAME,
                Price = item.PRICE,
                Quantity = item.QUANTITY,
                Image = item.PRODUCTIMAGE
            };

            order.OrderItems.Add(orderItem);
        }

        return order;
    }
}