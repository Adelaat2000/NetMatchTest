using Microsoft.AspNetCore.Mvc;
using Netmatch_opdracht.Models;
using NetMatch.Logic.Services; 

namespace Netmatch_opdracht.Controllers
{
    public class ReisOverzichtController : Controller
    {
        private readonly ReisOverzichtService _service;

        public ReisOverzichtController(ReisOverzichtService service)
        {
            _service = service;
        }

        public IActionResult ToonReisOverzicht(int tripId)
        {
            var trip = _service.GetTripById(tripId);

            var vm = new ReisOverzichtViewModel
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

            return View(vm);
        }
    }
}