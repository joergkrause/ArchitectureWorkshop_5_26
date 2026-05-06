namespace Workshop.DomainModels;

public class Customer : EntityBase
{

  public string Number { get; set; } = default!;

  public string Name { get; set; } = default!;

  public string? Address { get; set; }

  public string? City { get; set; }
  

}
