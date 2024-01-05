namespace FHLibrary.DomainModel;
internal class Feedback
{
    internal protected virtual string? id { get; set; }
    internal protected virtual DateTime? date { get; set; }
    internal protected virtual string? comment { get; set; }
    internal protected virtual int? rate { get; set; } //postaviti ogranicenja

    internal protected virtual Passenger? passenger { get; set; }
    internal protected virtual Airport? airport { get; set; }
    internal protected virtual AvioCompany? avioCompany { get; set; }

    public Feedback() { }
}
