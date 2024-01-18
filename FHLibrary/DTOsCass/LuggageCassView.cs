using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLibrary.DTOsNeo
{
    public class LuggageCassView
    {
        public string? number { get; set; }
        public float? weight { get; set; }
        public string? dimension { get; set; }
        public float? pricePerKG { get; set; }
        public string? ticketId { get; set; }


        public LuggageCassView() { }

        internal LuggageCassView(LuggageCass? l)
        {
            if (l != null)
            {
                number = l.number;
                weight = l.weight;
                dimension = l.dimension;
                pricePerKG = l.pricePerKG;
                ticketId = l.ticketId;
            }
        }
    }
}
