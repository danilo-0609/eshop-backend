using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using UserAccess.Application.Common;

namespace UserAccess.Application.Users.AddProfileImage;

public sealed record AddProfileImageCommand(Guid Id, IFormFile FormFile, string FilePath) : ICommandRequest<ErrorOr<Unit>>;

