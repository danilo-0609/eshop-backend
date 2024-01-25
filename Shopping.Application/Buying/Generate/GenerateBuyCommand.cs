using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Items;

namespace Shopping.Application.Buying.Generate;

internal sealed record GenerateBuyCommand(
    Guid CustomerId,
    ItemId ItemId,
    int AmountOfItems,
    decimal Price) : ICommandRequest<ErrorOr<Unit>>;
