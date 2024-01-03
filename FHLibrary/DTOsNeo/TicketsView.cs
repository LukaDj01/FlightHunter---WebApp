using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHLibrary.DTOsNeo
{
    public class TicketsView
    {
        public string? id { get; set; }
        public string? purchaseDate { get; set; }
        public float? price { get; set; }
        public TicketsView() { }

        internal TicketsView(Ticket? t)
        {
            if(t != null)
            {
                id = t.id;
                purchaseDate = t.purchaseDate;
                price = t.price;
            }
        }
    }
}
