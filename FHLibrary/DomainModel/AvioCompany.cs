namespace FHLibrary.DomainModel;
internal class AvioCompany
{
    internal protected virtual string? id { get; set; }
    internal protected virtual string? email { get; set; }
    internal protected virtual string? password { get; set; }
    internal protected virtual string? name { get; set; }
    internal protected virtual string? phone { get; set; }
    internal protected virtual string? state { get; set; }

    internal AvioCompany() 
    { 
        
    }
}
