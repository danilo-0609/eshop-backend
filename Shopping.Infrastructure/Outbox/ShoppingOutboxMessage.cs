namespace Shopping.Infrastructure.Outbox;

internal sealed class ShoppingOutboxMessage
{
    internal Guid Id { get; set; }

    internal string Type { get; set; } = string.Empty;

    internal string Content { get; set; } = string.Empty;

    internal DateTime OcurredOnUtc { get; set; }

    internal DateTime? ProcessedOnUtc { get; set; }

    internal string? Error { get; set; }
}