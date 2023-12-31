
namespace FHLibrary.DomainModel;

internal class Airport
{
    internal protected virtual string? pib { get; set; }
    internal protected virtual string? name { get; set; }
    internal protected virtual string? phone { get; set; }
    internal protected virtual string? address { get; set; }
    internal protected virtual string? city { get; set; }
    internal protected virtual string? state { get; set; }
    internal protected virtual string? gateNumber { get; set; }
    internal protected virtual IList<Feedback>? feedbacks { get; set; }
    internal protected virtual IList<ExpiredFlight>? takeOffFlight { get; set; }
    internal protected virtual IList<ExpiredFlight>? landFlight { get; set; }

    internal Airport()
    { 
        feedbacks = new List<Feedback>();
        takeOffFlight = new List<ExpiredFlight>();
        landFlight = new List<ExpiredFlight>();
    }

}
