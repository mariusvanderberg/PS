
namespace PaySpace.Calculator.Web.Services.Helpers
{
    public interface IAuthService
    {
        Task<string?> GetAccessTokenAsync();
    }
}