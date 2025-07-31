using System.Reflection;

namespace InventoryModule.Persistence;

public static class AssemblyReference
{
    public static Assembly GetAssemblyReference => typeof(InventoryDbContext).Assembly;
}