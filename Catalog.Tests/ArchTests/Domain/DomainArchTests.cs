using BuildingBlocks.Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NetArchTest.Rules;
using System.Reflection;
using Xunit.Abstractions;

namespace Catalog.Tests.ArchTests.Domain;

public sealed class DomainArchTests
{
    private readonly ITestOutputHelper _output;
    private readonly Assembly _assembly = Catalog.Domain.AssemblyReference.Assembly;

    public DomainArchTests(ITestOutputHelper outputHelper)
    {
        _output = outputHelper;
    }

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjectsMoreThanDomainBuildingBlocks()
    {
        string[] otherProjects = new string[]
        {
            References.ApplicationNamespace,
            References.ApiNamespace,
            References.ApplicationBuildingBlocksNamespace,
            References.IntegrationEventsNamespace,
            References.InfrastructureNamespace,
            References.InfrastructureBuildingBlocksNamesapce
        };

        TestResult testResult = Types
            .InAssembly(_assembly).That().ResideInNamespaceStartingWith(References.DomainNamespace)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_Should_HaveDependencyOnDomainBuildingBlocks()
    {
        TestResult testResult = Types
            .InAssembly(_assembly).That().ResideInNamespaceStartingWith(References.DomainNamespace)
            .Should()
            .NotHaveDependencyOn(References.DomainBuildingBlocksNamespace)
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEntities_Should_BePublic()
    {
        TestResult testResult = Types
            .InAssembly(_assembly).That().ImplementInterface(typeof(IEntity))
            .And().ImplementInterface(typeof(IAggregateRoot))
            .And().ResideInNamespaceStartingWith(References.DomainNamespace)
            .Should()
            .BePublic()
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEntities_Should_BeSealed()
    {
        TestResult testResult = Types
            .InAssembly(_assembly).That().ImplementInterface(typeof(IEntity))
            .And().ImplementInterface(typeof(IAggregateRoot))
            .And().ResideInNamespaceStartingWith(References.DomainNamespace)
            .Should()
            .BeSealed()
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEntities_Should_HaveAParameterlessConstructor()
    {
        IEnumerable<Type> entityTypes = Types
            .InAssembly(_assembly)
            .That().ImplementInterface(typeof(IAggregateRoot))
            .And().ImplementInterface(typeof(IEntity))
            .GetTypes();

        List<Type> failingTypes = new List<Type>();
        foreach (var entityType in entityTypes)
        {
            var constructors = entityType.GetConstructors(BindingFlags.NonPublic |
                                                          BindingFlags.Instance);

            if (!constructors.Any(u => u.IsPrivate && u.GetParameters().Length == 0))
            {
                failingTypes.Add(entityType);
            }
        }

        failingTypes.Should().BeEmpty();
    }

    [Fact]
    public void DomainEntities_Should_HaveOnlyPrivateConstructors()
    {
        IEnumerable<Type> entityTypes = Types
            .InAssembly(_assembly)
            .That().ImplementInterface(typeof(IEntity))
            .GetTypes();

        List<Type> failingTypes = new List<Type>();
        foreach (var entityType in entityTypes)
        {
            var constructors = entityType.GetConstructors(BindingFlags.Public | 
                                                          BindingFlags.Instance);

            if (constructors.Any(u => u.IsPublic))
            {
                failingTypes.Add(entityType);
            }
        }

        failingTypes.Should().BeEmpty();
    }

    [Fact]
    public void DomainInterfaces_Should_StartsWithI()
    {
        TestResult testResult = Types
            .InAssembly(_assembly).That().ResideInNamespaceStartingWith(References.DomainNamespace)
            .And().AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        testResult.IsSuccessful.Should().BeTrue();  
    }

    [Fact]
    public void DomainEvents_Should_EndsWithEvent()
    {
        TestResult result = Types
            .InAssembly(_assembly).That().ResideInNamespaceStartingWith(References.DomainNamespace)
            .And().ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("Event")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        var result = Types
            .InAssembly(_assembly).That().ResideInNamespaceStartingWith(References.DomainNamespace)
            .And().ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        if (!result.IsSuccessful)
        {
            _output.WriteLine("Failing classes");

            foreach (var failure in result.FailingTypeNames)
            {
                _output.WriteLine($"- {failure}");
            }
        }

        result.IsSuccessful.Should().BeTrue();
    }
}
