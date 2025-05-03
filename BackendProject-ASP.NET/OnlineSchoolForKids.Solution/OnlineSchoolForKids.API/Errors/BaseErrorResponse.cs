
namespace OnlineSchoolForKids.API.Errors;

public class BaseErrorResponse
{
    public int StatusCode { get; set; }
	public string? Message { get; set; }

    public BaseErrorResponse(int statusCode , string? message = null)
    {
        StatusCode = statusCode ;
        Message = message ?? GetStatusCodeDefaultMessage(statusCode);
    }

	private string GetStatusCodeDefaultMessage(int statusCode)
	{
        return statusCode switch
        {
            400 => "Bad Request You've Made !",
			401 => "UnAuthorized!",
            404 => "Resource Not Found",
            500 => "Internal Server Error",
            _ => ""
        };
	}
}
