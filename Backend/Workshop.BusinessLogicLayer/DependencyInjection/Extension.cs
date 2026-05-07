using Workshop.BusinessLogicLayer;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extension
{

  public static IServiceCollection AddManagers(this IServiceCollection services)
  {
    services.AddScoped<ICustomerManager, CustomerManager>();
    services.AddScoped<IReadonlyCustomerManager, CustomerManager>();

    return services;
  }

}
