using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLibrary.DTOsNeo
{
    public class LuggageView
    {
        public string? number { get; set; }
        public float? weight { get; set; }
        public float? dimension { get; set; }
        public float? pricePerKG { get; set; }

        public LuggageView() { }

        internal LuggageView(Luggage? l)
        {
            if (l != null)
            {
                number = l.number;
                weight = l.weight;
                dimension = l.dimension;
                pricePerKG = l.pricePerKG;
            }
        }
    }
}
