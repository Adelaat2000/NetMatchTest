namespace NetMatch.DAL.DTO;

public class ReisOverzichtDTO
{
    public class AccommodationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Nights { get; set; }
        public int Guests { get; set; }
        public decimal Price { get; set; }
    }

    public class TransportDto
    {
        public int Id { get; set; }
        public string Route { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal Price { get; set; }
    }
}