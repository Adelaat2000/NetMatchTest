using NetMatch.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMatch.Logic.Mappers
{
    public class OfferteMapper
    {
        public OfferteClass toClass(int id, string naam)
        {
            return new OfferteClass(id, naam);
        }
    }
}
