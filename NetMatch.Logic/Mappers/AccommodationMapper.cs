using System.Collections.Generic;
using System.Linq;
using NetMatch.DAL.DAL;
using NetMatch.Logic.Models;
using DalAccommodationDto = NetMatch.DAL.DAL.AccommodationDTO;
using DalRoomType = NetMatch.DAL.DAL.RoomType;
using LogicRoomType = NetMatch.Logic.Models.RoomType;

namespace NetMatch.Logic.Mappers
{
    public static class AccommodationMapper
    {
        public static Accommodation ToModel(DalAccommodationDto dto)
        {
            if (dto == null) return null;

            return new Accommodation
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                Location = dto.Location,
                StarRating = dto.StarRating,
                Description = dto.Description,
                Rating = dto.Rating,
                ReviewCount = dto.ReviewCount,
                ImageUrl = dto.ImageUrl,
                FromPrice = dto.FromPrice,
                RoomTypes = dto.RoomTypes?.Select(ToModel).ToList() ?? new List<LogicRoomType>()
            };
        }

        public static DalAccommodationDto ToEntity(Accommodation model)
        {
            if (model == null) return null;

            return new AccommodationDTO
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Location = model.Location,
                StarRating = model.StarRating,
                Description = model.Description,
                Rating = model.Rating,
                ReviewCount = model.ReviewCount,
                ImageUrl = model.ImageUrl,
                FromPrice = model.FromPrice,
                RoomTypes = model.RoomTypes?.Select(ToEntity).ToList() ?? new List<DalRoomType>()
            };
        }

        public static LogicRoomType ToModel(DalRoomType entity)
        {
            if (entity == null) return null;

            return new LogicRoomType
            {
                Id = entity.Id,
                Name = entity.Name,
                PricePerNight = entity.PricePerNight,
                AccommodationId = entity.AccommodationId
            };
        }

        public static DalRoomType ToEntity(LogicRoomType model)
        {
            if (model == null) return null;

            return new DalRoomType
            {
                Id = model.Id,
                Name = model.Name,
                PricePerNight = model.PricePerNight,
                AccommodationId = model.AccommodationId
            };
        }
    }
}
