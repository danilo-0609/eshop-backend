namespace Catalog.Tests.ArchTests;

internal sealed class References
{
    internal static string ApplicationNamespace = typeof(Catalog.Application.AssemblyReference).Namespace!;

    internal static string InfrastructureNamespace = typeof(Catalog.Infrastructure.AssemblyReference).Namespace!;

    internal static string IntegrationEventsNamespace = typeof(IntegrationEvents.AssemblyReference).Namespace!;

    internal static string ApiNamespace = typeof(API.AssemblyReference).Namespace!;

    internal static string ApplicationBuildingBlocksNamespace = typeof(BuildingBlocks.Application.AssemblyReference).Namespace!;

    internal static string DomainBuildingBlocksNamespace = typeof(BuildingBlocks.Domain.AssemblyReference).Namespace!;

    internal static string InfrastructureBuildingBlocksNamesapce = typeof(BuildingBlocks.Infrastructure.AssemblyReference).Namespace!;

    internal static string DomainNamespace = typeof(Catalog.Domain.AssemblyReference).Namespace!;
}
