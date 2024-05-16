using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Authentication;

namespace PaySpace.Calculator.Web.Services.Helpers;

public class AuthService : IAuthService
{
    private readonly HttpClient client;
    private bool _registered;
    private Token? _token;

    public AuthService(HttpClient httpClient, IConfiguration configuration)
    {
        this.client = httpClient;
        var baseAddress = configuration.GetValue<string>("CalculatorSettings:ApiUrl")!;
        this.client.BaseAddress = new Uri(baseAddress);
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        if (!_registered)
        {
            await RegisterAsync();
        }

        if (_token is null)
        {
            try
            {
                await LoginAsync();
            }
            catch
            {
                await RegisterAsync();
            }
        }

        if (_token?.Expiry <= DateTime.Now)
        {
            await LoginAsync();
        }

        return _token?.AccessToken;
    }

    private async Task<bool> LoginAsync()
    {
        if (!_registered)
        {
            throw new AuthenticationException("Not registered. Please register first!");
        }

        var credentials = new Credentials(); // Defaults in class
        var content = new StringContent(JsonConvert.SerializeObject(credentials), MediaTypeHeaderValue.Parse("application/json"));
        var response = await client.PostAsync("login?useCookies=false", content, CancellationToken.None);

        if (!response.IsSuccessStatusCode)
        {
            _token = null;
            throw new AuthenticationException("Unable to login");
        }
        else
        {
            _token = await response!.Content!.ReadFromJsonAsync<Token>() ?? null;
        }

        return true;
    }

    private async Task<bool> RegisterAsync()
    {
        var credentials = new Credentials(); // Defaults in class
        var content = new StringContent(JsonConvert.SerializeObject(credentials), MediaTypeHeaderValue.Parse("application/json"));
        var response = await client.PostAsync("register", content, CancellationToken.None);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response!.Content!.ReadFromJsonAsync<ErrorResponse>();
            _registered = false;
            throw new AuthenticationException(error?.Errors.ToString());
        }
        else
        {
            _registered = true;
        }

        return _registered;
    }
}
