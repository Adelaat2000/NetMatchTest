using System.Data;
using Microsoft.Data.SqlClient;
using NetMatch.DAL.DTO;
using NetMatch.DAL.Interfaces;

public class ReisOverzichtRepository : IReisOverzichtRepository
{
    private readonly string _connectionString;

    public ReisOverzichtRepository(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("connectionString required", nameof(connectionString));

        _connectionString = connectionString;
    }

    public ReisOverzichtDTO.AccommodationDto GetAccommodationByTripId(int tripId)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = connection.CreateCommand())
        {
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = @"SELECT t.Id AS TripId,
                                           t.AccommodationId,
                                           t.Nights,
                                           t.Guests,
                                           a.Name,
                                           a.ImageUrl,
                                           ISNULL(a.FromPrice, 0) AS FromPrice
                                    FROM Trip t
                                    INNER JOIN Accommodations a ON a.Id = t.AccommodationId
                                    WHERE t.Id = @TripId";
            command.Parameters.AddWithValue("@TripId", tripId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    decimal fromPrice = reader.IsDBNull(reader.GetOrdinal("FromPrice"))
                        ? 0
                        : reader.GetDecimal(reader.GetOrdinal("FromPrice"));
                    int nights = reader.IsDBNull(reader.GetOrdinal("Nights"))
                        ? 0
                        : reader.GetInt32(reader.GetOrdinal("Nights"));
                    int guests = reader.IsDBNull(reader.GetOrdinal("Guests"))
                        ? 0
                        : reader.GetInt32(reader.GetOrdinal("Guests"));

                    return new ReisOverzichtDTO.AccommodationDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("AccommodationId")),
                        Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                        ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? string.Empty : reader.GetString(reader.GetOrdinal("ImageUrl")),
                        Nights = nights,
                        Guests = guests,
                        Price = nights * fromPrice
                    };
                }
            }
        }

        return null;
    }

    public List<ReisOverzichtDTO.TransportDto> GetTransportsByTripId(int tripId)
    {
        List<ReisOverzichtDTO.TransportDto> transports = new List<ReisOverzichtDTO.TransportDto>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = connection.CreateCommand())
        {
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = @"SELECT Id, Route, Date, Time, Price
                                    FROM Transport
                                    WHERE TripId = @TripId";
            command.Parameters.AddWithValue("@TripId", tripId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    transports.Add(new ReisOverzichtDTO.TransportDto
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Route = reader.IsDBNull(reader.GetOrdinal("Route")) ? string.Empty : reader.GetString(reader.GetOrdinal("Route")),
                        Date = reader.IsDBNull(reader.GetOrdinal("Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Date")),
                        Time = reader.IsDBNull(reader.GetOrdinal("Time")) ? TimeSpan.Zero : reader.GetTimeSpan(reader.GetOrdinal("Time")),
                        Price = reader.IsDBNull(reader.GetOrdinal("Price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("Price"))
                    });
                }
            }
        }

        return transports;
    }

    public void UpdateAccommodationForTrip(int tripId, int accommodationId, int nights, int guests)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = connection.CreateCommand())
        {
            connection.Open();
            command.CommandType = CommandType.Text;
            command.CommandText = @"UPDATE Trip
                                    SET AccommodationId = @AccommodationId,
                                        Nights = @Nights,
                                        Guests = @Guests
                                    WHERE Id = @TripId";
            command.Parameters.AddWithValue("@AccommodationId", accommodationId);
            command.Parameters.AddWithValue("@TripId", tripId);
            command.Parameters.AddWithValue("@Nights", nights);
            command.Parameters.AddWithValue("@Guests", guests);

            command.ExecuteNonQuery();
        }
    }
}
