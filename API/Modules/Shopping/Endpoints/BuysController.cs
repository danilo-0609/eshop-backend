using API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using Shopping.Application.Buying.GetByCustomerId;
using System.Drawing.Text;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

namespace API.Modules.Shopping.Endpoints;

[Route("api/[controller]")]
[ApiController]
public sealed class BuysController : ApiController
{
    private readonly ISender _sender;

    public BuysController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.GetBuys)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBuys([FromHeader] Guid id)
    {
        var query = new GetBuysByCustomerIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }
}
