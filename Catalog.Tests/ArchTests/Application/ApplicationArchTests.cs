using BuildingBlocks.Application.Commands;
using BuildingBlocks.Application.Queries;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit.Abstractions;

namespace Catalog.Tests.ArchTests.Application;

public sealed class ApplicationArchTests
{
    private readonly Assembly _assembly = typeof(Catalog.Application.AssemblyReference).Assembly;
    private readonly ITestOutputHelper _output;

    public ApplicationArchTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        string[] otherProjects = new string[]
        {
            References.ApiNamespace,
            References.InfrastructureNamespace,
            References.InfrastructureBuildingBlocksNamesapce
        };

        TestResult result = Types
            .InAssembly(_assembly)
            .ShouldNot()
            .HaveDependencyOnAll(otherProjects)
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_HaveDependencyOnDomain()
    {
        TestResult result = Types
            .InAssembly(_assembly)
            .That().ResideInNamespaceEndingWith("Handler")
            .Should()
            .HaveDependencyOn(References.DomainNamespace)
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_BeInternal()
    {
        TestResult result = Types
            .InAssembly(_assembly)
            .That()
            .ImplementInterface(typeof(ICommandRequestHandler<,>))
            .And().ImplementInterface(typeof(IQueryRequestHandler<,>))
            .Should()
            .NotBePublic()
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_BeSealed()
    {
        TestResult result = Types
            .InAssembly(_assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .BeSealed()
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Commands_Should_BeSealed()
    {
        TestResult result = Types
            .InAssembly(_assembly)
            .That()
            .HaveNameEndingWith("Command")
            .Should()
            .BeSealed()
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void QueryHandlers_Should_ImplementErrorOr()
    {
        TestResult result = Types
            .InAssembly(_assembly)
            .That()
            .HaveNameEndingWith("QueryHandler")
            .Should()
            .HaveDependencyOn("ErrorOr")
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Queries_Should_BeSealed()
    {
        TestResult result = Types
            .InAssembly(_assembly)
            .That()
            .HaveNameEndingWith("Query")
            .Should()
            .BeSealed()
            .GetResult();

        if (!result.IsSuccessful)
        {
            CheckFails(result);
        }

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Commands_Should_ImplementErrorOr()
    {
        TestResult result = Types
            .InAssembly(_assembly)
            .That()
            .HaveNameEndingWith("CommandHandler")
            .Should()
            .HaveDependencyOn("ErrorOr")
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
