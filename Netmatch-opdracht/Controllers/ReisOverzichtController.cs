using Microsoft.AspNetCore.Mvc;
using NetMatch.Logic.Services;
using NetMatch.Logic.Models;
using Netmatch_opdracht.Models.ViewModels;
using System.Linq;
using Netmatch_opdracht.Models;

namespace Netmatch_opdracht.Controllers
{
    public class ReisOverzichtController : Controller
    {
        private readonly ReisOverzichtService _service;

        public ReisOverzichtController(ReisOverzichtService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult SelectAccommodation([FromBody] AccommodationSelectionViewModel selection)
        {
            if (selection == null)
            {
                return BadRequest("Geen selectie ontvangen.");
            }

            ReisOverzichtModel.Trip trip = _service.UpdateAccommodationForTrip(
                selection.TripId,
                selection.AccommodationId,
                selection.Nights,
                selection.Guests);

            ReisOverzichtViewModel viewModel = MapTripToViewModel(trip);
            return PartialView("~/Views/OfferteBuilder/_ReisOverzichtSummary.cshtml", viewModel);
        }

        public IActionResult ToonReisOverzicht(int tripId)
        {
            ReisOverzichtModel.Trip trip = _service.GetTripById(tripId);

            ReisOverzichtViewModel viewModel = MapTripToViewModel(trip);
            return View("~/Views/OfferteBuilder/_ToonReisOverzicht.cshtml", viewModel);
        }

        private ReisOverzichtViewModel MapTripToViewModel(ReisOverzichtModel.Trip trip)
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
                    Time = t.Time.ToString(@"hh\\:mm"),
                    Price = $"{t.Price:C}"
                }).ToList()
            };
        }
    }
}
