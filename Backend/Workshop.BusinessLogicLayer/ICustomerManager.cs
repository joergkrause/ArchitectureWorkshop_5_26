using Workshop.DataTransferObjects;

namespace Workshop.BusinessLogicLayer
{

  public interface IReadonlyCustomerManager
  {
    Task<IEnumerable<CustomerDto>> GetAllCustomers();
    Task<CustomerDetailsDto> GetCustomerById(int id);
  }

  public interface ICustomerManager : IReadonlyCustomerManager
  {
    Task CreateCustomer(CustomerCreateDto dto);
  }
}