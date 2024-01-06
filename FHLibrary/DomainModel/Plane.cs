using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLibrary.DomainModel
{
    internal class Plane
    {
        internal protected virtual string? serialNumber { get; set; }
        internal protected virtual string? fuel { get; set; }
        internal protected virtual string? type { get; set; }
        internal protected virtual IList<ExpiredFlight>? flights { get; set; }

        internal Plane() 
        { 
            flights = new List<ExpiredFlight>();
        }
    }
}
