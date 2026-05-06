using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Workshop.BusinessLogicLayer;
using Workshop.DataAccessLayer;
using Workshop.DomainModels;

namespace Workshop.UnitTests;

[TestClass]
public sealed class BusinessLogicTests
{

  // The Microsoft way!
  private static IServiceProvider BuildProvider(string dbName)
  {
    var services = new ServiceCollection();
    services.AddDbContext<WorkshopContext>(options => options.UseInMemoryDatabase(dbName));
    return services.BuildServiceProvider();
  }


  [TestMethod]
  public async Task CustomerManager_GetAllCustomers_ReturnsAllCustomers()
  {
    // Setup
    // Mock for IServiceProvider
    var serviceProvider = BuildProvider("TestDb");
    var seed = serviceProvider.GetRequiredService<WorkshopContext>();
    seed.Customers.Add(new Customer { Number = "A0001", Name = "Alice" });
    seed.Customers.Add(new Customer { Number = "A0002", Name = "Bob" });
    await seed.SaveChangesAsync();

    var customerManager = new CustomerManager(serviceProvider);
    // Execute
    var result = await customerManager.GetAllCustomers();

    // Verify
    Assert.AreEqual(2, result.Count());

  }

  [TestMethod]
  public void CustomerManager_GetCustomerById_ReturnsCustomer()
  {
    // Setup

    // Execute

    // Verify
  }

  [TestMethod]
  public void CustomerManager_GetCustomerById_ThrowsExceptionForWrongId()
  {
    // Setup

    // Execute

    // Verify
  }
}