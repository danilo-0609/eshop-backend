using UserAccess.Application.Common;
using ErrorOr;
using MediatR;
using UserAccess.Domain.UserRegistrations;
using UserAccess.Domain.UserRegistrations.Errors;

namespace UserAccess.Application.UserRegistration.ConfirmUserRegistration;

internal sealed class ConfirmUserRegistrationCommandHandler 
    : ICommandRequestHandler<ConfirmUserRegistrationCommand, ErrorOr<Unit>>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;

    public ConfirmUserRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository)
    {
        _userRegistrationRepository = userRegistrationRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var userRegistration = await _userRegistrationRepository.GetByIdAsync(UserRegistrationId.Create(request.UserRegistrationId));
    
        if (userRegistration is null)
        {
            return UserRegistrationErrors.NotFound;
        }

        var confirmation = userRegistration.Confirm();

        if (confirmation.IsError)
        {
            return confirmation.FirstError;
        }
        

        return Unit.Value;
    }
}