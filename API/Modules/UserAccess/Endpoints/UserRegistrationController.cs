using API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.UserRegistration.GetUserRegistrationByIda;
using UserAccess.Infrastructure.Authentication;

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

    [HasPermission(Permission.ReadMember)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserRegistrationById(Guid id)
    {
        var query = new GetUserRegistrationByIdQuery(id);

        var response = await _sender.Send(query);

        return response.Match(success => Ok(success),
                              error => Problem(error));
    }

}
