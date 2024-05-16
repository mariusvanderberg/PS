using Microsoft.Extensions.Configuration;

namespace PaySpace.Calculator.Web.Services.Helpers;

public class PaySpaceService
{
    public readonly HttpClient client;

    public PaySpaceService(HttpClient httpClient, IConfiguration configuration, IAuthService authService)
    {
        this.client = httpClient;
        var baseAddress = configuration.GetValue<string>("CalculatorSettings:ApiUrl")!;
        this.client.BaseAddress = new Uri(baseAddress);

        // Authenticate / register
        var token = authService.GetAccessTokenAsync().Result;
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
}
