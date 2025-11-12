using NetMatch.DAL.DAL;

namespace NetMatch.Dal.Interfaces

{
    public interface IAccommodationRepository
    {
        // CRUD voor Accommodaties
        void Create(AccommodationDTO accommodation);
        AccommodationDTO GetById(int id);
        IEnumerable<AccommodationDTO> GetAll();
        IEnumerable<AccommodationDTO> GetByType(string type);
        void Update(AccommodationDTO accommodation);
        void Delete(int id);
        
        // CRUD voor Kamertypes
        void CreateRoomType(RoomType roomType);
        RoomType GetRoomTypeById(int id);
        void UpdateRoomType(RoomType roomType);
        void DeleteRoomType(int id);
    }
}