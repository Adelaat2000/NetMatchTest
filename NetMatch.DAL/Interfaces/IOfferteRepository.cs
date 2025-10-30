using NetMatch.DAL.DTO;

namespace NetMatch.DAL.Interfaces;

/// <summary>
/// Repository interface for Offerte data access.
/// Works with DTOs to maintain separation between DAL and Logic layers.
/// </summary>
public interface IOfferteRepository
{
    void Create(OfferteDTO offerte);
    IEnumerable<OfferteDTO> GetAll();
    OfferteDTO GetById(int id);
    void Update(OfferteDTO offerte);
    void Delete(int id);
}