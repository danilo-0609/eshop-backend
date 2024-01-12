using BuildingBlocks.Domain;

namespace Shopping.Domain.Payments;

public sealed record PaymentId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }
    
    public static PaymentId Create(Guid value) => new PaymentId(value);

    public static PaymentId CreateUnique() => new PaymentId(Guid.NewGuid());

    public PaymentId(Guid value)
    {
        Value = value;
    }

    private PaymentId()
    {
    }
}
