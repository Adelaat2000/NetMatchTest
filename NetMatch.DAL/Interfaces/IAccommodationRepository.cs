using NetMatch.DAL.DAL;
using NetMatch.Logic.Models;

namespace NetMatch.Dal.Interfaces

{
    public interface IAccommodationRepository
    {
        // CRUD voor Accommodaties
        void Create(Accommodation accommodation);
        Accommodation GetById(int id);
        IEnumerable<Accommodation> GetAll();
        IEnumerable<Accommodation> GetByType(string type);
        void Update(Accommodation accommodation);
        void Delete(int id);
        
        // CRUD voor Kamertypes
        void CreateRoomType(RoomType roomType);
        RoomType GetRoomTypeById(int id);
        void UpdateRoomType(RoomType roomType);
        void DeleteRoomType(int id);
    }
}