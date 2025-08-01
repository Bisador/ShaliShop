using System.Reflection;

namespace CatalogModule.Application;

public static class CatalogModuleApplicationAssemblyReference
{
    public static Assembly Get() => typeof(CatalogModuleApplicationAssemblyReference).Assembly;
}