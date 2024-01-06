namespace FHLibrary.DTOsNeo;
public class ExpiredFlightView
{
    public string? serial_number { get; set; }
    public int? capacity { get; set; }
    public int? available_seats { get; set; }
    public AvioCompanyView? avioCompany { get; set; }
    public AirportView? takeOffAirport { get; set; }
    public AirportView? landAirport { get; set; }
    public PlaneView? plane { get; set; }

    public DateTime? dateTimeLand { get; set; }
    public DateTime? dateTimeTakeOff { get; set; }
    public string? gateLand { get; set; }
    public string? gateTakeOff { get; set; }

    public ExpiredFlightView() { }
    internal ExpiredFlightView(ExpiredFlight? f) 
    { 
        
        if(f!=null)
        {
            serial_number = f.serial_number;
            capacity = f.capacity;
            available_seats = f.available_seats;
            avioCompany = new AvioCompanyView(f.avioCompany);
            dateTimeLand = f.dateTimeLand;
            dateTimeTakeOff = f.dateTimeTakeOff;
            gateLand = f.gateLand;
            gateTakeOff = f.gateTakeOff;
        }
    }
    internal ExpiredFlightView(ExpiredFlight? f, AvioCompany? ac, Airport? toa, Airport? la, Plane? p) : this(f)
    {
        avioCompany = new AvioCompanyView(ac);
        takeOffAirport = new AirportView(toa);
        landAirport = new AirportView(la);
        plane = new PlaneView(p);
    }
}
