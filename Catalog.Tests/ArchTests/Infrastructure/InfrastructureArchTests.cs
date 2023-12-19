using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit.Abstractions;

namespace Catalog.Tests.ArchTests.Infrastructure;

public sealed class InfrastructureArchTests
{
    private readonly ITestOutputHelper _output;
    private readonly Assembly _assembly = typeof(Catalog.Infrastructure.AssemblyReference).Assembly;

    public InfrastructureArchTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ApplicationDbContext_Should_HaveDependencyOnEntityFramework()
    {
        TestResult result = Types
            .InAssembly(_assembly).That().HaveName("ApplicationDbContext")
            .Should()
            .HaveDependencyOnAny("Microsoft.EntityFrameworkCore")
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void RepositoriesImplementation_Should_BeSealedAndInternal()
    {
        TestResult result = Types
            .InAssembly(_assembly).That().HaveNameEndingWith("Repository")
            .Should()
            .BeSealed()
            .And()
            .NotBePublic()
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Configurations_Should_BeSealedAndInternal()
    {
        TestResult result = Types
            .InAssembly(_assembly).That().HaveNameEndingWith("Configuration")
            .Should()
            .BeSealed()
            .And()
            .NotBePublic()
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_ContainItsDependencyInjectionPublicAndStatic()
    {
        TestResult result = Types
            .InAssembly(_assembly).That().HaveName("DependencyInjection")
            .Should()
            .BePublic()
            .And()
            .BeStatic()
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    private void CheckFails(TestResult result)
    {
        _output.WriteLine("Fails in: ");

        foreach (string? failure in result.FailingTypeNames)
        {
            _output.WriteLine($"- {failure}");
        }
    }
}
