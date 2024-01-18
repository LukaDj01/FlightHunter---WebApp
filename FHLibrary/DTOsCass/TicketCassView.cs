using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FHLibrary.DTOsNeo
{
    public class TicketsCassView
    {
        public string? id { get; set; }
        public DateTime? purchaseDate { get; set; }
        public float? price { get; set; }
        public string? seatNumber { get; set; }
        public string? passengerEmail { get; set; }
        public string? flightSerialNumber { get; set; }


        public TicketsCassView() 
        {

        }

        internal TicketsCassView(TicketCass? t)
        {
            if(t != null)
            {
                id = t.id;
                purchaseDate = t.purchaseDate;
                price = t.price;
                seatNumber = t.seatNumber;
                passengerEmail = t.passengerEmail;
                flightSerialNumber = t.flightSerialNumber;
            }
        }
    }
}
