using System.Reflection;

namespace BuildingBlocks.Domain;

public sealed class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}
