using Microsoft.AspNetCore.Mvc;
using Netmatch_opdracht.Models;
using Netmatch_opdracht.Models.ViewModels;
using NetMatch.Logic.Services;
using NetMatch.Logic.Models;
using System.Collections.Generic;
using System.Linq;

namespace Netmatch_opdracht.Controllers
{
    public class OfferteBuilderController : Controller
    {
        private readonly AccommodationService _accommodationService;
        private readonly ReisOverzichtService _reisOverzichtService;

        public OfferteBuilderController(AccommodationService accommodationService, ReisOverzichtService reisOverzichtService)
        {
            _accommodationService = accommodationService;
            _reisOverzichtService = reisOverzichtService;
        }

        public IActionResult Index()
        {
            int tripId = 1;
            ReisOverzichtViewModel reisOverzicht = MapTripToViewModel(_reisOverzichtService.GetTripById(tripId));

            OfferteBuilderViewModel viewModel = new OfferteBuilderViewModel
            {
                ItineraryName = "Parisian Dreams Itinerary",
                Destination = "Paris, Frankrijk",
                SelectedCategory = "accommodations",
                TripId = tripId,
                ReisOverzicht = reisOverzicht
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
            AccommodationListViewModel viewModel = BuildAccommodationList("Hotel");
            return PartialView("_AccommodationList", viewModel);
        }

        public IActionResult GetResorts()
        {
            AccommodationListViewModel viewModel = BuildAccommodationList("Resort");
            return PartialView("_AccommodationList", viewModel);
        }

        public IActionResult GetVillas()
        {
            AccommodationListViewModel viewModel = BuildAccommodationList("Villa");
            return PartialView("_AccommodationList", viewModel);
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
            ReisOverzichtViewModel reisOverzicht = MapTripToViewModel(_reisOverzichtService.GetTripById(1));
            return PartialView("_ReisOverzichtSummary", reisOverzicht);
        }

        public IActionResult GetActivityDetailForm()
        {
            return PartialView("_ActivityDetailForm");
        }

        private AccommodationListViewModel BuildAccommodationList(string type)
        {
            IEnumerable<Accommodation> accommodations = _accommodationService.GetAccommodationsByType(type);
            List<AccommodationViewModel> models = new List<AccommodationViewModel>();

            foreach (Accommodation accommodation in accommodations)
            {
                AccommodationViewModel model = new AccommodationViewModel
                {
                    Id = accommodation.Id,
                    Name = accommodation.Name,
                    Type = accommodation.Type,
                    Location = accommodation.Location,
                    StarRating = accommodation.StarRating,
                    Description = accommodation.Description,
                    Rating = accommodation.Rating,
                    ReviewCount = accommodation.ReviewCount,
                    ImageUrl = accommodation.ImageUrl,
                    FromPrice = accommodation.FromPrice,
                    PriceForStay = accommodation.FromPrice * 3
                };

                models.Add(model);
            }

            return new AccommodationListViewModel
            {
                TripId = 1,
                Nights = 3,
                Guests = 2,
                Accommodations = models
            };
        }

        private ReisOverzichtViewModel MapTripToViewModel(Logic.Models.ReisOverzichtModel.Trip trip)
        {
            if (trip == null)
            {
                return new ReisOverzichtViewModel();
            }

            return new ReisOverzichtViewModel
            {
                AccommodationName = trip.Accommodation.Name,
                AccommodationImageUrl = trip.Accommodation.ImageUrl,
                AccommodationGuests = $"{trip.Accommodation.Guests} gasten",
                AccommodationNights = $"{trip.Accommodation.Nights} nachten",
                AccommodationPrice = $"{trip.Accommodation.Price:C}",
                Subtotal = $"{trip.Subtotal:C}",
                Taxes = $"{trip.Taxes:C}",
                Total = $"{trip.Total:C}",
                Transports = trip.Transports.Select(t => new TransportViewModel
                {
                    Route = t.Route,
                    Date = t.Date.ToString("dd-MM-yyyy"),
                    Time = t.Time.ToString(@"hh\:mm"),
                    Price = $"{t.Price:C}"
                }).ToList()
            };
        }
    }
}