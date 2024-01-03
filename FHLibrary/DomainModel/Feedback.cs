namespace FHLibrary.DomainModel;
internal class Feedback
{
    internal protected virtual string? id { get; set; }
    internal protected virtual DateTime? date { get; set; }
    internal protected virtual string? comment { get; set; }
    internal protected virtual int? rate { get; set; } //postaviti ogranicenja

    //internal protected virtual string? pass_id { get; set; }
    //internal protected virtual string? airport_id { get; set; }
    //internal protected virtual string? company_id { get; set; }


    public Feedback() { }
}
