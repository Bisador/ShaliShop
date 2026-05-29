using System.Reflection;

namespace CheckoutService.Persistence;

public static class AssemblyReference
{
    public static Assembly GetAssemblyReference => typeof(CheckoutDbContext).Assembly;
}