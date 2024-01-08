namespace FHLibrary.QueryEntities;
internal class Flight
{
    internal protected virtual string? serial_number { get; set; }
    internal protected virtual int? capacity { get; set; }
    internal protected virtual int? available_seats { get; set; }
    internal protected virtual string? avioCompanyId { get; set; }
    internal protected virtual string? takeOffAirportPib { get; set; }
    internal protected virtual string? landAirportPib { get; set; }
    internal protected virtual string? planeSerialNumber { get; set; }
    internal protected virtual DateTime? dateTimeLand { get; set; }
    internal protected virtual DateTime? dateTimeTakeOff { get; set; }
    internal protected virtual string? gateLand { get; set; }
    internal protected virtual string? gateTakeOff { get; set; }

    internal Flight() {}

}
