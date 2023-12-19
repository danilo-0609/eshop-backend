using System.Data;

namespace UserAccess.Application.Common;

public interface IDbConnectionFactory
{
    IDbConnection CreateOpenConnection();
}