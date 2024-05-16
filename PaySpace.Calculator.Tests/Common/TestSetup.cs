using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Services;

namespace PaySpace.Calculator.Tests.Common;

/// <summary>
/// Base fixture <see cref="TestSetup"/> helps with setting up services
/// </summary>
[SetUpFixture]
public static class TestSetup
{
    private static IConfiguration? _configuration;
    private static IServiceScopeFactory? _scopeFactory;

    public static void RunBeforeAnyTests()
    {
        var builder = CreateConfigurationBuilder();

        _configuration = builder.Build();

        var hostingEnvironment = Mock.Of<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>(w =>
            w.EnvironmentName == "Test" &&
            w.ApplicationName == "PaySpace.Calculator");

        var services = new ServiceCollection();
        services.AddSingleton(_configuration);
        services.AddSingleton(hostingEnvironment);
        services.AddLogging();

        // Mapper
        services.AddMapster();

        // Add database...
        services.AddDbContext<CalculatorContext>(o => {
            var options = o.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new CalculatorContext((DbContextOptions<CalculatorContext>)options);
            context.Database.EnsureCreated();
        });
        services.AddCalculatorServices();

        _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
    }

    private static IConfigurationBuilder CreateConfigurationBuilder() =>
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile("appsettings.Development.json", true, true)
            .AddEnvironmentVariables();

    public static IServiceScope GetServiceScope()
    {
        return _scopeFactory!.CreateScope();
    }
}
