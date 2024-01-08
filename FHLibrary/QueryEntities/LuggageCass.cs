using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLibrary.DomainModel
{
    internal class LuggageCass
    {
        internal protected virtual string? number { get; set; }
        internal protected virtual float? weight { get; set; }
        internal protected virtual float? dimension { get; set; }
        internal protected virtual float? pricePerKG { get; set; }
        internal protected virtual string? ticketId { get; set; }

        internal LuggageCass() { }
    }
}
