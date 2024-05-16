namespace PaySpace.Calculator.Web.Services.Models;

public class Credentials
{
    public string Email { get; set; } = "someone@demo.com";
    public string Password { get; set; } = "Hello@123";
}

public class Token
{
    private int ExpiresInSeconds { get; set; }
    public string TokenType { get; set; } = string.Empty!;
    public string AccessToken { get; set; } = string.Empty!;
    public int ExpiresIn
    {
        get { return ExpiresInSeconds; }
        set
        {
            ExpiresInSeconds = value;
            Expiry = DateTime.Now.AddSeconds(value);
        }
    }
    public string RefreshToken { get; set; } = string.Empty!;
    public DateTime Expiry { get; set; }
}
