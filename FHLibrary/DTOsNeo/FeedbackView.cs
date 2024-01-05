namespace FHLibrary.DTOsNeo;
public class FeedbackView
{
    public string? id { get; set; }
    public DateTime? date { get; set; }
    public string? comment { get; set; }
    public int? rate { get; set; }

    public PassengerView? passenger { get; set; }
    public AirportView? airport { get; set; }
    public AvioCompanyView? avioCompany { get; set; }


    public FeedbackView() { }
    internal FeedbackView(Feedback? f) 
    { 
        
        if(f!=null)
        {
            id = f.id;
            date = f.date;
            comment = f.comment;
            rate = f.rate;
            passenger = new PassengerView(f.passenger);
            if(f.avioCompany!=null)
                avioCompany = new AvioCompanyView(f.avioCompany);
            else
                airport = new AirportView(f.airport);
        }
    }
    internal FeedbackView(Feedback? f, Passenger? p, AvioCompany? ac, Airport? ap) : this(f)
    {
        passenger = new PassengerView(p);
        if(ac!=null)
            avioCompany = new AvioCompanyView(ac);
        else
            airport = new AirportView(ap);
    }
}
