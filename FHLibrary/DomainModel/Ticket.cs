using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FHLibrary.DomainModel;

internal class Ticket
{
    internal protected virtual string? id { get; set; }
    internal protected virtual DateTime? purchaseDate { get; set; }
    internal protected virtual float? price { get; set; }
    internal protected virtual string? seatNumber { get; set; }
    internal protected virtual IList<Luggage>? luggages { get; set; }
    internal protected virtual Passenger? passenger { get; set; }
    internal protected virtual ExpiredFlight? flight { get; set; }

    internal Ticket()
    {
        luggages = new List<Luggage>();

    }
}