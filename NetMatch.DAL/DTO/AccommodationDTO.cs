using System.Collections.Generic;

namespace NetMatch.DAL.DAL
{
    public class AccommodationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public int StarRating { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
        public string ImageUrl { get; set; }
        public decimal FromPrice { get; set; }
        public virtual ICollection<RoomType> RoomTypes { get; set; }
    }
}