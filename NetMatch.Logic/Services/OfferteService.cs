using NetMatch.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMatch.Logic.Services
{
    public class OfferteService
    {
        public OfferteService()
        {

        }

        public List<OfferteClass> GetAllOffertes()
        {
            List<OfferteClass> ListOffertes = new List<OfferteClass>();
            ListOffertes.Add(new OfferteClass(1, "offerte nummer 1"));
            return ListOffertes;
        }
        
        public void Create(object model)
        {
            var OfferteClass = new OfferteClass(1, "offerte nummer 1");
        }
    }
}
