namespace NetMatch.Logic.Models;

public class ReisOverzichtModel
{
    public class Accommodation
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Nights { get; set; }
        public int Guests { get; set; }
        public decimal Price { get; set; }
    }

    public class Transport
    {
        public string Route { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal Price { get; set; }
    }

    public class Trip
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; }
        public List<Transport> Transports { get; set; } = new();
        public int Nights { get; set; }
        public int Guests { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxes { get; set; }
        public decimal Total => Subtotal + Taxes;
    }
}


