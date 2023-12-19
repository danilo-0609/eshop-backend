using System.Reflection;

namespace Catalog.Application;

public sealed class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
