using Microsoft.AspNetCore.Mvc;
using Netmatch_opdracht.Models;
using NetMatch.Logic.Models;
using NetMatch.Logic.Services;

namespace Netmatch_opdracht.Controllers;

public class OfferteController : Controller
{
    private readonly OfferteService _offerteService;

    public OfferteController(OfferteService offerteService)
    {
        _offerteService = offerteService;
    }

    // GET: Offerte/Index - Show all offertes
    public IActionResult Index()
    {
        var offertes = _offerteService.GetAllOffertes();
        return View(offertes);
    }

    // GET: Offerte/Create - Show create form
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: Offerte/Create - Handle form submission
    [HttpPost]
    public IActionResult Create(CreateOfferteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Map ViewModel to domain model
        var offerte = new OfferteClass
        {
            Name = model.OfferteNaam
        };
        
        // Create via service
        _offerteService.Create(offerte);
        
        TempData["SuccessMessage"] = $"Offerte '{offerte.Name}' succesvol aangemaakt!";
        return RedirectToAction("Index");
    }
}