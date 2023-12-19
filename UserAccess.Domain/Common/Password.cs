using System.Security.Cryptography;
using System.Text;
using BuildingBlocks.Domain;

namespace UserAccess.Domain.Common;

public sealed record Password : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    public static Password Create(string value)
    {
        string hash = GenerateHash256(value);

        return new Password(hash);
    }

    private static string GenerateHash256(string value)
    {
        using SHA256 sha256 = SHA256.Create();

        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));

        StringBuilder password = new StringBuilder();

        foreach (byte d in bytes)
        {
            password.Append(d.ToString());
        }

        return password.ToString();
    }

    private Password(string value)
    {
        Value = value;
    }
}