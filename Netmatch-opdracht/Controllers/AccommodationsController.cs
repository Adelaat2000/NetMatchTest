using Microsoft.AspNetCore.Mvc;
using NetMatch.Logic.Models;
using NetMatch.Logic.Services;
using System.Collections.Generic;

namespace Netmatch_opdracht.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccommodationsController : ControllerBase
    {
        private readonly AccommodationService _accommodationService;

        public AccommodationsController(AccommodationService accommodationService)
        {
            _accommodationService = accommodationService;
        }

        [HttpPost]
        public IActionResult CreateAccommodation([FromBody] Accommodation accommodation)
        {
            _accommodationService.CreateAccommodation(accommodation);
            return CreatedAtAction(nameof(GetAccommodationById), new { id = accommodation.Id }, accommodation);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Accommodation>> GetAllAccommodations()
        {
            return Ok(_accommodationService.GetAllAccommodations());
        }

        [HttpGet("{id}")]
        public ActionResult<Accommodation> GetAccommodationById(int id)
        {
            Accommodation accommodation = _accommodationService.GetAccommodationById(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            return Ok(accommodation);
        }

        [HttpGet("type/{type}")]
        public ActionResult<IEnumerable<Accommodation>> GetAccommodationsByType(string type)
        {
            return Ok(_accommodationService.GetAccommodationsByType(type));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccommodation(int id, [FromBody] Accommodation accommodation)
        {
            if (id != accommodation.Id)
            {
                return BadRequest("ID in URL komt niet overeen met ID in body.");
            }
            _accommodationService.UpdateAccommodation(accommodation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccommodation(int id)
        {
            _accommodationService.DeleteAccommodation(id);
            return NoContent();
        }

        [HttpPost("roomtype")]
        public IActionResult CreateRoomType([FromBody] RoomType roomType)
        {
            _accommodationService.CreateRoomType(roomType);
            return Ok(roomType);
        }

        [HttpDelete("roomtype/{id}")]
        public IActionResult DeleteRoomType(int id)
        {
            _accommodationService.DeleteRoomType(id);
            return NoContent();
        }
    }
}
