using System.Collections.Generic;
using NetMatch.DAL.DAL;

namespace NetMatch.Logic.Models
{
    public class Accommodation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Hotel", "Resort", "Villa"
        public string Location { get; set; }
        public int StarRating { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
        public string ImageUrl { get; set; }
        
        // Relatie naar kamertypes
        public ICollection<RoomType> RoomTypes { get; set; } = new List<RoomType>();
    }
}