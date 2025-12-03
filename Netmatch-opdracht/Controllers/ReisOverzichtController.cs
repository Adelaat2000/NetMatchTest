using Microsoft.AspNetCore.Mvc;
using Netmatch_opdracht.Models;
using NetMatch.Logic.Services;
using Netmatch_opdracht.Models.ViewModels;
using System.Linq;

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

            Logic.Models.ReisOverzichtModel.Trip trip = _service.UpdateAccommodationForTrip(
                selection.TripId,
                selection.AccommodationId,
                selection.Nights,
                selection.Guests);

            ReisOverzichtViewModel viewModel = MapTripToViewModel(trip);
            return PartialView("_ReisOverzichtSummary", viewModel);
        }

        public IActionResult ToonReisOverzicht(int tripId)
        {
            var trip = _service.GetTripById(tripId);
            ReisOverzichtViewModel vm = MapTripToViewModel(trip);

            return View(vm);
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