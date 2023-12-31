using System.Threading.Tasks;
using DevStore.Core.Mediator;
using DevStore.Orders.API.Application.Commands;
using DevStore.Orders.API.Application.Queries;
using DevStore.WebAPI.Core.Controllers;
using DevStore.WebAPI.Core.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.Orders.API.Controllers;

[Authorize]
[Route("orders")]
public class OrderController : MainController
{
    private readonly IMediatorHandler _mediator;
    private readonly IOrderQueries _orderQueries;
    private readonly IAspNetUser _user;

    public OrderController(IMediatorHandler mediator,
        IAspNetUser user,
        IOrderQueries orderQueries)
    {
        _mediator = mediator;
        _user = user;
        _orderQueries = orderQueries;
    }

    [HttpPost("")]
    public async Task<IActionResult> AddOrder(AddOrderCommand order)
    {
        order.CustomerId = _user.GetUserId();
        return CustomResponse(await _mediator.SendCommand(order));
    }

    [HttpGet("last")]
    public async Task<IActionResult> LastOrder()
    {
        var order = await _orderQueries.GetLastOrder(_user.GetUserId());

        return order == null ? NotFound() : CustomResponse(order);
    }

    [HttpGet("customers")]
    public async Task<IActionResult> Customers()
    {
        var orders = await _orderQueries.GetByCustomerId(_user.GetUserId());

        return orders == null ? NotFound() : CustomResponse(orders);
    }
}