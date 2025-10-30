using NetMatch.DAL.DTO;
using System.Collections.Generic;

namespace NetMatch.DAL.Interfaces
{
    public interface IReisOverzichtRepository
    {
        ReisOverzichtDTO.AccommodationDto GetAccommodationByTripId(int tripId);
        List<ReisOverzichtDTO.TransportDto> GetTransportsByTripId(int tripId);
    }
}