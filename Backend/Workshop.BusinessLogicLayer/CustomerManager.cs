using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Workshop.DataTransferObjects;

namespace Workshop.BusinessLogicLayer;

public class CustomerManager(IServiceProvider serviceProvider) : Manager(serviceProvider), ICustomerManager, IReadonlyCustomerManager
{

  public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
  {
    var models = await Context.Customers.ToListAsync();

    var dtos = models.Select(m => CustomerDto.CreateCustomerDto(m));

    return dtos;
  }

  public async Task<CustomerDetailsDto> GetCustomerById(int id)
  {
    var model = await Context.Customers.FindAsync(id);
    // TODO: result pattern
    if (model == null)
    {
      throw new Exception($"Customer with id {id} not found.");
    }
    var dto = CustomerDetailsDto.CreateCustomerDetailsDto(model);
    return dto;
  }

  public async Task CreateCustomer(CustomerCreateDto dto)
  {
    // TODO
  }

}
