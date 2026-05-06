using Workshop.DataTransferObjects;

namespace Workshop.BusinessLogicLayer
{
  public interface ICustomerManager
  {
    Task<IEnumerable<CustomerDto>> GetAllCustomers();
    Task<CustomerDetailsDto> GetCustomerById(int id);
  }
}