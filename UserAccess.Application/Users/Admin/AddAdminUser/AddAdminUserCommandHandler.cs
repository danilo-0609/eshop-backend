using BuildingBlocks.Application.Commands;
using ErrorOr;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.Admin.AddAdminUser;
internal sealed class AddAdminUserCommandHandler
    : ICommandRequestHandler<AddAdminUserCommand, ErrorOr<Guid>>
{
    private readonly IUserRepository _userRepository;

    public AddAdminUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(AddAdminUserCommand request, CancellationToken cancellationToken)
    {
        User admin = User.CreateAdmin(
            request.Login,
            request.Password,
            request.Email,
            request.FirstName,
            request.LastName,
            $"{request.FirstName} {request.LastName}",
            request.Address,
            DateTime.UtcNow);

        await _userRepository.AddAsync(admin);

        return admin.Id.Value;
    }
}