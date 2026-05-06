using Microsoft.EntityFrameworkCore;
using Workshop.DomainModels;

namespace Workshop.DataAccessLayer;

public class WorkshopContext : DbContext
{

  public WorkshopContext(DbContextOptions<WorkshopContext> options) 
    : base(options)
  {
    
  }

  public DbSet<Customer> Customers { get; set; }

  public override int SaveChanges()
  {
    throw new NotSupportedException();
  }


  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Customer>().ToTable("Customers");
    modelBuilder.Entity<Customer>().HasKey(c => c.Id);

    modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(100);
    modelBuilder.Entity<Customer>().Property(c => c.City).IsRequired(false).HasMaxLength(100);
    modelBuilder.Entity<Customer>().Property(c => c.Address).IsRequired(false).HasMaxLength(160);

  }

}
