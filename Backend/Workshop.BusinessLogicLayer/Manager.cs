using Microsoft.Extensions.DependencyInjection;
using Workshop.DataAccessLayer;

namespace Workshop.BusinessLogicLayer;

public abstract class Manager(IServiceProvider serviceProvider)
{
  private readonly WorkshopContext _context = serviceProvider.GetRequiredService<WorkshopContext>();

  protected WorkshopContext Context => _context;

  // protected ServerRestClient Client => _client;

  //protected ICustomerRepository CustomerRepository => serviceProvider.GetRequiredService<ICustomerRepository>();
}
