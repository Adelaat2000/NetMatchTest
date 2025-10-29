using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;

namespace NetMatch.DAL.DAL;

public class DbConnection
{
    private readonly string? _connectionString;

    //fetch connection string from appsettings.json
    public DbConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("NetmatchDB");
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new InvalidOperationException("Connection string 'NetmatchDB' not found.");
        }
    }

    //open and return a new SqlConnection
    public SqlConnection GetConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}