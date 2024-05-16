using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace PaySpace.Calculator.Tests.Common;

/// <summary>
/// Base test for ease-of-use
/// </summary>
public class TestBase
{
    protected IServiceScope Scope { get; private set; }

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        TestSetup.RunBeforeAnyTests();
    }

    [SetUp]
    public void TestSetUp()
    {
        Scope = TestSetup.GetServiceScope();
    }

    [TearDown]
    public void TestTearDown()
    {
        Scope.Dispose();
    }

    protected T Resolve<T>() where T : class => Scope.ServiceProvider.GetService<T>()!;
}
