using System.Data;

namespace UserAccess.Application.Abstractions;

public interface IDbConnectionFactory
{
    IDbConnection CreateOpenConnection();
}