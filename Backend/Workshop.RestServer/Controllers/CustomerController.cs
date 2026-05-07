using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.BusinessLogicLayer;
using Workshop.DataTransferObjects;

namespace Workshop.RestServer.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [Produces("application/json")]
  public class CustomerController : ControllerBase
  {

    private readonly ICustomerManager _customerManager;

    public CustomerController(ICustomerManager customerManager)
    {
      _customerManager = customerManager;
    }

    [HttpGet(Name = "GetAll")]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCustomers()
    {
      var customers = await _customerManager.GetAllCustomers();
      return Ok(customers);
    }

    [HttpGet("{id:int}", Name = "GetById")]
    [ProducesResponseType(typeof(CustomerDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
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

    [HttpPost(Name = "Create")]
    [ProducesResponseType(typeof(CustomerDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto dto)
    {
      await _customerManager.CreateCustomer(dto);
      return CreatedAtAction(nameof(GetCustomerById), new { id = -1 /* TODO */ }, dto);
    }

    [HttpPut("{id:int}", Name = "Update")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerUpdateDto dto)
    {
      // TODO
      return NoContent();
    }

    [HttpDelete("{id:int}", Name = "Delete")]
    [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
    {
      // TODO
      return Accepted();
    }
  }
}