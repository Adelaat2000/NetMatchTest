using NetMatch.Logic.Models;

namespace Netmatch_opdracht.Models;

public class HomeViewModel
{
    public CreateOfferteViewModel CreateOfferte { get; set; } = new CreateOfferteViewModel();
    public List<OfferteClass> Offertes { get; set; } = new List<OfferteClass>();
}