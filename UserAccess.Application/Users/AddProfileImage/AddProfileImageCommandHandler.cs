using ErrorOr;
using MediatR;
using UserAccess.Application.Abstractions;
using UserAccess.Application.Common;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.AddProfileImage;

internal sealed class AddProfileImageCommandHandler : ICommandRequestHandler<AddProfileImageCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IBlobService _blobService;
    private readonly IAuthorizationService _authorizationService;

    public AddProfileImageCommandHandler(IUserRepository userRepository, IBlobService blobService, IAuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _blobService = blobService;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(AddProfileImageCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is  null)
        {
            return UserErrorsCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(user.Id.Value);

        if (authorizationService.IsError)
        {
            return UserErrorsCodes.CannotUploadImage;
        }

        string fileName = await _blobService.UploadFileBlobAsync(request.FilePath, request.FormFile.FileName);

        var update = User.Update(
            user.Id,
            user.Login,
            user.Password.Value,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Address,
            user.Roles,
            user.CreatedDateTime,
            DateTime.UtcNow,
            $"{user.Id.Value}" + fileName);

        await _userRepository.UpdateAsync(update);

        return Unit.Value;
    }
}
