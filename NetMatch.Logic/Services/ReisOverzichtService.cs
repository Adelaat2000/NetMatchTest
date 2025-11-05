using NetMatch.Logic.Models;
using NetMatch.DAL.Interfaces;
using System.Linq;

namespace NetMatch.Logic.Services
{
    /// <summary>
    /// Service for managing trip overview business logic.
    /// Depends on IReisOverzichtRepository via constructor injection.
    /// Handles all mapping from DTOs to domain models.
    /// </summary>
    public class ReisOverzichtService
    {
        private readonly IReisOverzichtRepository _repository;

        /// <summary>
        /// Constructor injection of the repository interface.
        /// This ensures the Logic layer only depends on abstractions, not concrete DAL implementations.
        /// </summary>
        /// <param name="repository">The repository interface for data access</param>
        public ReisOverzichtService(IReisOverzichtRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a trip by ID, including all business logic and DTO-to-Model mapping.
        /// </summary>
        /// <param name="tripId">The unique identifier of the trip</param>
        /// <returns>A fully populated Trip model with calculated totals</returns>
        public ReisOverzichtModel.Trip GetTripById(int tripId)
        {
            // Fetch raw data from DAL via the interface
            var accommodationDto = _repository.GetAccommodationByTripId(tripId);
            var transportDtos = _repository.GetTransportsByTripId(tripId);

            // Map Accommodation DTO → Domain Model
            var accommodation = new ReisOverzichtModel.Accommodation
            {
                Name = accommodationDto.Name,
                ImageUrl = accommodationDto.ImageUrl,
                Nights = accommodationDto.Nights,
                Guests = accommodationDto.Guests,
                Price = accommodationDto.Price
            };

            // Map Transport DTOs → Domain Models
            var transports = transportDtos.Select(t => new ReisOverzichtModel.Transport
            {
                Route = t.Route,
                Date = t.Date,
                Time = t.Time,
                Price = t.Price
            }).ToList();

            // Business logic: Calculate subtotal and taxes
            decimal subtotal = accommodation.Price + transports.Sum(t => t.Price);
            decimal taxes = subtotal * 0.1m; // 10% tax rule (business logic)

            // Return the complete domain model
            return new ReisOverzichtModel.Trip
            {
                Accommodation = accommodation,
                Transports = transports,
                Subtotal = subtotal,
                Taxes = taxes
            };
        }
    }
}