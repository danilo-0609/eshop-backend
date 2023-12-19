using System.Reflection;

namespace Catalog.Domain;

public sealed class AssemblyReference
{
    public static Assembly Assembly = typeof(Assembly).Assembly;
}
