using System.Reflection;

namespace BuildingBlocks.Application;

public sealed class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
