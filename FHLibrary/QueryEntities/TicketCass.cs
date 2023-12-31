using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FHLibrary.DomainModel;

internal class TicketCass
{
    internal protected virtual string? id { get; set; }
    internal protected virtual string? purchaseDate { get; set; }
    internal protected virtual float? price { get; set; }
    internal protected virtual string? seatNumber { get; set; }
    internal protected virtual string? passengerEmail { get; set; }
    internal protected virtual string? flightSerialNumber{ get; set; }
    public DateTime getPurchaseDate()
    {
        if (purchaseDate == null) return new DateTime();

        long timestamp = long.Parse(purchaseDate);
        DateTime startDateTime = new DateTime(1985, 1, 1, 0, 0, 0, 0);
        return startDateTime.AddMilliseconds(timestamp).ToLocalTime();
    }

    internal TicketCass()
    {

    }
}