using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Workshop.DomainModels;

namespace Workshop.DataTransferObjects;

public class CustomerDto
{
  public int Id { get; set; }

  [StringLength(100), Required]
  [JsonPropertyName("customerNumber")]
  public string Number { get; set; } = default!;

  [StringLength(100), Required]
  [JsonPropertyName("customerName")]
  public string Name { get; set; } = default!;

  public static CustomerDto CreateCustomerDto(Customer model)
  {
    return new CustomerDto
    {
      Id = model.Id,
      Name = model.Name,
      Number = model.Number
    };
  }

}

public class CustomerDetailsDto : CustomerDto
{
  [StringLength(120)]
  public string? Address { get; set; }
  
  [StringLength(120)]
  public string? City { get; set; }
  public static new CustomerDetailsDto CreateCustomerDetailsDto(Customer model)
  {
    return new CustomerDetailsDto
    {
      Id = model.Id,
      Name = model.Name,
      Number = model.Number,
      Address = model.Address,
      City = model.City
    };
  }
}

public class CustomerUpdateDto : CustomerDetailsDto
{
  // TODO
}

public class  CustomerCreateDto : CustomerDetailsDto
{
  // TODO
}
public class CustomerDeleteDto
{
  public int Id { get; set; }
}
