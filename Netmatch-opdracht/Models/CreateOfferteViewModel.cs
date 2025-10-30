using System.ComponentModel.DataAnnotations;

namespace Netmatch_opdracht.Models;

public class CreateOfferteViewModel
{
    [Required(ErrorMessage = "Offerte naam is verplicht")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Naam moet tussen 3 en 100 karakters zijn")]
    [Display(Name = "Offerte Naam")]
    public string OfferteNaam { get; set; } = string.Empty;
}