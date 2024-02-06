using API.Controllers;
using API.Modules.Catalog.Requests;
using Catalog.Application.Comments.AddComment;
using Catalog.Application.Comments.DeleteComment;
using Catalog.Application.Comments.GetAllComments;
using Catalog.Application.Comments.UpdateComment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

namespace API.Modules.Catalog.Endpoints;

[Route("api/[controller]")]
[ApiController]
public sealed class CommentsController : ApiController
{
    private readonly ISender _sender;

    public CommentsController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.AddComment)]
    [HttpPost("publish/{id}")]
    public async Task<IActionResult> AddComment(Guid id, [FromBody] CommentRequest request)
    {
        var command = new AddCommentCommand(id, request.Comment);

        var response = await _sender.Send(command);

        return response.Match(
            success => Created(success.ToString(), success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.DeleteComment)]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        var command = new DeleteCommentCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.GetComments)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllComments(Guid id)
    {
        var query = new GetAllCommentsQuery(id);

        var response = await _sender.Send(query);

        return response.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HasPermission(Permissions.UpdateComment)]
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateComment(Guid id, [FromBody] CommentRequest request)
    {
        var command = new UpdateCommentCommand(id, request.Comment);

        var response = await _sender.Send(command);

        return response.Match(
            success => NoContent(),
            errors => Problem(errors));
    }
}
