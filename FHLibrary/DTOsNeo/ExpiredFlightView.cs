namespace FHLibrary.DTOsNeo;
public class ExpiredFlightView
{
    public string? serial_number { get; set; }
    public int? capacity { get; set; }
    public int? available_seats { get; set; }
    public AvioCompanyView? avioCompany { get; set; }

    public ExpiredFlightView() { }
    internal ExpiredFlightView(ExpiredFlight? f) 
    { 
        
        if(f!=null)
        {
            serial_number = f.serial_number;
            capacity = f.capacity;
            available_seats = f.available_seats;
            avioCompany = new AvioCompanyView(f.avioCompany);
        }
    }
    internal ExpiredFlightView(ExpiredFlight? f, AvioCompany? ac) : this(f)
    {
        avioCompany = new AvioCompanyView(ac);
    }
}
