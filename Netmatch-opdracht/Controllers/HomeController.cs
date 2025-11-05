using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Netmatch_opdracht.Models;
using NetMatch.Logic.Services;
using NetMatch.Logic.Models;

namespace Netmatch_opdracht.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly OfferteService _offerteService;

    public HomeController(ILogger<HomeController> logger, OfferteService offerteService)
    {
        _logger = logger;
        _offerteService = offerteService;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            Offertes = _offerteService.GetAllOffertes()
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult CreateOfferte(HomeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Als validatie faalt, toon de home pagina opnieuw met errors
            model.Offertes = _offerteService.GetAllOffertes();
            return View("Index", model);
        }

        // Map ViewModel to domain model
        var offerte = new OfferteClass
        {
            Naam = model.CreateOfferte.OfferteNaam
        };
        
        // Create via service
        _offerteService.Create(offerte);
        
        TempData["SuccessMessage"] = $"Offerte '{offerte.Naam}' succesvol aangemaakt!";
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}