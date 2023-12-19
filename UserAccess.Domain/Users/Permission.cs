namespace UserAccess.Domain.Users;

public sealed class Permission
{
    public int Id { get; set; }

    public string Name { get; init; } = string.Empty;

    public Permission() { }
}
