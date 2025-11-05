namespace Netmatch_opdracht.Models;

public class ReisOverzichtViewModel
{
    public string AccommodationName { get; set; }
    public string AccommodationImageUrl { get; set; }
    public string AccommodationGuests { get; set; }
    public string AccommodationNights { get; set; }
    public string AccommodationPrice { get; set; }

    public List<TransportViewModel> Transports { get; set; } = new();
    public string Subtotal { get; set; }
    public string Taxes { get; set; }
    public string Total { get; set; }
}

public class TransportViewModel
{
    public string Route { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public string Price { get; set; }
}
