using System.Reflection;

namespace CatalogModule.Persistence;

public static class AssemblyReference
{
    public static Assembly GetAssemblyReference => typeof(CatalogDbContext).Assembly;
}