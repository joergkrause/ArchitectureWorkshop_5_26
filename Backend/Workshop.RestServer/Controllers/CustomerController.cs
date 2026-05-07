using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.BusinessLogicLayer;
using Workshop.DataTransferObjects;

namespace Workshop.RestServer.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {

    private readonly ICustomerManager _customerManager;

    public CustomerController(ICustomerManager customerManager)
    {
      _customerManager = customerManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
      var customers = await _customerManager.GetAllCustomers();
      return Ok(customers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] int id)
    {
      try
      {
        var customer = await _customerManager.GetCustomerById(id);
        return Ok(customer);
      }
      catch (Exception ex)
      {
        return NotFound(ex.Message);
      }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto dto)
    {
      await _customerManager.CreateCustomer(dto);
      return CreatedAtAction(nameof(GetCustomerById), new { id = -1 /* TODO */ }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerUpdateDto dto)
    {
      // TODO
      return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
    {
      // TODO
      return Accepted();
    }
  }
}