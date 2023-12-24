using API.Controllers;
using API.Modules.UserAccess.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using UserAccess.Application.UserRegistration.ConfirmUserRegistration;
using UserAccess.Application.UserRegistration.GetUserRegistrationByIda;
using UserAccess.Application.UserRegistration.RegisterNewUser;
using UserAccess.Domain.Enums;
using UserAccess.Infrastructure.Authorization;

namespace API.Modules.UserAccess.Endpoints;

[Route("api/[controller]")]
[ApiController]
public class UserRegistrationController : ApiController
{
    private readonly ISender _sender;

    public UserRegistrationController(ISender sender)
    {
        _sender = sender;
    }

    [HasPermission(Permissions.ReadUser)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserRegistrationById(Guid id)
    {
        var query = new GetUserRegistrationByIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(success => Ok(success),
                              error => Problem(error));
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterNewUser(RegisterNewUserRequest request)
    {
        var command = new RegisterNewUserCommand(
            request.Login,
            request.Password,
            request.Email,
            request.FirstName,
            request.LastName,
            request.Address);

        var response = await _sender.Send(command);

        return response.Match(
            success => Ok(success),
            error => Problem(error));
    }

    [AllowAnonymous]
    [HttpPost("/confirm/{id:guid}")]
    public async Task<IActionResult> ConfirmUserRegistration(Guid id)
    {
        var command = new ConfirmUserRegistrationCommand(id);

        var response = await _sender.Send(command);

        return response.Match(
            success => Created(success.ToString(), id),
            error => Problem(error)); 
    }
}
