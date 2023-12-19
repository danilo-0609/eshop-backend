using API.Controllers;
using API.Modules.UserAccess.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using UserAccess.Application.Users.Login;

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
}
