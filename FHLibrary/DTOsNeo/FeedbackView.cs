namespace FHLibrary.DTOsNeo;
public class FeedbackView
{
    public string? id { get; set; }
    public DateTime? date { get; set; }
    public string? comment { get; set; }
    public int? rate { get; set; }

    //public virtual string? pass_id { get; set; }
    //public virtual string? airport_id { get; set; }
    //public virtual string? company_id { get; set; }

    public FeedbackView() { }
    internal FeedbackView(Feedback? f) 
    { 
        
        if(f!=null)
        {
            id = f.id;
            date = f.date;
            comment = f.comment;
            rate = f.rate;
        }
    }
}
