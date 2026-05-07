
using Microsoft.EntityFrameworkCore;
using Workshop.DataAccessLayer;

namespace Workshop.RestServer
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

      builder.Services.AddControllers();
      // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
      builder.Services.AddOpenApi();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.MapOpenApi();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
