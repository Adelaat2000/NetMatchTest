using NetMatch.Logic.Models;
using NetMatch.DAL.Interfaces;
using NetMatch.DAL.DTO;
using System.Collections.Generic;
using System.Linq;

namespace NetMatch.Logic.Services
{
    /// <summary>
    /// Service for managing Offerte business logic.
    /// Depends on IOfferteRepository via constructor injection.
    /// Handles all mapping from DTOs to domain models.
    /// </summary>
    public class OfferteService
    {
        private readonly IOfferteRepository _repository;

        /// <summary>
        /// Constructor injection of the repository interface.
        /// This ensures the Logic layer only depends on abstractions, not concrete DAL implementations.
        /// </summary>
        /// <param name="repository">The repository interface for data access</param>
        public OfferteService(IOfferteRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all offertes and maps them to domain models.
        /// </summary>
        /// <returns>List of Offerte domain models</returns>
        public List<OfferteClass> GetAllOffertes()
        {
            // Fetch DTOs from repository
            var dtos = _repository.GetAll();
            
            // Map DTOs to domain models
            return dtos.Select(dto => new OfferteClass
            {
                Id = dto.Id,
                Naam = dto.Naam
            }).ToList();
        }

        /// <summary>
        /// Retrieves a single offerte by ID.
        /// </summary>
        /// <param name="id">The offerte ID</param>
        /// <returns>The offerte domain model, or null if not found</returns>
        public OfferteClass GetOfferteById(int id)
        {
            var dto = _repository.GetById(id);
            
            if (dto == null)
                return null;
                
            // Map DTO to domain model
            return new OfferteClass
            {
                Id = dto.Id,
                Naam = dto.Naam
            };
        }
        
        /// <summary>
        /// Creates a new offerte.
        /// </summary>
        /// <param name="offerte">The offerte domain model to create</param>
        public void Create(OfferteClass offerte)
        {
            if (offerte == null)
                throw new System.ArgumentNullException(nameof(offerte));
                
            // Map domain model to DTO
            var dto = new OfferteDTO
            {
                Id = offerte.Id,
                Naam = offerte.Naam
            };
            
            // Create via repository
            _repository.Create(dto);
            
            // Update the domain model with the generated ID
            offerte.Id = dto.Id;
        }

        /// <summary>
        /// Updates an existing offerte.
        /// </summary>
        /// <param name="offerte">The offerte domain model to update</param>
        public void Update(OfferteClass offerte)
        {
            if (offerte == null)
                throw new System.ArgumentNullException(nameof(offerte));
                
            // Map domain model to DTO
            var dto = new OfferteDTO
            {
                Id = offerte.Id,
                Naam = offerte.Naam
            };
            
            _repository.Update(dto);
        }

        /// <summary>
        /// Deletes an offerte by ID.
        /// </summary>
        /// <param name="id">The ID of the offerte to delete</param>
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
