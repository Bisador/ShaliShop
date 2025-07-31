using System.Reflection;

namespace CheckoutModule.Persistence;

public static class CheckoutAssemblyReference
{
    public static Assembly GetAssemblyReference => typeof(CheckoutDbContext).Assembly;
}