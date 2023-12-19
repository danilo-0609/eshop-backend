using BuildingBlocks.Domain;

namespace Catalog.Domain.Ratings;
public sealed record RatingId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static RatingId Create(Guid value) => new RatingId(value);

    public static RatingId CreateUnique() => new RatingId(Guid.NewGuid());

    private RatingId(Guid value)
    {
        Value = value;
    }

    private RatingId()
    {
        
    }
}
