using Microsoft.AspNetCore.Mvc;

namespace Netmatch_opdracht.Controllers
{
    // Dit is een 'Controller' (voor webpaginas), 
    // GEEN 'ApiController' (voor data).
    public class AccommodationController : Controller
    {
        // Deze methode wordt aangeroepen als de gebruiker 
        // naar https://.../Accommodation gaat.
        public IActionResult Index()
        {
            // Dit zegt: "Zoek het bestand 
            // 'Views/Accommodation/Index.cshtml' en stuur dat
            // als HTML-pagina naar de browser."
            return View();
        }
    }
}