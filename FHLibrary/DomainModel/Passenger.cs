using System.ComponentModel.DataAnnotations;

namespace FHLibrary.DomainModel;
internal class Passenger
{
    //[DataType(DataType.EmailAddress)]
    //[EmailAddress]
    internal protected virtual string? email { get; set; }
    internal protected virtual string? password { get; set; }
    internal protected virtual string? passport { get; set; }
    internal protected virtual string? phone { get; set; }
    internal protected virtual DateTime? birth_date { get; set; }
    internal protected virtual string? nationality { get; set; }
    internal protected virtual string? first_name { get; set; }
    internal protected virtual string? last_name { get; set; }
    internal protected virtual string? addr_street { get; set; }
    internal protected virtual int? addr_stNo { get; set; }
    internal protected virtual IList<Feedback>? feedbacks { get; set; }
    internal protected virtual IList<Ticket>? tickets { get; set; }


    public Passenger()
    { 
        feedbacks = new List<Feedback>();
        tickets = new List<Ticket>();
    }
}
