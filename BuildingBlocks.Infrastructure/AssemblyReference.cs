using System.Reflection;

namespace BuildingBlocks.Infrastructure;

public sealed class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
