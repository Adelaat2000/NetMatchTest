using System.Data;
using Microsoft.Data.SqlClient;
using NetMatch.DAL.DAL;
using NetMatch.Logic.Models;
using NetMatch.Dal.Interfaces;
using NetMatch.DAL.DAL;



namespace NetMatch.DAL.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly string _connectionString;
        
        public AccommodationRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) 
                throw new ArgumentException("connectionString required", nameof(connectionString));
            _connectionString = connectionString;
        }

        public void Create(Accommodation accommodation)
        {
            if (accommodation == null) throw new ArgumentNullException(nameof(accommodation));

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO Accommodations 
                                      (Name, Type, Location, StarRating, Description, Rating, ReviewCount, ImageUrl) 
                                  VALUES 
                                      (@Name, @Type, @Location, @StarRating, @Description, @Rating, @ReviewCount, @ImageUrl);
                                  SELECT CAST(SCOPE_IDENTITY() AS int);"; // Geeft de nieuwe ID terug

                cmd.Parameters.AddWithValue("@Name", (object)accommodation.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Type", (object)accommodation.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Location", (object)accommodation.Location ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StarRating", accommodation.StarRating);
                cmd.Parameters.AddWithValue("@Description", (object)accommodation.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Rating", accommodation.Rating);
                cmd.Parameters.AddWithValue("@ReviewCount", accommodation.ReviewCount);
                cmd.Parameters.AddWithValue("@ImageUrl", (object)accommodation.ImageUrl ?? DBNull.Value);

                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out var id))
                {
                    accommodation.Id = id;
                }
            }
        }

        public Accommodation GetById(int id)
        {
            Accommodation accommodation = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                //Haal de accommodatie op
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Accommodations WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            accommodation = MapReaderToAccommodation(reader);
                        }
                    }
                }
                
                if (accommodation == null) return null;

                //Haal de kamers op
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM RoomTypes WHERE AccommodationId = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accommodation.RoomTypes.Add(MapReaderToRoomType(reader));
                        }
                    }
                }
            }
            return accommodation;
        }

        public IEnumerable<Accommodation> GetAll()
        {
            var list = new List<Accommodation>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Accommodations";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapReaderToAccommodation(reader));
                    }
                }
            }
            return list;
        }

        public IEnumerable<Accommodation> GetByType(string type)
        {
            var list = new List<Accommodation>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM Accommodations WHERE Type = @Type";
                cmd.Parameters.AddWithValue("@Type", type);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapReaderToAccommodation(reader));
                    }
                }
            }
            return list;
        }

        public void Update(Accommodation accommodation)
        {
            if (accommodation == null) throw new ArgumentNullException(nameof(accommodation));

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE Accommodations SET 
                                      Name = @Name, Type = @Type, Location = @Location, 
                                      StarRating = @StarRating, Description = @Description, 
                                      Rating = @Rating, ReviewCount = @ReviewCount, ImageUrl = @ImageUrl
                                  WHERE Id = @Id";
                
                cmd.Parameters.AddWithValue("@Id", accommodation.Id);
                cmd.Parameters.AddWithValue("@Name", (object)accommodation.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Type", (object)accommodation.Type ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Location", (object)accommodation.Location ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@StarRating", accommodation.StarRating);
                cmd.Parameters.AddWithValue("@Description", (object)accommodation.Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Rating", accommodation.Rating);
                cmd.Parameters.AddWithValue("@ReviewCount", accommodation.ReviewCount);
                cmd.Parameters.AddWithValue("@ImageUrl", (object)accommodation.ImageUrl ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "DELETE FROM RoomTypes WHERE AccommodationId = @Id";
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }
                        
                        //Verwijder accommodatie
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "DELETE FROM Accommodations WHERE Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        
        public void CreateRoomType(RoomType roomType)
        {
            if (roomType == null) throw new ArgumentNullException(nameof(roomType));

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO RoomTypes (Name, PricePerNight, AccommodationId) 
                                  VALUES (@Name, @PricePerNight, @AccommodationId);
                                  SELECT CAST(SCOPE_IDENTITY() AS int);";
                
                cmd.Parameters.AddWithValue("@Name", (object)roomType.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PricePerNight", roomType.PricePerNight);
                cmd.Parameters.AddWithValue("@AccommodationId", roomType.AccommodationId);

                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out var id))
                {
                    roomType.Id = id;
                }
            }
        }
        public RoomType GetRoomTypeById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM RoomTypes WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapReaderToRoomType(reader); // Geef RoomType terug
                    }
                }
            }
            return null; // Niet gevonden
        }
        public void UpdateRoomType(RoomType roomType)
        {
            if (roomType == null) throw new ArgumentNullException(nameof(roomType));

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE RoomTypes SET 
                                      Name = @Name, PricePerNight = @PricePerNight, 
                                      AccommodationId = @AccommodationId 
                                  WHERE Id = @Id";
                
                cmd.Parameters.AddWithValue("@Id", roomType.Id);
                cmd.Parameters.AddWithValue("@Name", (object)roomType.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PricePerNight", roomType.PricePerNight);
                cmd.Parameters.AddWithValue("@AccommodationId", roomType.AccommodationId);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRoomType(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM RoomTypes WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }
        
        
        private Accommodation MapReaderToAccommodation(SqlDataReader reader)
        {
            return new Accommodation
            {
                Id = reader.GetInt32("Id"),
                Name = reader.IsDBNull("Name") ? null : reader.GetString("Name"),
                Type = reader.IsDBNull("Type") ? null : reader.GetString("Type"),
                Location = reader.IsDBNull("Location") ? null : reader.GetString("Location"),
                StarRating = reader.GetInt32("StarRating"),
                Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                Rating = reader.GetDecimal("Rating"),
                ReviewCount = reader.GetInt32("ReviewCount"),
                ImageUrl = reader.IsDBNull("ImageUrl") ? null : reader.GetString("ImageUrl")
            };
        }

        private RoomType MapReaderToRoomType(SqlDataReader reader)
        {
            return new RoomType
            {
                Id = reader.GetInt32("Id"),
                Name = reader.IsDBNull("Name") ? null : reader.GetString("Name"),
                PricePerNight = reader.GetDecimal("PricePerNight"),
                AccommodationId = reader.GetInt32("AccommodationId")
            };
        }
    }
}