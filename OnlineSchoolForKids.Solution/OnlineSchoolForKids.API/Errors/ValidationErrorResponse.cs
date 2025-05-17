namespace OnlineSchoolForKids.API.Errors;

public class ValidationErrorResponse : BaseErrorResponse
{
    public IEnumerable<object> Errors { get; set; }

    public ValidationErrorResponse() 
        :base(400)
    {
        Errors = new List<string>();
    }
}
