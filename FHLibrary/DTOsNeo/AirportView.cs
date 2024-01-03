using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FHLibrary.DTOsNeo
{
    public class AirportView
    {
        public string? pib { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? gateNumber { get; set; }

        public AirportView() { }
        internal AirportView(Airport? a)
        {
            if(a!=null)
            {
                pib = a.pib;
                name = a.name;
                phone = a.phone;
                address = a.address;
                city = a.city;
                state = a.state;
                gateNumber = a.gateNumber;
            }
        }
    }
    
}
