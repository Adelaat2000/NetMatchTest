using NetMatch.Logic.Models;

namespace NetMatch.DAL.Interfaces;

public interface IOfferteRepository
{
    void Create(OfferteClass offerte);
    IEnumerable<OfferteClass> GetAll();
    OfferteClass GetById(int id);
    void Update(OfferteClass offerte);
    void Delete(int id);
}