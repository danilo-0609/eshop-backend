using BuildingBlocks.Application.Commands;
using Catalog.Domain.Products;
using Catalog.Domain.Sales;
using ErrorOr;

namespace Catalog.Application.Sales;

internal sealed class GenerateSaleCommandHandler : ICommandRequestHandler<GenerateSaleCommand, ErrorOr<Guid>>
{
    private readonly ISaleRepository _saleRepository;

    public GenerateSaleCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(GenerateSaleCommand command, CancellationToken cancellationToken)
    {
        Sale sale = Sale.Generate(
            ProductId.Create(command.ProductId),
            command.AmountOfProducts,
            command.UnitPrice,
            command.UserId,
            DateTime.UtcNow);

        await _saleRepository.AddAsync(sale);

        return sale.Id.Value;
    }

}