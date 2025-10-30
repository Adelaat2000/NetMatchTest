
using System;

namespace NetMatch.Logic.Models
{
    public class OfferteClass
    {
        public OfferteClass() { }

        public OfferteClass(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
