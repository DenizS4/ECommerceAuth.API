using System.Data;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ECommerce.Infrastructure.DbContext;

public class DapperDbContext  
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;
    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        string? connectionString = _configuration.GetConnectionString("PostgreSQL");
        
        // Npg Sql Connection

        _connection = new NpgsqlConnection(connectionString);
    }
    public IDbConnection DbConnection => _connection;
}