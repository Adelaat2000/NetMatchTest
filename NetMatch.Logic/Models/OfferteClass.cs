
using System;

namespace NetMatch.Logic.Models
{
    public class OfferteClass
    {
        public OfferteClass() { }

        public OfferteClass(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }

        public int Id { get; set; }
        public string? Naam { get; set; }
    }
}
