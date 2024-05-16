namespace PaySpace.Calculator.Web.Services.Models;

public class ErrorResponse
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Status { get; set; }
    public Errors Errors { get; set; } = null!;
}

public class Errors
{
    public string[] DuplicateUserName { get; set; } = null!;
}
