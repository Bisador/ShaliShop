using System.Reflection;

namespace CatalogService.Application;

public static class CatalogServiceApplicationAssemblyReference
{
    public static Assembly Get() => typeof(CatalogServiceApplicationAssemblyReference).Assembly;
}