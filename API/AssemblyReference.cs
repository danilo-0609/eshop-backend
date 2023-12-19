using System.Reflection;

namespace API;

public sealed class AssemblyReference
{
    public static Assembly Assembly = typeof(AssemblyReference).Assembly;
}
