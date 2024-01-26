using API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Baskets.AddItem;
using Shopping.Application.Baskets.BuyBasket;
using Shopping.Application.Baskets.CreateBasket;
using Shopping.Application.Baskets.DeleteBasket;
using Shopping.Application.Baskets.DeleteItem;
using Shopping.Application.Baskets.GetBasketById;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

namespace API.Modules.Shopping.Endpoints;

[Route("api/[controller]")]
[ApiController]
public sealed class BasketsController : ApiController
{
    private readonly ISender _sender;

    public BasketsController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.AddItemInBasket)]
    [HttpPost("add-item/{id}")]
    public async Task<IActionResult> AddItemInBasket([FromHeader] Guid id, [FromBody] Guid itemId)
    {
        var command = new AddItemToBasketCommand(id, itemId);

        var response = await _sender.Send(command);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.BuyBasket)]
    [HttpPost("buy/{id}")]
    public async Task<IActionResult> BuyBasket([FromHeader] Guid id)
    {
        var command = new BuyBasketCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.AddBasket)]
    [HttpPost("create")]
    public async Task<IActionResult> CreateBasket([FromBody] Guid itemId)
    {
        var command = new CreateBasketCommand(itemId);

        var response = await _sender.Send(command);

        return response.Match(
            success => Created(success.ToString(), success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.DeleteBasket)]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBasket([FromHeader] Guid id)
    {
        var command = new DeleteBasketCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.DeleteBasketItem)]
    [HttpDelete("delete-item/{id}")]
    public async Task<IActionResult> DeleteBasketItem([FromHeader] Guid id, Guid itemId)
    {
        var command = new DeleteItemFromBasketCommand(id, itemId);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.GetBasket)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBasket([FromHeader] Guid id)
    {
        var query = new GetBasketByIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }
}
