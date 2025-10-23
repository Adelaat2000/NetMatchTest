using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMatch.Logic.Models
{
    public class OfferteClass
    {
        //properties
        public int id { get; private set; }
        public string naam { get; private set; }

        //constructor
        public OfferteClass(int id, string naam)
        {
            this.id = id;
            this.naam = naam;
        }
    }
}
