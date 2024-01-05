namespace FHLibrary.DomainModel;
internal class AvioCompany
{
    internal protected virtual string? id { get; set; }
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    internal protected virtual string? email { get; set; }
    internal protected virtual string? password { get; set; }
    internal protected virtual string? name { get; set; }
    internal protected virtual string? phone { get; set; }
    internal protected virtual string? state { get; set; }
    internal protected virtual IList<ExpiredFlight>? expiredFlights { get; set; }
    internal protected virtual IList<Feedback>? feedbacks { get; set; }

    internal AvioCompany() 
    { 
        expiredFlights = new List<ExpiredFlight>();
        feedbacks = new List<Feedback>();
    }
}
