namespace FHLibrary.DTOsNeo;
public class AvioCompanyView
{
    public string? id { get; set; }
    public string? email { get; set; }
    public string? password { get; set; }
    public string? name { get; set; }
    public string? phone { get; set; }
    public string? state { get; set; }
    public AvioCompanyView() { }
    internal AvioCompanyView(AvioCompany? ac) 
    { 
        if(ac!=null)
        {
            id = ac.id;
            email = ac.email;
            password = ac.password;
            name = ac.name;
            phone = ac.phone;
            state = ac.state;
        }
    }
}
