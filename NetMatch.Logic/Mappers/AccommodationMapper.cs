using NetMatch.DAL.DAL;
using NetMatch.Logic.Models;
using System.Linq;
using System.Collections.Generic;

namespace NetMatch.Logic.Mappers
{
    public static class AccommodationMapper
    {
        public static Accommodation ToModel(AccommodationDTO dto)
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
                FromPrice = 0, 
                RoomTypes = dto.RoomTypes?.Select(ToModel).ToList() ?? new List<RoomType>()
            };
        }

        public static AccommodationDTO ToEntity(Accommodation model)
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
                ImageUrl = model.ImageUrl
            };
        }

        public static RoomType ToModel(NetMatch.DAL.DAL.RoomType entity)
        {
            if (entity == null) return null;
            
            return new RoomType
            {
                Id = entity.Id,
                Name = entity.Name,
                PricePerNight = entity.PricePerNight,
                AccommodationId = entity.AccommodationId
            };
        }

        public static NetMatch.DAL.DAL.RoomType ToEntity(RoomType model)
        {
            if (model == null) return null;
            
            return new NetMatch.DAL.DAL.RoomType
            {
                Id = model.Id,
                Name = model.Name,
                PricePerNight = model.PricePerNight,
                AccommodationId = model.AccommodationId
            };
        }
    }
}