using BuildingBlocks.Domain;
using Newtonsoft.Json;

namespace UserAccess.Domain.UserRegistrations;

public sealed record UserRegistrationId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }
    
    public static UserRegistrationId Create(Guid value) => new UserRegistrationId(value); 

    public static UserRegistrationId CreateUnique() => new UserRegistrationId(Guid.NewGuid());

    [JsonConstructor]
    private UserRegistrationId(Guid value)
    {
        Value = value;
    }

    private UserRegistrationId() 
    {
    }
}
