namespace FHLibrary.DomainModel;
internal class ExpiredFlight
{
    internal protected virtual string? serial_number { get; set; }
    internal protected virtual int? capacity { get; set; }
    internal protected virtual int? available_seats { get; set; }

    internal ExpiredFlight() {}

}
