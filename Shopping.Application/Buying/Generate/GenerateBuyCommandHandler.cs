using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using Shopping.Domain.Buying;

namespace Shopping.Application.Buying.Generate;

internal sealed class GenerateBuyCommandHandler : ICommandRequestHandler<GenerateBuyCommand, ErrorOr<Unit>>
{
    private readonly IBuyRepository _buyRepository;

    public GenerateBuyCommandHandler(IBuyRepository buyRepository)
    {
        _buyRepository = buyRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(GenerateBuyCommand request, CancellationToken cancellationToken)
    {
        var buy = Buy.Generate(
            request.CustomerId,
            request.ItemId,
            request.AmountOfItems,
            request.Price,
            DateTime.UtcNow);

        await _buyRepository.AddAsync(buy);

        return Unit.Value;
    }
}
