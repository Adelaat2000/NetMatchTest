using NetMatch.DAL.Interfaces;
using NetMatch.Logic.Models;
using System.Collections.Generic;
using NetMatch.Logic.Mappers;
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
            var accommodationEntity = AccommodationMapper.ToEntity(accommodation);
            _accommodationRepository.Create(accommodationEntity);
        }
        
        public Accommodation GetAccommodationById(int id)
        {
            var accommodationEntity = _accommodationRepository.GetById(id);
            return AccommodationMapper.ToModel(accommodationEntity);
        }

        public IEnumerable<Accommodation> GetAllAccommodations()
        {
            var accommodationEntities = _accommodationRepository.GetAll();
            return accommodationEntities.Select(AccommodationMapper.ToModel);
        }

        public IEnumerable<Accommodation> GetAccommodationsByType(string type)
        {
            var accommodationEntities = _accommodationRepository.GetByType(type);
            return accommodationEntities.Select(AccommodationMapper.ToModel);
        }

        public void UpdateAccommodation(Accommodation accommodation)
        {
            var accommodationEntity = AccommodationMapper.ToEntity(accommodation);
            _accommodationRepository.Update(accommodationEntity);
        }

        public void DeleteAccommodation(int id)
        {
            _accommodationRepository.Delete(id);
        }
        public void CreateRoomType(RoomType roomType)
        {
            var roomTypeEntity = AccommodationMapper.ToEntity(roomType);
            _accommodationRepository.CreateRoomType(roomTypeEntity);
        }
        
        public RoomType GetRoomTypeById(int id)
        {
            var roomTypeEntity = _accommodationRepository.GetRoomTypeById(id);
            return AccommodationMapper.ToModel(roomTypeEntity);
        }

        public void UpdateRoomType(RoomType roomType)
        {
            var roomTypeEntity = AccommodationMapper.ToEntity(roomType);
            _accommodationRepository.UpdateRoomType(roomTypeEntity);
        }

        public void DeleteRoomType(int id)
        {
            _accommodationRepository.DeleteRoomType(id);
        }
    }
}