using API.Controllers;
using Catalog.Application.Sales.GetAllSalesByProductId;
using Catalog.Application.Sales.GetAllSalesByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

namespace API.Modules.Catalog.Endpoints;

[Route("api/[controller]")]
[ApiController]
public sealed class SalesController : ApiController
{
    private readonly ISender _sender;

    public SalesController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.GetSales)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllSalesByProduct([FromHeader] Guid id)
    {
        var query = new GetAllSalesByProductIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.GetSales)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSalesByUser([FromHeader] Guid id)
    {
        var query = new GetSalesByUserIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }
}
