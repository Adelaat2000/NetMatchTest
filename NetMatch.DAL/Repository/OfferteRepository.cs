using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using NetMatch.DAL.Interfaces;
using NetMatch.DAL.DTO;

namespace NetMatch.DAL.Repositories
{
    /// <summary>
    /// Repository for Offerte data access.
    /// Works with DTOs and raw database operations.
    /// </summary>
    public class OfferteRepository : IOfferteRepository
    {
        private readonly string _connectionString;

        public OfferteRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) 
                throw new ArgumentException("connectionString required", nameof(connectionString));
            _connectionString = connectionString;
        }

        public void Create(OfferteDTO offerte)
        {
            if (offerte == null) throw new ArgumentNullException(nameof(offerte));

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Offertes ([Name]) VALUES (@Name); SELECT CAST(SCOPE_IDENTITY() AS int);";
                cmd.Parameters.AddWithValue("@Name", (object)offerte.Name ?? DBNull.Value);

                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out var id))
                {
                    offerte.Id = id;
                }
            }
        }

        public IEnumerable<OfferteDTO> GetAll()
        {
            var list = new List<OfferteDTO>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, [Name] FROM Offertes";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new OfferteDTO
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1)
                        });
                    }
                }
            }

            return list;
        }

        public OfferteDTO GetById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Id, [Name] FROM Offertes WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new OfferteDTO
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1)
                        };
                    }
                }
            }

            return null;
        }

        public void Update(OfferteDTO offerte)
        {
            if (offerte == null) throw new ArgumentNullException(nameof(offerte));

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Offertes SET [Name] = @Name WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Name", (object)offerte.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", offerte.Id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM Offertes WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
