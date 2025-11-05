using NetMatch.DAL.Interfaces;
using NetMatch.Logic.Models;
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

        public void CreateAccommodation(Accommodation accommodation)
        {
            _accommodationRepository.Create(accommodation);
        }
        
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
        
        public void CreateRoomType(RoomType roomType)
        {
            _accommodationRepository.CreateRoomType(roomType);
        }
        
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