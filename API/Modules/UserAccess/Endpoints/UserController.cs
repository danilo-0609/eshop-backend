﻿using API.Controllers;
using API.Modules.UserAccess.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.Abstractions;
using UserAccess.Application.Users.AddProfileImage;
using UserAccess.Application.Users.ChangeAddress;
using UserAccess.Application.Users.ChangeLogin;
using UserAccess.Application.Users.ChangeName;
using UserAccess.Application.Users.ChangePassword;
using UserAccess.Application.Users.GetUserById;
using UserAccess.Application.Users.GetUserProfileImage;
using UserAccess.Application.Users.Login;
using UserAccess.Application.Users.RemoveUser;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;
using UserAccess.Infrastructure.Migrations;

namespace API.Modules.UserAccess.Endpoints;

[Route("api/[controller]")]
[ApiController]
public class UserController : ApiController
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("login")]
    public async Task<IActionResult> LoginUser(LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
    {
         var response = await _sender
            .Send(new LoginUserQuery(loginUserRequest.Email, loginUserRequest.Password), cancellationToken);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return Ok(response.Value);
    }

    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/address/{id}")]
    public async Task<IActionResult> ChangeUserAddress(ChangeAddressRequest request, Guid id, CancellationToken cancellationToken)
    {
        var response = await _sender
            .Send(new ChangeEmailCommand(id, request.Address), cancellationToken);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/email/{id}")]
    public async Task<IActionResult> ChangeUserEmail([FromBody] ChangeEmailRequest request, Guid id, CancellationToken cancellationToken)
    {
        var response = await _sender
            .Send(new ChangeEmailCommand(id, request.Email), cancellationToken);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/login/{id}")]
    public async Task<IActionResult> ChangeUserLogin([FromBody] ChangeLoginRequest request, Guid id, CancellationToken cancellationToken)
    {
        var response = await _sender
            .Send(new ChangeLoginCommand(id, request.Login), cancellationToken);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/name/{id}")]
    public async Task<IActionResult> ChangeUserName([FromBody] ChangeUserNameRequest request, Guid id, CancellationToken cancellationToken)
    {
        var response = await _sender
            .Send(new ChangeNameCommand(id, request.FirstName, request.LastName), cancellationToken);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/password/{id}")]
    public async Task<IActionResult> ChangeUserPassword(ChangePasswordRequest request, Guid id, CancellationToken cancellationToken)
    {
        var response = await _sender
            .Send(new ChangePasswordCommand(id, request.ActualPassword, request.NewPassword), cancellationToken);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("upload/profileimg/{id}")]
    public async Task<IActionResult> UploadProfileImage([FromForm] IFormFile file, Guid id)
    {
        var filePath = await GetFilePath(file);

        var response = await _sender.Send(new AddProfileImageCommand(id, file, filePath));

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    private static async Task<string> GetFilePath(IFormFile file)
    {
        string filePath = Path.GetTempFileName();

        using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    [HasPermission(Permissions.ReadUser)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(new GetUserByIdQuery(id), cancellationToken);

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return Ok(response.Value);
    }

    [HasPermission(Permissions.ReadUser)]
    [HttpGet("profileImage/{id:guid}")]
    public async Task<IActionResult> GetUserProfileImageById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(new GetUserProfileImageQuery(id), cancellationToken);
        
        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return File(response.Value.Content!, response.Value.ContentType!);
    }

    [HasPermission(Permissions.RemoveUser)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveUser(Guid id, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(new RemoveUserCommand(id), cancellationToken);
        
        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }
}
