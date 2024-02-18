using BuildingBlocks.Application.Blobs;
using ErrorOr;
using MassTransit.Initializers;
using UserAccess.Application.Abstractions;
using UserAccess.Application.Common;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.GetUserProfileImage;

internal sealed class GetUserProfileImageQueryHandler : IQueryRequestHandler<GetUserProfileImageQuery, ErrorOr<BlobObject>>
{
    private readonly IUserRepository _userRepository;
    private readonly IBlobService _blobService;
    private readonly IAuthorizationService _authorizationService;

    public GetUserProfileImageQueryHandler(IUserRepository userRepository, IBlobService blobService, IAuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _blobService = blobService;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<BlobObject>> Handle(GetUserProfileImageQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository
            .GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(user.Id.Value);

        if (authorizationService.IsError)
        {
            return authorizationService.FirstError;
        }

        BlobObject? blobObject = await _blobService.GetBlobAsync(user.ProfileImageName);
    
        if (blobObject is null)
        {
            return Error.NotFound("User.ProfileImageNotFound", "User profile image was not found");
        }

        return blobObject;
    }
}
