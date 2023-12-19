using Catalog.Domain.Products;
using Catalog.Domain.Sales;
using Catalog.Domain.Sales.Events;

namespace Catalog.Tests.UnitTests.Domain;

public sealed class SaleTests 
{
    [Fact]
    public void Generate_Should_RaiseSaleGeneratedDomainEvent()
    {
        Sale sale = Sale.Generate(
            ProductId.CreateUnique(),
            10,
            192.22m,
            Guid.NewGuid(),
            DateTime.UtcNow);

        bool raisedSaleGeneratedDomainEvent = sale
            .DomainEvents
            .Any(s => s.GetType() == typeof(SaleGeneratedDomainEvent));

        Assert.True(raisedSaleGeneratedDomainEvent);
    }
}
