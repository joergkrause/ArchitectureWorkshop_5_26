using Microsoft.EntityFrameworkCore;
using Workshop.DataAccessLayer;

namespace Workshop.GrpcServer
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddDbContext<WorkshopContext>(options =>
      {
#if DEBUG
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
#endif
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
      });
      builder.Services.AddManagers();

      builder.Services.AddGrpc();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      app.MapGrpcService<CustomerSrv>();
      app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

      app.Run();
    }
  }
}
