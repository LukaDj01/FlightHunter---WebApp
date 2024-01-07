using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FHLibrary.DomainModel;

internal class Ticket
{
    internal protected virtual string? id { get; set; }
    internal protected virtual string? purchaseDate { get; set; }
    internal protected virtual float? price { get; set; }
    internal protected virtual string? seatNumber { get; set; }
    internal protected virtual IList<Luggage>? luggages { get; set; }
    internal protected virtual Passenger? passenger { get; set; }
    public DateTime getPurchaseDate()
    {
        if (purchaseDate == null) return new DateTime();

        long timestamp = long.Parse(purchaseDate);
        DateTime startDateTime = new DateTime(1985, 1, 1, 0, 0, 0, 0);
        return startDateTime.AddMilliseconds(timestamp).ToLocalTime();
    }

    internal Ticket()
    {
        luggages = new List<Luggage>();

    }
}