using System.Data;
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
    
    public DataTableReader ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        DataTable table = new DataTable();
                        table.Load(reader);  // Laad de gegevens in een DataTable
                        return table.CreateDataReader();  // Retourneer de DataTableReader
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log de fout of geef een gedetailleerde foutmelding
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;  // Hergooi de exception om deze verder omhoog te laten gaan
            }
            catch (Exception ex)
            {
                // Algemene fout voor andere onverwachte problemen
                Console.WriteLine($"General Error: {ex.Message}");
                throw;  // Hergooi de exception
            }
        }

        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        connection.Open();
                        return command.ExecuteNonQuery();  // Aantal rijen dat beïnvloed is
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log de fout of geef een gedetailleerde foutmelding
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;  // Hergooi de exception om deze verder omhoog te laten gaan
            }
            catch (Exception ex)
            {
                // Algemene fout voor andere onverwachte problemen
                Console.WriteLine($"General Error: {ex.Message}");
                throw;  // Hergooi de exception
            }
        }
    }


