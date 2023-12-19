using System.Reflection;

namespace Catalog.Infrastructure;

public sealed class AssemblyReference
{
    public static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
