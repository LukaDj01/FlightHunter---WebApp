namespace FHLibrary.DTOsNeo;
public class PassengerView
{
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string? email { get; set; }
    public string? password { get; set; }
    public string? passport { get; set; }
    public string? phone { get; set; }
    public DateTime? birth_date { get; set; }
    public string? nationality { get; set; }
    public string? first_name { get; set; }
    public string? last_name { get; set; }
    public string? addr_street { get; set; }
    public int? addr_stNo { get; set; }
    public virtual IList<FeedbackView>? feedbacks { get; set; }
    public PassengerView()
    { 
        feedbacks = new List<FeedbackView>();
    }
    internal PassengerView(Passenger? p) 
    { 
        if(p!=null){
            email = p.email;
            password = p.password;
            passport = p.passport;
            phone = p.phone;
            birth_date = p.birth_date;
            nationality = p.nationality;
            first_name = p.first_name;
            last_name = p.last_name;
            addr_street = p.addr_street;
            addr_stNo = p.addr_stNo;
        }
    }
}
