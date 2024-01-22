using API.Controllers;
using API.Modules.UserAccess.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.Users.ChangeAddress;
using UserAccess.Application.Users.ChangeLogin;
using UserAccess.Application.Users.ChangeName;
using UserAccess.Application.Users.ChangePassword;
using UserAccess.Application.Users.GetUserById;
using UserAccess.Application.Users.Login;
using UserAccess.Application.Users.RemoveUser;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

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
    public async Task<IActionResult> LoginUser(LoginUserRequest loginUserRequest)
    {
         var response = await _sender
            .Send(new LoginUserQuery(loginUserRequest.Email, loginUserRequest.Password));

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return Ok(response.Value);
    }

    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/address/{id}")]
    public async Task<IActionResult> ChangeUserAddress(ChangeAddressRequest request, Guid id)
    {
        var response = await _sender
            .Send(new ChangeEmailCommand(id, request.Address));

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [Authorize]
    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/email/{id}")]
    public async Task<IActionResult> ChangeUserEmail([FromBody] ChangeEmailRequest request, Guid id)
    {
        var response = await _sender
            .Send(new ChangeEmailCommand(id, request.Email));

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [Authorize]
    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/login/{id}")]
    public async Task<IActionResult> ChangeUserLogin([FromBody] ChangeLoginRequest request, Guid id)
    {
        var response = await _sender
            .Send(new ChangeLoginCommand(id, request.Login));

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/name/{id}")]
    public async Task<IActionResult> ChangeUserName([FromBody] ChangeNameRequest request, Guid id)
    {
        var response = await _sender
            .Send(new ChangeNameCommand(id, request.FirstName, request.LastName));

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [HasPermission(Permissions.ChangeUser)]
    [HttpPut("change/password/{id}")]
    public async Task<IActionResult> ChangeUserPassword(ChangePasswordRequest request, Guid id)
    {
        var response = await _sender
            .Send(new ChangePasswordCommand(id, request.ActualPassword, request.NewPassword));

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }

    [HasPermission(Permissions.ReadUser)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var response = await _sender.Send(new GetUserByIdQuery(id));

        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return Ok(response.Value);
    }

    [HasPermission(Permissions.RemoveUser)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveUser(Guid id)
    {
        var response = await _sender.Send(new RemoveUserCommand(id));
        
        if (response.IsError)
        {
            return Problem(response.Errors);
        }

        return NoContent();
    }
}
