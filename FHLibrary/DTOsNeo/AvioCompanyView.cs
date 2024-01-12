namespace FHLibrary.DTOsNeo;
public class AvioCompanyView
{
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string? email { get; set; }
    public string? password { get; set; }
    public string? name { get; set; }
    public string? phone { get; set; }
    public string? state { get; set; }
    public virtual IList<ExpiredFlightView>? expiredFlights { get; set; }
    public virtual IList<FlightView>? flights { get; set; }
    public virtual IList<FeedbackView>? feedbacks { get; set; }
    public virtual IList<PlaneView>? planes { get; set; }
    public AvioCompanyView() 
    { 
        expiredFlights = new List<ExpiredFlightView>();
        flights = new List<FlightView>();
        feedbacks = new List<FeedbackView>();
        planes = new List<PlaneView>();
    }
    internal AvioCompanyView(AvioCompany? ac) 
    { 
        if(ac!=null)
        {
            email = ac.email;
            password = ac.password;
            name = ac.name;
            phone = ac.phone;
            state = ac.state;
        }
    }
}
