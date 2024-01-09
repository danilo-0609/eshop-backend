namespace UserAccess.Infrastructure.Outbox;

public sealed class UserAccessOutboxMessage
{
    public Guid Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime OcurredOnUtc { get; set; }

    public DateTime? ProcessedOnUtc { get; set; }

    public string? Error { get; set; }

    public UserAccessOutboxMessage() { }
}
