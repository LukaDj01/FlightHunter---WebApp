namespace FHLibrary.DomainModel;
internal class ExpiredFlight
{
    internal protected virtual string? serial_number { get; set; }
    internal protected virtual int? capacity { get; set; }
    internal protected virtual int? available_seats { get; set; }
    internal protected virtual AvioCompany? avioCompany { get; set; }
    internal protected virtual Airport? takeOffAirport { get; set; }
    internal protected virtual Airport? landAirport { get; set; }
    internal protected virtual Plane? plane { get; set; }
    internal protected virtual DateTime? dateTimeLand { get; set; }
    internal protected virtual DateTime? dateTimeTakeOff { get; set; }
    internal protected virtual string? gateLand { get; set; }
    internal protected virtual string? gateTakeOff { get; set; }

    internal ExpiredFlight() {}

}
