using Grpc.Core;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using Workshop.BusinessLogicLayer;
using Workshop.DataTransferObjects;
using CustomerProto = Workshop.GrpcServer.CustomerService;

namespace Workshop.GrpcServer;

// public record Customer(Guid Id, string Name, string Email);

public class CustomerSrv : CustomerProto.CustomerServiceBase
{
  private readonly ILogger<CustomerSrv> _logger;
  private readonly ICustomerManager _customerManager;

  public CustomerSrv(ILogger<CustomerSrv> logger, ICustomerManager customerManager)
  {
    _logger = logger;
    _customerManager = customerManager;
  }

  public override async Task<CustomersReply> GetAllCustomers(CustomersRequest request, ServerCallContext context)
  {
    _logger.LogInformation("Received request to get all customers");
    var customers = await _customerManager.GetAllCustomers();

    var reply = new CustomersReply();
    reply.Customers.AddRange(customers.Select(c => new CustomerReply
    {
      Id = c.Id,
      Name = c.Name,
      Number = c.Number,
    }));

    return reply;
  }

  public override async Task<CustomerDetailReply> GetCustomerById(CustomerRequest request, ServerCallContext context)
  {
    _logger.LogInformation("Received request to get customer by id: {CustomerId}", request.Id);
    try
    {
      var customer = await _customerManager.GetCustomerById(request.Id);
      if (customer == null)
      {
        _logger.LogWarning("Customer with id {CustomerId} not found", request.Id);
        throw new RpcException(new Status(StatusCode.NotFound, $"Customer with id {request.Id} not found."));
      }
      return new CustomerDetailReply
      {
        Id = customer.Id,
        Name = customer.Name,
        Number = customer.Number,
        City = customer.City,
        Address = customer.Address,
      };
    }
    catch (RpcException)
    {
      throw;
    }
    catch (Exception ex) when (ex is not RpcException)
    {
      _logger.LogError(ex, "Error getting customer by id: {CustomerId}", request.Id);
      throw new RpcException(new Status(StatusCode.Internal, "An error occurred while processing the request."));
    }
  }

  public override async Task<CustomerReply> CreateCustomer(CreateCustomerRequest request, ServerCallContext context)
  {
    _logger.LogInformation("Received request to create customer with name: {CustomerName}", request.Name);

    var dto = new CustomerCreateDto
    {
      Name = request.Name,
      Number = request.Number,
      City = request.City,
      Address = request.Address,
    };

    await _customerManager.CreateCustomer(dto);

    return new CustomerReply
    {
      Id = dto.Id,
      Name = dto.Name,
      Number = dto.Number,
    };
  }

}
