using BuildingBlocks.Application.Queries;
using ErrorOr;

namespace UserAccess.Application.Users.Login;
public sealed record LoginUserQuery(
    string Email, 
    string Password) : IQueryRequest<ErrorOr<string>>;