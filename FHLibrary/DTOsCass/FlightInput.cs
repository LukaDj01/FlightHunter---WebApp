namespace FHLibrary.DTOsCass;
public class FlightInput
{
    public string? serial_number { get; set; }
    public int? capacity { get; set; }
    public int? available_seats { get; set; }
    public string? avioCompanyEmail { get; set; }
    public string? takeOffAirportPib { get; set; }
    public string? landAirportPib { get; set; }
    public string? planeSerialNumber { get; set; }
    public DateTime? dateTimeLand { get; set; }
    public DateTime? dateTimeTakeOff { get; set; }
    public string? gateLand { get; set; }
    public string? gateTakeOff { get; set; }

    public FlightInput() { }
    internal FlightInput(Flight? f) 
    { 
        
        if(f!=null)
        {
            serial_number = f.serial_number;
            capacity = f.capacity;
            available_seats = f.available_seats;
            avioCompanyEmail = f.avioCompanyEmail;
            takeOffAirportPib = f.takeOffAirportPib;
            landAirportPib = f.landAirportPib;
            planeSerialNumber = f.planeSerialNumber;
            dateTimeLand = f.dateTimeLand;
            dateTimeTakeOff = f.dateTimeTakeOff;
            gateLand = f.gateLand;
            gateTakeOff = f.gateTakeOff;
        }
    }
}
