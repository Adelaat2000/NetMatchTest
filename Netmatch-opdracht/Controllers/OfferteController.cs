using Netmatch_opdracht.Models;
using NetMatch.Logic.Services;

namespace Netmatch_opdracht.Controllers;
using Microsoft.AspNetCore.Mvc;

public class OfferteController : Controller
{
    private readonly OfferteService _offerteService;

    public OfferteController(OfferteService offerteService)
    {
        _offerteService = offerteService;
    }
    
    [HttpPost]
    public IActionResult Create(CreateOfferteViewModel model)
    {
        //naam moet gecorigeerd worden.
        _offerteService.Create(model);
        
        return RedirectToAction("Index", "Home");
    }
}