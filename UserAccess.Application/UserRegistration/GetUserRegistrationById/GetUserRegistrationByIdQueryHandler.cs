﻿using BuildingBlocks.Application.Queries;
using ErrorOr;
using UserAccess.Application.UserRegistration.GetUserRegistrationByIda;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Application.UserRegistration.GetUserRegistrationById;

internal sealed class GetUserRegistrationByIdQueryHandler : IQueryRequestHandler<GetUserRegistrationByIdQuery, ErrorOr<UserRegistrationResponse>>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;

    public GetUserRegistrationByIdQueryHandler(IUserRegistrationRepository userRegistrationRepository)
    {
        _userRegistrationRepository = userRegistrationRepository;
    }

    public async Task<ErrorOr<UserRegistrationResponse>> Handle(GetUserRegistrationByIdQuery request, CancellationToken cancellationToken)
    {
        var userRegistration = await _userRegistrationRepository
                                     .GetByIdAsync(UserRegistrationId.Create(request.UserRegistrationId));
        
        if (userRegistration is null)
        {
            return Error.NotFound("UserRegistration.NotFound", "User registration was not found");
        }


        return new UserRegistrationResponse(
            userRegistration.Id.Value,
            userRegistration.Login,
            userRegistration.Email,
            userRegistration.Name,
            userRegistration.RegisteredDate,
            userRegistration.Status.Value,
            userRegistration.ConfirmedDate);
    }
}
