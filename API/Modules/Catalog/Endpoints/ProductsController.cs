﻿using API.Controllers;
using API.Modules.Catalog.Requests;
using Catalog.Application.Products.GetAllProductsBySeller;
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
            request.Size,
            request.ProductType,
            request.Tags,
            request.InStock,
            request.Color);

        var response = await _sender.Send(command);

        return response.Match(
            success => Created(success.ToString(), success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.RemoveProduct)]
    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveProduct([FromHeader] Guid id)
    {
        var command = new RemoveProductCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }


    [HasPermission(Permissions.GetProducts)]
    [HttpGet("{name}")]
    public async Task<IActionResult> GetProductsByName([FromHeader] string name)
    {
        var query = new GetProductsByNameQuery(name);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [HasPermission(Permissions.GetProducts)]
    [HttpGet("{sellerId}")]
    public async Task<IActionResult> GetProductsBySellerId([FromHeader] Guid sellerId)
    {
        var query = new GetAllProductsBySellerQuery(sellerId);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [HasPermission(Permissions.GetProducts)]
    [HttpGet("productType")]
    public async Task<IActionResult> GetProductsByProductType([FromBody] string productType)
    {
        var query = new GetProductsByProductTypeQuery(productType);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [HasPermission(Permissions.GetProducts)]
    [HttpGet("tag")]
    public async Task<IActionResult> GetProductsByTag([FromBody] string tag)
    {
        var query = new GetProductsByTagQuery(tag);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-color/{id}")]
    public async Task<IActionResult> ModifyProductColor([FromHeader] Guid id, [FromBody] string color)
    {
        var command = new ModifyColorCommand(id, color);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-description/{id}")]
    public async Task<IActionResult> ModifyProductDescription([FromHeader] Guid id, [FromBody] string description)
    {
        var command = new ModifyDescriptionCommand(id, description);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-instock/{id}")]
    public async Task<IActionResult> ModifyProductsInStock([FromHeader] Guid id, [FromBody] int inStock)
    {
        var command = new ModifyInStockCommand(id, inStock);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-name/{id}")]
    public async Task<IActionResult> ModifyProductName([FromHeader] Guid id, [FromBody] string name)
    {
        var command = new ModifyNameCommand(id, name);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-price/{id}")]
    public async Task<IActionResult> ModifyProductPrice([FromHeader] Guid id, [FromBody] decimal price)
    {
        var command = new ModifyPriceCommand(id, price);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-type/{id}")]
    public async Task<IActionResult> ModifyProductType([FromHeader] Guid id, [FromBody] string productType)
    {
        var command = new ModifyProductTypeCommand(id, productType);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-size/{id}")]
    public async Task<IActionResult> ModifyProductSize([FromHeader] Guid id, [FromBody] string size)
    {
        var command = new ModifySizeCommand(id, size);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }

    [HasPermission(Permissions.ModifyProduct)]
    [HttpPut("modify-tag/{id}")]
    public async Task<IActionResult> ModifyProductTag([FromHeader] Guid id, [FromBody] List<string> tags)
    {
        var command = new ModifyTagCommand(id, tags);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            error => Problem(error));
    }
}
