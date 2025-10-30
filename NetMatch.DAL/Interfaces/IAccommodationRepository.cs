using NetMatch.Logic.Models;
using System.Collections.Generic;
using NetMatch.DAL.DAL;

namespace NetMatch.DAL.Interfaces
{
    public interface IAccommodationRepository
    {
        // CRUD voor Accommodaties
        void Create(Accommodation accommodation);
        Accommodation GetById(int id);
        IEnumerable<Accommodation> GetAll();
        IEnumerable<Accommodation> GetByType(string type); //
        void Update(Accommodation accommodation);
        void Delete(int id);
        
        // CRUD voor Kamertypes
        void CreateRoomType(RoomType roomType);
        void GetRoomTypeById(int id);
        void UpdateRoomType(RoomType roomType);
        void DeleteRoomType(int id);
    }
}