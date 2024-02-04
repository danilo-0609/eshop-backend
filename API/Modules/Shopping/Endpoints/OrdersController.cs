using API.Controllers;
using API.Modules.Shopping.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Orders.Confirm;
using Shopping.Application.Orders.Expire;
using Shopping.Application.Orders.GetById;
using Shopping.Application.Orders.Pay;
using Shopping.Application.Orders.Place;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

namespace API.Modules.Shopping.Endpoints;

[Route("api/[controller]")]
[ApiController]
public sealed class OrdersController : ApiController
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.PlaceOrder)]
    [HttpPost("place/{id}")]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request, Guid id)
    {
        var command = new PlaceOrderCommand(id, request.AmountRequested);

        var response = await _sender.Send(command);

        return response.Match(
            success => Created(success.ToString(), success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.PayOrder)]
    [HttpPost("pay/{id}")]
    public async Task<IActionResult> PayOrder(Guid id)
    {
        var command = new PayOrderCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.ExpireOrder)]
    [HttpPut("expire/{id}")]
    public async Task<IActionResult> ExpireOrder(Guid id)
    {
        var command = new ExpireOrderCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors =>  Problem(errors));
    }

    [HasPermission(Permissions.ConfirmOrder)]
    [HttpPut("confirm/{id}")]
    public async Task<IActionResult> ConfirmOrder(Guid id)
    {
        var command = new ConfirmOrderCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.GetOrders)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrders(Guid id)
    {
        var query = new GetOrderByIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }
}
