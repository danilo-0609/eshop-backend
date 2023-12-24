using API.Controllers;
using Catalog.Application.Products.GetProductsByName;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

namespace API.Modules.Catalog.Endpoints;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ApiController
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.GetProducts)]
    [HttpGet("{name:string}")]
    public async Task<IActionResult> GetProductsByName(string name)
    {
        var query = new GetProductsByNameQuery(name);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }
}
