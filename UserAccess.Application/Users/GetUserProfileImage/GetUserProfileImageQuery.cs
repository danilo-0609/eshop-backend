using BuildingBlocks.Application.Blobs;
using ErrorOr;
using UserAccess.Application.Common;

namespace UserAccess.Application.Users.GetUserProfileImage;

public sealed record GetUserProfileImageQuery(Guid Id) : IQueryRequest<ErrorOr<BlobObject>>;
