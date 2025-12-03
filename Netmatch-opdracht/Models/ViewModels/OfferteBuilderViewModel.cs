using System.Collections.Generic;
using Netmatch_opdracht.Models;

namespace Netmatch_opdracht.Models.ViewModels
{
    public class OfferteBuilderViewModel
    {
        public string ItineraryName { get; set; } = "Parisian Dreams Itinerary";
        public string Destination { get; set; } = "Paris, Frankrijk";
        public string SelectedCategory { get; set; } = "accommodations"; // accommodations, activities, transportation
        public decimal TotalPrice { get; set; } = 0;
        public List<SelectedComponent> SelectedComponents { get; set; } = new List<SelectedComponent>();
        public int TripId { get; set; } = 1;
        public ReisOverzichtViewModel ReisOverzicht { get; set; }
    }

    public class SelectedComponent
    {
        public string Type { get; set; } = ""; // ACCOMMODATIE, VERVOER, ACTIVITEIT
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Details { get; set; }
    }
}