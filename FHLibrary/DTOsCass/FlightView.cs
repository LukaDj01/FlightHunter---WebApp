﻿namespace FHLibrary.DTOsCass;
public class FlightView
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

    public FlightView() { }
    internal FlightView(Flight? f) 
    { 
        
        if(f!=null)
        {
            serial_number = f.serial_number;
            capacity = f.capacity;
            available_seats = f.available_seats;
            dateTimeLand = f.dateTimeLand;
            dateTimeTakeOff = f.dateTimeTakeOff;
            gateLand = f.gateLand;
            gateTakeOff = f.gateTakeOff;
        }
    }
    internal FlightView(FlightInput? f) 
    { 
        
        if(f!=null)
        {
            serial_number = f.serial_number;
            capacity = f.capacity;
            available_seats = f.available_seats;
            dateTimeLand = f.dateTimeLand;
            dateTimeTakeOff = f.dateTimeTakeOff;
            gateLand = f.gateLand;
            gateTakeOff = f.gateTakeOff;
        }
    }
}
