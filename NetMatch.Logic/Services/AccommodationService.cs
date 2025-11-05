using NetMatch.DAL.Interfaces; // Gebruikt de Interface (Stap 2)
using NetMatch.Logic.Models;    // Gebruikt de Logic Models (Stap 1)
using System.Collections.Generic;
using NetMatch.DAL.DAL;
using NetMatch.Dal.Interfaces;

namespace NetMatch.Logic.Services
{
    public class AccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationService(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        // --- Accommodatie Methoden ---

        public void CreateAccommodation(Accommodation accommodation)
        {
            _accommodationRepository.Create(accommodation);
        }

        // Deze methode geeft 'Accommodation' terug (uit Logic.Models)
        public Accommodation GetAccommodationById(int id)
        {
            return _accommodationRepository.GetById(id);
        }

        public IEnumerable<Accommodation> GetAllAccommodations()
        {
            return _accommodationRepository.GetAll();
        }

        public IEnumerable<Accommodation> GetAccommodationsByType(string type)
        {
            return _accommodationRepository.GetByType(type);
        }

        public void UpdateAccommodation(Accommodation accommodation)
        {
            _accommodationRepository.Update(accommodation);
        }

        public void DeleteAccommodation(int id)
        {
            _accommodationRepository.Delete(id);
        }

        // --- Kamertype Methoden ---

        public void CreateRoomType(RoomType roomType)
        {
            _accommodationRepository.CreateRoomType(roomType);
        }

        // HIER ZAT DE FOUT:
        // Deze methode geeft 'RoomType' terug (uit Logic.Models), geen void.
        public RoomType GetRoomTypeById(int id)
        {
            return _accommodationRepository.GetRoomTypeById(id);
        }

        public void UpdateRoomType(RoomType roomType)
        {
            _accommodationRepository.UpdateRoomType(roomType);
        }

        public void DeleteRoomType(int id)
        {
            _accommodationRepository.DeleteRoomType(id);
        }
    }
}