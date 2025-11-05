// File: NetMatch.DAL/Repositories/ReisOverzichtRepository.cs
using NetMatch.DAL.DTO;

using System;
using System.Collections.Generic;
using NetMatch.DAL.Interfaces;

namespace NetMatch.DAL.Repositories
{
    public class ReisOverzichtRepository : IReisOverzichtRepository
    {
        public ReisOverzichtDTO.AccommodationDto GetAccommodationByTripId(int tripId)
        {
            return new ReisOverzichtDTO.AccommodationDto
            {
                Id = tripId,
                Name = "Hotel Le Royal Monceau",
                ImageUrl = "/images/hotel-monseau.jpg",
                Nights = 3,
                Guests = 2,
                Price = 2250m
            };
        }

        public List<ReisOverzichtDTO.TransportDto> GetTransportsByTripId(int tripId)
        {
            return new List<ReisOverzichtDTO.TransportDto>
            {
                new ReisOverzichtDTO.TransportDto
                {
                    Id = 1,
                    Route = "AMS → CDG",
                    Date = new DateTime(2025,10,22),
                    Time = new TimeSpan(8,30,0),
                    Price = 350m
                },
                new ReisOverzichtDTO.TransportDto
                {
                    Id = 2,
                    Route = "CDG → AMS",
                    Date = new DateTime(2025,10,25),
                    Time = new TimeSpan(19,45,0),
                    Price = 350m
                }
            };
        }
    }
}