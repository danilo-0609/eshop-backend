using API.Controllers;
using API.Modules.Catalog.Requests;
using Catalog.Application.Ratings.AddRating;
using Catalog.Application.Ratings.Catalog.Application.Ratings.GetAllRatingsByProductId;
using Catalog.Application.Ratings.ChangeRating;
using Catalog.Application.Ratings.DeleteRating;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

namespace API.Modules.Catalog.Endpoints;

[Route("api/[controller]")]
[ApiController]
public sealed class RatingsController : ApiController
{
    private readonly ISender _sender;

    public RatingsController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.AddRating)]
    [HttpPost("publish/{id}")]
    public async Task<IActionResult> AddRating(Guid id, [FromBody] AddRatingRequest request)
    {
        var command = new AddRatingCommand(id, request.Rate, request.Feedback);

        var response = await _sender.Send(command);

        return response.Match(
            success => Created(success.ToString(), success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.UpdateRating)]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateRating(Guid id, [FromBody] AddRatingRequest request)
    {
        var command = new ChangeRatingCommand(id, request.Rate, request.Feedback);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.DeleteRating)]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRating(Guid id)
    {
        var command = new DeleteRatingCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.GetRatings)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllRatings(Guid id)
    {
        var query = new GetAllRatingsByProductIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }
}
