using System.Reflection;

namespace InventoryService.Persistence;

public static class AssemblyReference
{
    public static Assembly GetAssemblyReference => typeof(InventoryDbContext).Assembly;
}