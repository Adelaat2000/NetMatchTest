namespace NetMatch.DAL.DAL 
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PricePerNight { get; set; }

        public int AccommodationId { get; set; }
        
    }
}