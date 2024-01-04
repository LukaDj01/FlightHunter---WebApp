using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLibrary.DTOsNeo
{
    internal class PlaneView
    {
        public string? serialNumber { get; set; }
        public string? fuel { get; set; }
        public string? type { get; set; }
        public PlaneView() { }

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
