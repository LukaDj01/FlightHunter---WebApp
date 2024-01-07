using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FHLibrary.DTOsNeo
{
    public class TicketsView
    {
        public string? id { get; set; }
        public string? purchaseDate { get; set; }
        public float? price { get; set; }
        public string? seatNumber { get; set; }
        public virtual IList<LuggageView>? luggages { get; set; }
        public PassengerView? passenger { get; set; }


        public TicketsView() 
        {
            luggages = new List<LuggageView>();
        }

        internal TicketsView(Ticket? t)
        {
            if(t != null)
            {
                id = t.id;
                purchaseDate = t.purchaseDate;
                price = t.price;
                seatNumber = t.seatNumber;
            }
        }

        internal TicketsView(Ticket? t, Passenger? p) : this(t)
        {
            passenger = new PassengerView(p);
        }
    }
}
