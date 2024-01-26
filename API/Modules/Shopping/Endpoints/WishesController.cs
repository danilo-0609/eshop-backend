using API.Controllers;
using API.Modules.Shopping.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Wishes.AddItem;
using Shopping.Application.Wishes.ChangeVisibility;
using Shopping.Application.Wishes.Create;
using Shopping.Application.Wishes.Delete;
using Shopping.Application.Wishes.GetById;
using Shopping.Application.Wishes.RemoveItem;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

namespace API.Modules.Shopping.Endpoints;

[Route("api/[controller]")]
[ApiController]
public sealed class WishesController : ApiController
{
    private readonly ISender _sender;

    public WishesController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.AddItemInWish)]
    [HttpPost("add-item/{id}")]
    public async Task<IActionResult> AddItemInBasket([FromHeader] Guid id, [FromBody] Guid itemId)
    {
        var command = new AddItemToWishListCommand(id, itemId);

        var response = await _sender.Send(command);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.ChangeWishVisibility)]
    [HttpPut("change-visibility/{id}")]
    public async Task<IActionResult> ChangeWishVisibility([FromHeader] Guid id, [FromBody] bool visibility)
    {
        var command = new ChangeWishVisibilityCommand(id, visibility);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.CreateWish)]
    [HttpPost]
    public async Task<IActionResult> CreateWish([FromBody] CreateWishRequest request)
    {
        var command = new CreateWishListCommand(request.ItemId, request.Name, request.IsPrivate);

        var response = await _sender.Send(command);

        return response.Match(
            success => Created(success.ToString(), success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.DeleteWish)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWish([FromHeader] Guid id)
    {
        var command = new DeleteWishListCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.GetWish)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWish([FromHeader] Guid id)
    {
        var query = new GetWishListByIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.RemoveItemInWish)]
    [HttpDelete("delete-item/{id}")]
    public async Task<IActionResult> RemoveItem([FromHeader] Guid id, [FromBody] Guid itemId)
    {
        var command = new RemoveItemFromWishListCommand(id, itemId);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }
}
