using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using UserAccess.Application.Abstractions;

namespace UserAccess.Infrastructure.Configuration.DbConnectionConfiguration;

internal sealed class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateOpenConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("Database"));
    }
}
