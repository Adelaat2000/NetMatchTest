using Microsoft.AspNetCore.Mvc;
using Netmatch_opdracht.Models.ViewModels;
using NetMatch.Logic.Services;

namespace Netmatch_opdracht.Controllers
{
    public class OfferteBuilderController : Controller
    {
        private readonly AccommodationService _accommodationService;
        public OfferteBuilderController(AccommodationService accommodationService)
        {
            _accommodationService = accommodationService;
        }
    
        public IActionResult Index()
        {
            var viewModel = new OfferteBuilderViewModel
            {
                ItineraryName = "Parisian Dreams Itinerary",
                Destination = "Paris, Frankrijk",
                SelectedCategory = "accommodations"
            };

            return View(viewModel);
        }

        // Partial views voor verschillende categorieën
        public IActionResult GetBestemming()
        {
            return PartialView("_BestemmingSelection");
        }

        //Accomodations
        public IActionResult GetHotels()
        {
            var hotels = _accommodationService.GetAccommodationsByType("Hotel");        
            return PartialView("_AccommodationList", hotels);
        }

        public IActionResult GetResorts()
        {
            var resorts = _accommodationService.GetAccommodationsByType("Resort");
            return PartialView("_AccommodationList", resorts);
        }

        public IActionResult GetVillas()
        {
            var villas = _accommodationService.GetAccommodationsByType("Villa");
            return PartialView("_AccommodationList", villas);
        }

        public IActionResult GetActivities()
        {
            return PartialView("_ActivitySelection");
        }

        public IActionResult GetCarRentals()
        {
            return PartialView("_CarRentalSelection");
        }

        public IActionResult GetTrainTickets()
        {
            return PartialView("_TrainTicketSelection");
        }

        public IActionResult GetFlights()
        {
            return PartialView("_FlightSelection");
        }

        // Rechter sidebar partial views
        public IActionResult GetReisOverzicht()
        {
            var viewModel = new OfferteBuilderViewModel
            {
                ItineraryName = "Parisian Dreams Itinerary",
                Destination = "Paris, Frankrijk",
                TotalPrice = 0
            };
            return PartialView("_ReisOverzicht", viewModel);
        }

        public IActionResult GetActivityDetailForm()
        {
            return PartialView("_ActivityDetailForm");
        }
    }
}