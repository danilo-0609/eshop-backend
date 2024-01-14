using System.Reflection;

namespace Shopping.Application;

internal sealed class AssemblyReference
{
    internal static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
