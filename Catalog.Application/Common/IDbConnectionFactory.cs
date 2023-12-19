using System.Data;

namespace Catalog.Application.Common;
public interface IDbConnectionFactory
{
    IDbConnection CreateOpenConnection();            
}
