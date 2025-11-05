using Microsoft.AspNetCore.Mvc;

namespace Netmatch_opdracht.Controllers
{
    public class AccommodationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}