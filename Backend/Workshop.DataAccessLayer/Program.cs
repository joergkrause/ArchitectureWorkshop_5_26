using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.DataAccessLayer
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("This is the Workshop.DataAccessLayer project. It contains the data access layer for the workshop application.");
    }

    public class DbContextDesignTimeFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<WorkshopContext>
    {
      public WorkshopContext CreateDbContext(string[] args)
      {
        var optionsBuilder = new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<WorkshopContext>();
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\Workshop;Initial Catalog=WorkshopDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");
        return new WorkshopContext(optionsBuilder.Options);
      }
    }

  }
}
