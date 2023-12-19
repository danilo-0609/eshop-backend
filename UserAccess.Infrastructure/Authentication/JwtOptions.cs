namespace UserAccess.Infrastructure.Authentication;

public sealed class JwtOptions
{
    public string Issuer { get; init; } = "EshopApp";

    public string Audience { get; init; } = "EshopService";

    public string SecretKey { get; init; } = "SuperSecretEshopKey";
}
