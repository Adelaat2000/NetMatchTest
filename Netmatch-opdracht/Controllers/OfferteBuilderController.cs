using Microsoft.AspNetCore.Mvc;
using Netmatch_opdracht.Models.ViewModels;

namespace Netmatch_opdracht.Controllers
{
    public class OfferteBuilderController : Controller
    {
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

        public IActionResult GetHotels()
        {
            return PartialView("_HotelSelection");
        }

        public IActionResult GetResorts()
        {
            return PartialView("_ResortSelection");
        }

        public IActionResult GetVillas()
        {
            return PartialView("_VillaSelection");
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