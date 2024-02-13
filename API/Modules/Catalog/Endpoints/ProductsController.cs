using API.Controllers;
using API.Modules.Catalog.Requests;
using Catalog.Application.Products.GetAllProductsBySeller;
using Catalog.Application.Products.GetProductById;
using Catalog.Application.Products.GetProductsByName;
using Catalog.Application.Products.GetProductsByProductType;
using Catalog.Application.Products.GetProductsByTag;
using Catalog.Application.Products.ModifyProduct.ModifyColor;
using Catalog.Application.Products.ModifyProduct.ModifyDescription;
using Catalog.Application.Products.ModifyProduct.ModifyInStock;
using Catalog.Application.Products.ModifyProduct.ModifyName;
using Catalog.Application.Products.ModifyProduct.ModifyPrice;
using Catalog.Application.Products.ModifyProduct.ModifyProductType;
using Catalog.Application.Products.ModifyProduct.ModifySize;
using Catalog.Application.Products.ModifyProduct.ModifyTag;
using Catalog.Application.Products.PublishProducts;
using Catalog.Application.Products.RemoveProduct;
using MediatR;
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

    [HasPermission(Permissions.PublishProduct)]
    [HttpPost("publish")]
    public async Task<IActionResult> PublishProduct([FromBody] PublishProductRequest request)
    {
        var command = new PublishProductCommand(
            request.Name,
            request.Price,
            request.Description,
            request.Sizes,
            request.ProductType,
            request.Tags,
            request.InStock,
            request.Colors);

        var response = await _sender.Send(command);

        return response.Match(
            success => Created(success.ToString(), success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.GetProducts)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var query = new GetProductByIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.RemoveProduct)]
    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveProduct(Guid id)
    {
        var command = new RemoveProductCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }


    [HasPermission(Permissions.GetProducts)]
    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetProductsByName(string name)
    {
        var query = new GetProductsByNameQuery(name);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [HasPermission(Permissions.GetProducts)]
    [HttpGet("seller/{id}")]
    public async Task<IActionResult> GetProductsBySellerId(Guid id)
    {
        var query = new GetAllProductsBySellerQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [HasPermission(Permissions.GetProducts)]
    [HttpGet("productType/{productType}")]
    public async Task<IActionResult> GetProductsByProductType(string productType)
    {
        var query = new GetProductsByProductTypeQuery(productType);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [HasPermission(Permissions.GetProducts)]
    [HttpGet("tag/{tag}")]
    public async Task<IActionResult> GetProductsByTag(string tag)
    {
        var query = new GetProductsByTagQuery(tag);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-color/{id}")]
    public async Task<IActionResult> ModifyProductColor(Guid id, [FromBody] ChangeColorRequest request)
    {
        var command = new ModifyColorCommand(id, request.Colors);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-description/{id}")]
    public async Task<IActionResult> ModifyProductDescription(Guid id, [FromBody] ChangeDescriptionRequest request)
    {
        var command = new ModifyDescriptionCommand(id, request.Description);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-instock/{id}")]
    public async Task<IActionResult> ModifyProductsInStock(Guid id, [FromBody] ChangeInStockRequest request)
    {        
        var command = new ModifyInStockCommand(id, request.InStock);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-name/{id}")]
    public async Task<IActionResult> ModifyProductName(Guid id, [FromBody] ChangeNameRequest request)
    {
        var command = new ModifyNameCommand(id, request.Name);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-price/{id}")]
    public async Task<IActionResult> ModifyProductPrice(Guid id, [FromBody] ChangePriceRequest request)
    {   
        var command = new ModifyPriceCommand(id, request.Price);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-type/{id}")]
    public async Task<IActionResult> ModifyProductType(Guid id, [FromBody] ChangeProductTypeRequest request)
    {
        var command = new ModifyProductTypeCommand(id, request.ProductType);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-size/{id}")]
    public async Task<IActionResult> ModifyProductSize(Guid id, [FromBody] ChangeSizeRequest request)
    {
        var command = new ModifySizeCommand(id, request.Sizes);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-tag/{id}")]
    public async Task<IActionResult> ModifyProductTag(Guid id, [FromBody] ChangeTagsRequest request)
    {
        var command = new ModifyTagCommand(id, request.Tags);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }
}
