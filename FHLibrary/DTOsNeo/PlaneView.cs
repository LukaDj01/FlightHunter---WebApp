using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLibrary.DTOsNeo
{
    public class PlaneView
    {
        public string? serialNumber { get; set; }
        public string? fuel { get; set; }
        public string? type { get; set; }
        public virtual IList<ExpiredFlightView>? flights { get; set; }
        public PlaneView()
        { 
            flights = new List<ExpiredFlightView>();
        }

        internal PlaneView(Plane? p)
        {
            if (p != null)
            {
                serialNumber = p.serialNumber;
                fuel = p.fuel;
                type = p.type;
            }
        }
    }
}
