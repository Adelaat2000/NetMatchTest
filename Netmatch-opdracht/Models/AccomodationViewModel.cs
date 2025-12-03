using System.Collections.Generic;

namespace Netmatch_opdracht.Models
{
    public class AccommodationViewModel
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
        public decimal PriceForStay { get; set; }
    }

    public class AccommodationSelectionViewModel
    {
        public int TripId { get; set; }
        public int AccommodationId { get; set; }
        public int Nights { get; set; }
        public int Guests { get; set; }
    }

    public class AccommodationListViewModel
    {
        public int TripId { get; set; }
        public int Nights { get; set; }
        public int Guests { get; set; }
        public List<AccommodationViewModel> Accommodations { get; set; } = new List<AccommodationViewModel>();
    }
}