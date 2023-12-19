using System.Reflection;

namespace UserAccess.Application;

internal sealed class AssemblyReference
{
    public static Assembly Assembly = typeof(Assembly).Assembly;
}