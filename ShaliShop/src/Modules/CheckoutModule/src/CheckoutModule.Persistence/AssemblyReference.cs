using System.Reflection;

namespace CheckoutModule.Persistence;

public static class AssemblyReference
{
    public static Assembly GetAssemblyReference => typeof(CheckoutDbContext).Assembly;
}