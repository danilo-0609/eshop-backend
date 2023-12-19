using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit.Abstractions;

namespace Catalog.Tests.ArchTests;

public sealed class IntegrationEventsArchTests
{
    private readonly Assembly _assembly = typeof(Catalog.IntegrationEvents.AssemblyReference).Assembly;
    private readonly ITestOutputHelper _output;

    public IntegrationEventsArchTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void IntegrationEvents_Should_Not_HaveDependencyOnOtherProjects()
    {
        string[] otherProjects = new string[]
        {
            References.ApplicationNamespace,
            References.ApiNamespace,
            References.DomainNamespace,
            References.InfrastructureNamespace,
            References.DomainBuildingBlocksNamespace,
            References.InfrastructureBuildingBlocksNamesapce,
        };

        TestResult result = Types
            .InAssembly(_assembly).That().HaveNameEndingWith("IntegrationEvent")
            .Should()
            .NotHaveDependencyOnAll(otherProjects)
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
