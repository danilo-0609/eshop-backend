using System.Reflection;

namespace Catalog.IntegrationEvents;

public sealed class AssemblyReference
{
    public static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
