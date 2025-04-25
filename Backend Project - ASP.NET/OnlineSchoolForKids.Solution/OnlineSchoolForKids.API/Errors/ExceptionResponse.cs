namespace OnlineSchoolForKids.API.Errors;

public class ExceptionResponse : BaseErrorResponse
{
    public string? Details { get; set; }

    public ExceptionResponse(int statusCode , string? message = null , string? details = null)
        :base(statusCode, message)
    {
        Details = details;
    }
}
