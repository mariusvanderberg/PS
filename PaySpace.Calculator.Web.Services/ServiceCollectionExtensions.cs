using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculator.Web.Services.Helpers;

namespace PaySpace.Calculator.Web.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCalculatorHttpServices(this IServiceCollection services)
        {
            services.AddHttpClient<PaySpaceService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddScoped<ICalculatorHttpService, CalculatorHttpService>();
        }
    }
}