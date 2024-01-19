using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FHLibrary.QueryEntities;

internal class TicketCass
{
    internal protected virtual string? id { get; set; }
    internal protected virtual DateTime? purchaseDate { get; set; }
    internal protected virtual float? price { get; set; }
    internal protected virtual string? seatNumber { get; set; }
    internal protected virtual bool? isExpired { get; set; }
    internal protected virtual string? passengerEmail { get; set; }
    internal protected virtual string? flightSerialNumber{ get; set; }

    internal TicketCass()
    {

    }
}